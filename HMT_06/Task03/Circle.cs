using System;

namespace Task03
{
  public class Circle : IFigure
  {
    public Dot Center { get; private set; }
    public int Radius { get; private set; }

    public double GetLenght()
    {
      return 2 * Math.PI * Radius;
    }

    public void Draw()
    {
      Console.WriteLine(string.Format("Фигура: Окружность. Координаты X: {0} Y: {1} \nРадиус: {2}. \nДлина окружности: {3}",
        this.Center.X, this.Center.Y, this.Radius, this.GetLenght()));
    }

    public Circle(Dot center, int radius)
    {
      this.Center = center;
      this.Radius = radius;//todo pn нет проверки на отрицательность
    }
  }
}
