using GameLib;

namespace Bot;

public struct PointWithMass
{
    public DoublePoint Point { get; set; }
    public int Mass { get; set; }

    public PointWithMass(DoublePoint point, int mass = 1)
    {
        Point = point;
        Mass = mass;
    }
}

public struct DoublePoint
{
    public double X { get; set; }
    public double Y { get; set; }

    public DoublePoint(double x = default, double y = default)
    {
        X = x;
        Y = y;
    }

    public static implicit operator DoublePoint(Point point)
    {
        return new DoublePoint(point.X, point.Y);
    }

    public static explicit operator Point(DoublePoint point)
    {
        return new Point((int)point.X, (int)point.Y);
    }
    
    public override string ToString()
    {
        return $"X={X}, Y={Y}";
    }
}