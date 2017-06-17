using System;

namespace Task03
{
  public class Line : IFigure
  {
    public Dot A { get; private set; }
    public Dot B { get; private set; }
    public double Lenght { get; private set; }

    public void Draw()
    {
      Console.WriteLine(string.Format("Фигура: Линия. Координаты точки A: X: {0} Y: {1} \nКоординаты точки B: X: {2} Y: {3}",
        this.A.X, this.A.Y, this.B.X, this.B.Y));
    }

    public Line(Dot a, Dot b)
    {
      this.A = a;
      this.B = b;
      var squareLenght = Math.Pow((b.X - a.X), 2) + Math.Pow((b.Y - a.Y), 2);
      if (squareLenght != 0)
      {
        this.Lenght = Math.Sqrt(squareLenght);
      }
      else
      {
        this.Lenght = default(double);
      }
    }
  }
}
