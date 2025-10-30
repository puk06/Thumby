using System.Drawing;

namespace Thumby.Core.Models;

public class SerializableColor
{
    public byte R { get; set; }
    public byte G { get; set; }
    public byte B { get; set; }
    public byte A { get; set; }

    public SerializableColor() { }

    public SerializableColor(Color color)
    {
        R = color.R;
        G = color.G;
        B = color.B;
        A = color.A;
    }

    public SerializableColor(int r, int g, int b, int a)
    {
        R = (byte)Math.Clamp(r, 0, 255);
        G = (byte)Math.Clamp(g, 0, 255);
        B = (byte)Math.Clamp(b, 0, 255);
        A = (byte)Math.Clamp(a, 0, 255);
    }

    public Color ToColor() => Color.FromArgb(A, R, G, B);
}
