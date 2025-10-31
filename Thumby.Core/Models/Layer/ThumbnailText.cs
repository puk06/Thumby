using Thumby.Core.Attributes;
using Thumby.Core.Interfaces;

namespace Thumby.Core.Models.Layer;

public class ThumbnailText : ICanvasLayer
{
    public string LayerName { get; } = "テキスト";
    public int Index { get; set; }

    [Title("レイヤー情報")]
    [UIField("有効化")]
    public bool Enabled { get; set; } = true;

    [UIField("レイヤー名")]
    public string CustomLayerName { get; set; } = string.Empty;

    [UIField("枠のプレビュー")]
    public bool PreviewRect { get; set; } = true;

    [Space(10)]
    [Title("テキスト")]
    [UIField("内容")]
    [MultiLine]
    public string Content { get; set; } = string.Empty;

    [UIField("位置")]
    public SerializablePoint Position { get; set; } = new();

    [UIField("ローテーション")]
    public float Rotation { get; set; } = 0f;

    [UIField("サイズ")]
    public float FontSize { get; set; } = 70f;
    
    [UIField("字間")]
    public float LetterSpacing { get; set; }

    [UIField("行間")]
    public float LineSpacing { get; set; }

    [UIField("フォント名")]
    public string FontName { get; set; } = string.Empty;

    [UIField("色 (RGBA)")]
    public SerializableColor Color { get; set; } = new(255, 255, 255, 255);

    [Space(10)]
    [Title("影設定")]
    [UIField("文字の影")]
    public bool Shadow { get; set; } = false;

    [UIField("影の色 (RGBA)")]
    public SerializableColor ShadowColor { get; set; } = new(0, 0, 0, 100);

    [UIField("影の位置オフセット")]
    public SerializablePoint ShadowOffset { get; set; } = new(15, 15);

    [Space(10)]
    [Title("下線設定")]
    [UIField("文字の下線")]
    public bool Underline { get; set; } = false;

    [UIField("下線の色 (RGBA)")]
    public SerializableColor UnderlineColor { get; set; } = new(255, 255, 255, 255);

    [UIField("下線の太さ")]
    public int UnderlineWidth { get; set; } = 2;

    [UIField("下線のオフセット")]
    public int UnderlineOffset { get; set; } = 0;

    public ICanvasLayer Clone()
    {
        return new ThumbnailText
        {
            Index = Index,
            Enabled = Enabled,
            CustomLayerName = CustomLayerName,
            PreviewRect = PreviewRect,
            Content = Content,
            Position = new SerializablePoint(Position.X, Position.Y),
            Rotation = Rotation,
            FontSize = FontSize,
            LineSpacing = LineSpacing,
            LetterSpacing = LetterSpacing,
            FontName = FontName,
            Color = new SerializableColor(
                Color.A,
                Color.R,
                Color.G,
                Color.B
            ),
            Shadow = Shadow,
            ShadowColor = new SerializableColor(
                ShadowColor.A,
                ShadowColor.R,
                ShadowColor.G,
                ShadowColor.B
            ),
            ShadowOffset = new SerializablePoint(ShadowOffset.X, ShadowOffset.Y),
            Underline = Underline,
            UnderlineColor = new SerializableColor(
                UnderlineColor.A,
                UnderlineColor.R,
                UnderlineColor.G,
                UnderlineColor.B
            ),
            UnderlineWidth = UnderlineWidth,
            UnderlineOffset = UnderlineOffset
        };
    }
}
