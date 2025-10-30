namespace Thumby.Core.Models;

public class SerializablePoint
{
    public int X { get; set; }
    public int Y { get; set; }

    public SerializablePoint() { }

    public SerializablePoint(int x, int y)
    {
        X = x;
        Y = y;
    }
}
