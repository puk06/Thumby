using Thumby.Core.Attributes;
using Thumby.Core.Interfaces;

namespace Thumby.Core.Models.Layer;

public class ThumbnailLine : ICanvasLayer
{
    public string LayerName { get; } = "直線";
    public int Index { get; set; }

    [Title("レイヤー情報")]
    [UIField("有効化")]
    public bool Enabled { get; set; } = true;

    [UIField("レイヤー名")]
    public string CustomLayerName { get; set; } = string.Empty;

    [Space(10)]
    [Title("直線")]
    [UIField("開始点")]
    public SerializablePoint StartPoint { get; set; } = new();

    [UIField("終了点")]
    public SerializablePoint EndPoint { get; set; } = new();

    [UIField("線の太さ")]
    public int LineWidth { get; set; } = 3;
    
    [UIField("線の色")]
    public SerializableColor LineColor { get; set; } = new(255, 255, 255, 255);

    [UIField("角を丸くする")]
    public bool RoundCorner { get; set; } = true;

    public ICanvasLayer Clone()
    {
        return new ThumbnailLine
        {
            Index = Index,
            Enabled = Enabled,
            CustomLayerName = CustomLayerName,
            StartPoint = new SerializablePoint(StartPoint.X, StartPoint.Y),
            EndPoint = new SerializablePoint(EndPoint.X, EndPoint.Y),
            LineWidth = LineWidth,
            LineColor = new SerializableColor(
                LineColor.A,
                LineColor.R,
                LineColor.G,
                LineColor.B
            ),
            RoundCorner = RoundCorner
        };
    }
}
