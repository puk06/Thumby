using Thumby.Core.Interfaces;
using Thumby.Core.Attributes;

namespace Thumby.Core.Models.Layer;

public class LayerEffect : ICanvasLayer
{
    public string LayerName { get; } = "レイヤー効果";
    public int Index { get; set; }

    [Title("レイヤー情報")]
    [UIField("有効化")]
    public bool Enabled { get; set; } = true;

    [UIField("レイヤー名")]
    public string CustomLayerName { get; set; } = string.Empty;

    [Space(10)]
    [Title("ぼかし")]
    [UIField("ガウスぼかし")]
    public bool ApplyGaussianBlur { get; set; } = false;

    [UIField("ぼかし半径")]
    public float BlurRadius { get; set; } = 0f;

    [Space(10)]
    [Title("明るさ")]
    [UIField("明るさ適用")]
    public bool ApplyBrightness { get; set; } = false;

    [UIField("明るさ")]
    public float Brightness { get; set; } = 0f;
    
    [Space(10)]
    [Title("コントラスト")]
    [UIField("コントラスト適用")]
    public bool ApplyContrast { get; set; } = false;

    [UIField("コントラスト")]
    public float Contrast { get; set; } = 0f;

    public ICanvasLayer Clone()
    {
        return new LayerEffect
        {
            Index = Index,
            Enabled = Enabled,
            CustomLayerName = CustomLayerName,
            ApplyGaussianBlur = ApplyGaussianBlur,
            BlurRadius = BlurRadius,
            ApplyBrightness = ApplyBrightness,
            Brightness = Brightness,
            ApplyContrast = ApplyContrast,
            Contrast = Contrast
        };
    }
}
