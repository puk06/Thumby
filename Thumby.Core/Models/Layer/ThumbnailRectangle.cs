using Thumby.Core.Attributes;
using Thumby.Core.Interfaces;

namespace Thumby.Core.Models.Layer;

public class ThumbnailRectangle : ICanvasLayer
{
    public string LayerName { get; } = "長方形";
    public int Index { get; set; }

    [UIField("有効化")]
    public bool Enabled { get; set; } = true;

    [UIField("レイヤー名")]
    public string CustomLayerName { get; set; } = string.Empty;

    [Space(30)]
    [UIField("座標 + サイズ")]
    public SerializableRectangle Rect { get; set; } = new();

    [UIField("角丸")]
    public float CornerRadius { get; set; }

    [UIField("線の太さ")]
    public float StrokeWidth { get; set; }

    [UIField("線の色 (RGBA)")]
    public SerializableColor StrokeColor { get; set; } = new();

    // [UIField("文字の下なら描画しない")]
    // public bool HideUnderText { get; set; }
}
