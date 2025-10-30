using Thumby.Core.Attributes;
using Thumby.Core.Interfaces;

namespace Thumby.Core.Models.Layer;

public class ThumbnailText : ICanvasLayer
{
    public string LayerName { get; } = "テキスト";
    public int Index { get; set; }

    [UIField("有効化")]
    public bool Enabled { get; set; } = true;

    [UIField("レイヤー名")]
    public string CustomLayerName { get; set; } = string.Empty;

    [Space(30)]
    [UIField("内容")]
    [MultiLine]
    public string Content { get; set; } = string.Empty;

    [UIField("位置")]
    public SerializablePoint Position { get; set; } = new();

    [UIField("サイズ")]
    public float FontSize { get; set; } = 70f;

    [UIField("行間")]
    public float LineSpacing { get; set; }

    [UIField("字間")]
    public float LetterSpacing { get; set; }

    [UIField("フォント名")]
    public string FontName { get; set; } = string.Empty;

    [UIField("色 (RGBA)")]
    public SerializableColor Color { get; set; } = new(255, 255, 255, 255);
}
