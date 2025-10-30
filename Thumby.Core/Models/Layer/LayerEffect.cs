using Thumby.Core.Interfaces;
using Thumby.Core.Attributes;

namespace Thumby.Core.Models.Layer;

public class LayerEffect : ICanvasLayer
{
    public string LayerName { get; } = "レイヤー効果";
    public int Index { get; set; }

    [UIField("有効化")]
    public bool Enabled { get; set; } = true;

    [UIField("レイヤー名")]
    public string CustomLayerName { get; set; } = string.Empty;

    [Space(30)]
    [UIField("ガウスぼかし")]
    public bool ApplyGaussianBlur { get; set; } = false;

    [UIField("ぼかし半径")]
    public float BlurRadius { get; set; } = 0f;

    [Space(10)]

    [UIField("明るさ適用")]
    public bool ApplyBrightness { get; set; } = false;

    [UIField("明るさ")]
    public float Brightness { get; set; } = 0f;
    
    [Space(10)]
    [UIField("コントラスト適用")]
    public bool ApplyContrast { get; set; } = false;

    [UIField("コントラスト")]
    public float Contrast { get; set; } = 0f;
}
