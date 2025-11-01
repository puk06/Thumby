using System.Drawing.Drawing2D;
using System.Drawing.Text;
using Thumby.Core.Interfaces;

namespace Thumby.WinForm.LayerProcessing;

internal class LayerProcessor
{
    internal static void ProcessLayers(Bitmap bitmap, Graphics graphics, List<ICanvasLayer> canvasLayers, bool isPreview)
    {
        graphics.SmoothingMode = SmoothingMode.AntiAlias;
        graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
        graphics.CompositingQuality = CompositingQuality.HighQuality;
        graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
        graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;

        for (int i = canvasLayers.Count - 1; i >= 0; i--)
        {
            var canvasLayer = canvasLayers[i];
            if (!canvasLayer.Enabled) continue;

            ThumbnailRenderer.ProcessLayer(bitmap, graphics, canvasLayer, isPreview);
        }
    }
}
