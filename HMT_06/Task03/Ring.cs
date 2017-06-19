using System;

namespace Task03
{
  public class Ring : IFigure
  {
    public Dot Center { get; private set; }
    public Round InnerRound { get; private set; }
    public Round OuterRound { get; private set; }

    public double GetArea()
    {
      return OuterRound.GetArea() - InnerRound.GetArea();
    }

    public void Draw()
    {
      Console.WriteLine(string.Format("Фигура: Кольцо. Координаты X: {0} Y: {1} \nВнутренний радиус: {2}. \nВнешний радиус: {3}. \nПлощадь кольца: {4}",
        this.Center.X, this.Center.Y, this.InnerRound.Radius, this.OuterRound.Radius, this.GetArea()));
    }

    public Ring(Dot center, Round innerRound, Round outerRound)
    {
		this.Center = center;
		this.InnerRound = innerRound;//todo pn нет проверок на корректность (0 < innerRound < outerRound)
		this.OuterRound = outerRound;
    }
  }
}
