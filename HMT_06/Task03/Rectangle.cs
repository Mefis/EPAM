using System;

namespace Task03
{
  public class Rectangle : IFigure
  {
    public Dot A { get; private set; }
    public Dot B { get; private set; }
    public Dot C { get; private set; }
    public Dot D { get; private set; }
    public double Length { get; private set; }

    public void Draw()
    {
      Console.WriteLine(string.Format("Фигура: Прямоугольник. Координаты точки A: X: {0} Y: {1} \nКоординаты точки B: X: {2} Y: {3}" +
        "Координаты точки C: X: {4} Y: {5} \nКоординаты точки D: X: {6} Y: {7} \nПериметр прямоугольника: {8}",
        this.A.X, this.A.Y, this.B.X, this.B.Y, this.C.X, this.C.Y, this.D.X, this.D.Y, this.Length));
    }

    public Rectangle(Dot a, Dot b, Dot c, Dot d)
    {
      this.A = a;
      this.B = b;
      this.C = c;
      this.D = d;
      var height = new Line(a, b);
      var width = new Line(b, c);
      this.Length = 2 * (height.Lenght + width.Lenght);
    }
  }
}
