using System;

namespace Task03
{
  public class Round : Circle, IFigure
  {
    public double GetArea()
    {
      var r = base.Radius;
      return Math.PI * r * r; 
    }

    new public void Draw()
    {
      Console.WriteLine(string.Format("Фигура: Круг. Координаты X: {0} Y: {1} \nРадиус: {2}. \nПлощадь круга: {3}",
        this.Center.X, this.Center.Y, this.Radius, this.GetArea()));
    }

    public Round(Dot center, int radius) : base(center, radius)
    {
    }
  }
}
