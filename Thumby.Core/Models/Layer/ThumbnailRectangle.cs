using Thumby.Core.Attributes;
using Thumby.Core.Interfaces;

namespace Thumby.Core.Models.Layer;

public class ThumbnailRectangle : ICanvasLayer
{
    public string LayerName { get; } = "長方形";
    public int Index { get; set; }

    [Title("レイヤー情報")]
    [UIField("有効化")]
    public bool Enabled { get; set; } = true;

    [UIField("レイヤー名")]
    public string CustomLayerName { get; set; } = string.Empty;

    [Space(10)]
    [Title("図形")]
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

    public ICanvasLayer Clone()
    {
        return new ThumbnailRectangle
        {
            Index = Index,
            Enabled = Enabled,
            CustomLayerName = CustomLayerName,
            Rect = new SerializableRectangle(
                Rect.X,
                Rect.Y,
                Rect.Width,
                Rect.Height
            ),
            CornerRadius = CornerRadius,
            StrokeWidth = StrokeWidth,
            StrokeColor = new SerializableColor(
                StrokeColor.A,
                StrokeColor.R,
                StrokeColor.G,
                StrokeColor.B
            )
        };
    }
}
