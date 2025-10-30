using Thumby.Core.Attributes;
using Thumby.Core.Interfaces;

namespace Thumby.Core.Models.Layer;

public class ThumbnailImage : ICanvasLayer
{
    public string LayerName { get; } = "画像";
    public int Index { get; set; }

    [UIField("有効化")]
    public bool Enabled { get; set; } = true;

    [UIField("レイヤー名")]
    public string CustomLayerName { get; set; } = string.Empty;

    [Space(30)]
    [UIField("ファイルパス")]
    public string FilePath { get; set; } = string.Empty;

    [UIField("位置")]
    public SerializablePoint Position { get; set; } = new();

    [UIField("スケール")]
    public float Scale { get; set; } = 1.0f;

    [UIField("ローテーション")]
    public float Rotation { get; set; } = 0f;
}
