namespace Bot;

public static class Utils
{
    public static double GetLength(DoublePoint a, DoublePoint b) => Math.Sqrt(Math.Pow(a.X - b.X, 2) + Math.Pow(a.Y - b.Y, 2));
}