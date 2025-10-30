using System.Drawing.Imaging;

namespace Thumby.WinForm.Utils;

public static class GraphicsExtensions
{
    public static Bitmap GaussianBlur(this Bitmap image, int radius)
    {
        if (radius < 1) return image;

        int w = image.Width;
        int h = image.Height;
        Bitmap blurred = new(w, h);

        Rectangle rect = new(0, 0, w, h);
        BitmapData srcData = image.LockBits(rect, ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
        BitmapData dstData = blurred.LockBits(rect, ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);

        int bytes = srcData.Stride * srcData.Height;
        byte[] pixelBuffer = new byte[bytes];
        byte[] resultBuffer = new byte[bytes];

        System.Runtime.InteropServices.Marshal.Copy(srcData.Scan0, pixelBuffer, 0, bytes);
        image.UnlockBits(srcData);

        double sigma = radius / 2.0;
        double[,] kernel = CreateGaussianKernel(radius, sigma);
        int filterOffset = radius;

        for (int y = filterOffset; y < h - filterOffset; y++)
        {
            for (int x = filterOffset; x < w - filterOffset; x++)
            {
                double blue = 0, green = 0, red = 0, alpha = 0;

                for (int fy = -filterOffset; fy <= filterOffset; fy++)
                {
                    for (int fx = -filterOffset; fx <= filterOffset; fx++)
                    {
                        int calcOffset = ((y + fy) * srcData.Stride) + ((x + fx) * 4);

                        double weight = kernel[fy + filterOffset, fx + filterOffset];

                        blue += pixelBuffer[calcOffset] * weight;
                        green += pixelBuffer[calcOffset + 1] * weight;
                        red += pixelBuffer[calcOffset + 2] * weight;
                        alpha += pixelBuffer[calcOffset + 3] * weight;
                    }
                }

                int resultOffset = (y * srcData.Stride) + (x * 4);
                resultBuffer[resultOffset] = (byte)Math.Min(Math.Max(blue, 0), 255);
                resultBuffer[resultOffset + 1] = (byte)Math.Min(Math.Max(green, 0), 255);
                resultBuffer[resultOffset + 2] = (byte)Math.Min(Math.Max(red, 0), 255);
                resultBuffer[resultOffset + 3] = (byte)Math.Min(Math.Max(alpha, 0), 255);
            }
        }

        System.Runtime.InteropServices.Marshal.Copy(resultBuffer, 0, dstData.Scan0, bytes);
        blurred.UnlockBits(dstData);

        return blurred;
    }

    private static double[,] CreateGaussianKernel(int radius, double sigma)
    {
        int size = radius * 2 + 1;
        double[,] kernel = new double[size, size];
        double sum = 0;
        double piSigma = 2 * Math.PI * sigma * sigma;

        for (int y = -radius; y <= radius; y++)
        {
            for (int x = -radius; x <= radius; x++)
            {
                double value = Math.Exp(-(x * x + y * y) / (2 * sigma * sigma)) / piSigma;
                kernel[y + radius, x + radius] = value;
                sum += value;
            }
        }

        // 正規化
        for (int y = 0; y < size; y++)
        {
            for (int x = 0; x < size; x++)
            {
                kernel[y, x] /= sum;
            }
        }

        return kernel;
    }
}
