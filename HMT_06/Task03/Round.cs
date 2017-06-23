namespace Task03
{
    using System;

    public class Round : Circle, IFigure
    {
        public Round(Dot center, int radius) : base(center, radius)
        {
        }

        public double GetArea()
        {
            var r = Radius;
            return Math.PI * r * r;
        }

        public new void Draw()
        {
            string outputString = "Фигура: Круг. Координаты X: {0} Y: {1} \nРадиус: {2}. \nПлощадь круга: {3}";
            Console.WriteLine(string.Format(outputString, this.Center.X, this.Center.Y, this.Radius, this.GetArea()));
        }
    }
}
