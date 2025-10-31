using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using Thumby.Core.Models;
using Thumby.Core.Models.Layer;
using Thumby.WinForm.Utils;

namespace Thumby.WinForm.LayerProcessing;

public static class ThumbnailRenderer
{
    public static void ProcessTextLayer(Graphics graphics, ThumbnailText text, bool isPreview = false)
    {
        using Font font = new(text.FontName, text.FontSize);
        using Brush brush = new SolidBrush(Color.FromArgb(text.Color.A, text.Color.R, text.Color.G, text.Color.B));
        using Pen underlinePen = new(text.UnderlineColor.ToColor(), text.UnderlineWidth);
        using Brush shadowBrush = new SolidBrush(Color.FromArgb(text.ShadowColor.A, text.ShadowColor.R, text.ShadowColor.G, text.ShadowColor.B));
        using GraphicsPath path = new();

        StringFormat format = new()
        {
            LineAlignment = StringAlignment.Near,
            Alignment = StringAlignment.Near
        };

        float letterSpacing = text.LetterSpacing;
        float lineSpacing = text.LineSpacing;

        // 描画開始位置
        SerializablePoint position = text.Position;

        var (width, height) = CalculateTextRectangle(graphics, text);

        float centerX = text.Position.X + width / 2f;
        float centerY = text.Position.Y + height / 2f;

        graphics.TranslateTransform(centerX, centerY);
        graphics.RotateTransform(text.Rotation);
        graphics.TranslateTransform(-centerX, -centerY);

        float x = position.X;
        float y = position.Y;

        var lineBreakCharsArray = text.Content.Split(Environment.NewLine);
        foreach (var lineBreakChars in lineBreakCharsArray)
        {
            for (int i = 0; i < lineBreakChars.Length; i++)
            {
                char c = lineBreakChars[i];
                bool isLastChar = i == lineBreakChars.Length - 1;

                if (text.Shadow) graphics.DrawString(c.ToString(), font, shadowBrush, new PointF(x + text.ShadowOffset.X, y + text.ShadowOffset.Y), format);
                graphics.DrawString(c.ToString(), font, brush, new PointF(x, y), format);

                float charWidth = graphics.MeasureString(c.ToString(), font).Width;

                if (text.Underline)
                {
                    path.AddLine(new PointF(x, y + font.GetHeight(graphics) + text.UnderlineOffset), new PointF(isLastChar ? x + charWidth : x + charWidth + text.LetterSpacing, y + font.GetHeight(graphics) + text.UnderlineOffset));
                    path.CloseFigure();
                    graphics.DrawPath(underlinePen, path);
                }

                x += charWidth;
                if (!isLastChar) x += letterSpacing;
            }

            x = position.X;
            y += font.GetHeight(graphics) + lineSpacing;
        }

        if (isPreview && text.PreviewRect) RenderTextLine(graphics, position, width, height);

        graphics.ResetTransform();
    }

    private static void RenderTextLine(Graphics graphics, SerializablePoint position, float width, float height)
    {
        using Pen pen = new(Color.White, 2);
        using GraphicsPath path = new();

        var defaultPosition = new PointF(position.X, position.Y);

        path.AddLine(defaultPosition, new PointF(position.X + width, position.Y));
        path.AddLine(new PointF(position.X + width, position.Y), new PointF(position.X + width, position.Y + height));
        path.AddLine(new PointF(position.X + width, position.Y + height), new PointF(position.X, position.Y + height));
        path.AddLine(new PointF(position.X, position.Y + height), defaultPosition);
        path.CloseFigure();
        graphics.DrawPath(pen, path);
    }

    private static (float width, float height) CalculateTextRectangle(Graphics graphics, ThumbnailText text)
    {
        using Font font = new(text.FontName, text.FontSize);
        using Brush brush = new SolidBrush(Color.FromArgb(text.Color.A, text.Color.R, text.Color.G, text.Color.B));

        StringFormat format = new()
        {
            LineAlignment = StringAlignment.Near,
            Alignment = StringAlignment.Near
        };

        float letterSpacing = text.LetterSpacing;
        float lineSpacing = text.LineSpacing;

        SerializablePoint position = text.Position;

        graphics.TranslateTransform(position.X, position.Y);
        graphics.RotateTransform(text.Rotation);
        graphics.TranslateTransform(-position.X, -position.Y);

        float x = 0;
        float y = 0;

        for (int i = 0; i < text.Content.Length; i++)
        {
            char c = text.Content[i];
            if (c == '\n')
            {
                x = position.X;
                y += font.GetHeight(graphics) + lineSpacing;
                continue;
            }

            float charWidth = graphics.MeasureString(c.ToString(), font).Width;

            x += charWidth;

            if (i != text.Content.Length - 1)
            {
                x += letterSpacing;
            }
        }

        return (x, y + font.GetHeight(graphics));
    }

    //  画像レイヤー
    public static void ProcessImageLayer(Graphics graphics, ThumbnailImage image)
    {
        if (!File.Exists(image.FilePath)) return;

        using Image img = Image.FromFile(image.FilePath);

        Rectangle destRect = new(
            image.Position.X,
            image.Position.Y,
            (int)(img.Width * image.Scale),
            (int)(img.Height * image.Scale)
        );

        graphics.DrawImage(img, destRect, 0, 0, img.Width, img.Height, GraphicsUnit.Pixel);
    }

    //  長方形・枠レイヤー
    public static void ProcessRectangleLayer(Graphics graphics, ThumbnailRectangle rect)
    {
        using Pen pen = new(rect.StrokeColor.ToColor(), rect.StrokeWidth);
        using GraphicsPath path = new();

        float r = rect.CornerRadius;
        var area = rect.Rect;

        if (r > 0)
        {
            path.AddArc(area.X, area.Y, r, r, 180, 90);
            path.AddArc(area.Right - r, area.Y, r, r, 270, 90);
            path.AddArc(area.Right - r, area.Bottom - r, r, r, 0, 90);
            path.AddArc(area.X, area.Bottom - r, r, r, 90, 90);
            path.CloseFigure();
            graphics.DrawPath(pen, path);
        }
        else
        {
            graphics.DrawRectangle(pen, area.X, area.Y, area.Width, area.Height);
        }
    }

    public static void ProcessLineLayer(Graphics graphics, ThumbnailLine line)
    {
        using Pen pen = new(line.LineColor.ToColor(), line.LineWidth)
        {
            StartCap = line.RoundCorner ? LineCap.Round : LineCap.Flat,
            EndCap = line.RoundCorner ? LineCap.Round : LineCap.Flat,
            LineJoin = LineJoin.Round
        };

        graphics.DrawLine(
            pen,
            new PointF(line.StartPoint.X, line.StartPoint.Y),
            new PointF(line.EndPoint.X, line.EndPoint.Y)
        );
    }

    // レイヤー効果（ガウスぼかし・明るさ・コントラスト）
    public static void ProcessEffectLayer(Bitmap source, Graphics graphics, LayerEffect effect)
    {
        if (effect.ApplyGaussianBlur && effect.BlurRadius > 0)
        {
            ApplyGaussianBlur(source, graphics, effect.BlurRadius);
        }

        if ((effect.ApplyBrightness && effect.Brightness != 0) || (effect.ApplyContrast && effect.Contrast != 0))
        {
            ApplyBrightnessContrast(source, graphics, effect.Brightness, effect.Contrast);
        }
    }

    // ガウスぼかし
    private static void ApplyGaussianBlur(Bitmap bitmap, Graphics graphics, float radius)
    {
        Bitmap blurred = GraphicsExtensions.GaussianBlur(bitmap, 5);
        graphics.DrawImage(blurred, 0, 0);
    }

    //  明るさ・コントラスト調整
    private static void ApplyBrightnessContrast(Bitmap image, Graphics graphics, float brightness, float contrast)
    {
        using ImageAttributes attributes = new();
        float b = brightness / 100f;
        float c = contrast / 100f + 1f;

        float t = 0.5f * (1f - c);

        float[][] matrix = [
            [c, 0, 0, 0, 0],
            [0, c, 0, 0, 0],
            [0, 0, c, 0, 0],
            [0, 0, 0, 1, 0],
            [b + t, b + t, b + t, 0, 1]
        ];

        ColorMatrix cm = new(matrix);
        attributes.SetColorMatrix(cm);

        graphics.DrawImage(image, new Rectangle(0, 0, image.Width, image.Height), 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, attributes);
    }
}
