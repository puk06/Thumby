using System.Text.Json.Serialization;
using Thumby.Core.Models.Layer;

namespace Thumby.Core.Interfaces;

[JsonPolymorphic(TypeDiscriminatorPropertyName = "$type")]
[JsonDerivedType(typeof(ThumbnailText), "TextLayer")]
[JsonDerivedType(typeof(ThumbnailImage), "ImageLayer")]
[JsonDerivedType(typeof(ThumbnailRectangle), "RectangleLayer")]
[JsonDerivedType(typeof(LayerEffect), "LayerEffect")]
public interface ICanvasLayer
{
    public string LayerName { get; }
    public int Index { get; set; }

    public bool Enabled { get; set; }
    public string CustomLayerName { get; set; }

    public string GetLayerName()
        => $"{(string.IsNullOrEmpty(CustomLayerName) ? $"{LayerName}レイヤー {Index}" : CustomLayerName)}";

    public string ToString(int index)
        => $"{(index == -1 ? "N/A" : index)}  |  {LayerName}  |   {GetLayerName()}";
}
