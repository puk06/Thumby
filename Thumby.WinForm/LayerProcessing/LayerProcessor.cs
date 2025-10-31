using System.Drawing.Drawing2D;
using System.Drawing.Text;
using Thumby.Core.Interfaces;
using Thumby.Core.Models.Layer;

namespace Thumby.WinForm.LayerProcessing;

internal class LayerProcessor
{
    public static void ProcessLayers(Bitmap bitmap, Graphics graphics, List<ICanvasLayer> canvasLayers, bool isPreview)
    {
        graphics.SmoothingMode = SmoothingMode.AntiAlias;
        graphics.TextRenderingHint = TextRenderingHint.AntiAlias;

        for (int i = canvasLayers.Count - 1; i >= 0; i--)
        {
            var item = canvasLayers[i];
            if (!item.Enabled) continue;
            
            if (item is ThumbnailText thumbnailText)
                ThumbnailRenderer.ProcessTextLayer(graphics, thumbnailText, isPreview);
            if (item is ThumbnailImage thumbnailImage)
                ThumbnailRenderer.ProcessImageLayer(graphics, thumbnailImage);
            if (item is ThumbnailRectangle thumbnailRectangle)
                ThumbnailRenderer.ProcessRectangleLayer(graphics, thumbnailRectangle);
            if (item is ThumbnailLine thumbnailLine)
                ThumbnailRenderer.ProcessLineLayer(graphics, thumbnailLine);
            if (item is LayerEffect layerEffect)
                ThumbnailRenderer.ProcessEffectLayer(bitmap, graphics, layerEffect);
        }
    }
}
