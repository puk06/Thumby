namespace Thumby.Core.Models;

public class SerializableRectangle
{
    public int X { get; set; }
    public int Y { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }

    public SerializableRectangle() { }

    public SerializableRectangle(int x, int y, int width, int height)
    {
        X = x;
        Y = y;
        Width = width;
        Height = height;
    }

    public int Right => X + Width;
    public int Bottom => Y + Height;
}
