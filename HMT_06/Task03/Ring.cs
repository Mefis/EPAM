namespace Task03
{
    using System;

    public class Ring : IFigure
    {
        public Ring(Dot center, Round innerRound, Round outerRound)
        {
            this.Center = center;
            if (0 < innerRound.Radius && innerRound.Radius < outerRound.Radius)
            {
                this.InnerRound = innerRound;
                this.OuterRound = outerRound;
            }
            else
            {
                this.InnerRound = default(Round);
                this.OuterRound = default(Round);
            }
        }

        public Dot Center { get; private set; }

        public Round InnerRound { get; private set; }

        public Round OuterRound { get; private set; }

        public double GetArea()
        {
            return this.OuterRound.GetArea() - this.InnerRound.GetArea();
        }

        public void Draw()
        {
            string outputString = "Фигура: Кольцо. Координаты X: {0} Y: {1} \nВнутренний радиус: {2}. \nВнешний радиус: {3}. \nПлощадь кольца: {4}";
            Console.WriteLine(string.Format(outputString, this.Center.X, this.Center.Y, this.InnerRound.Radius, this.OuterRound.Radius, this.GetArea()));
        }
    }
}
