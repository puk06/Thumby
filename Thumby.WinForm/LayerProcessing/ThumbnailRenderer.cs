using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using Thumby.Core.Models;
using Thumby.Core.Models.Layer;
using Thumby.WinForm.Utils;

namespace Thumby.WinForm.LayerProcessing;

public static class ThumbnailRenderer
{
    public static void ProcessTextLayer(Graphics graphics, ThumbnailText text)
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

        // 描画開始位置
        SerializablePoint position = text.Position;

        float x = position.X;
        float y = position.Y;

        foreach (char c in text.Content)
        {
            if (c == '\n')
            {
                x = position.X;
                y += font.GetHeight(graphics) + lineSpacing;
                continue;
            }

            graphics.DrawString(c.ToString(), font, brush, new PointF(x, y), format);

            float charWidth = graphics.MeasureString(c.ToString(), font).Width;
            x += charWidth + letterSpacing;
        }
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
