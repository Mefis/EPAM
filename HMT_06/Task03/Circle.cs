namespace Task03
{
    using System;

    public class Circle : IFigure
    {
        public Circle(Dot center, int radius)
        {
            this.Center = center;
            this.Radius = radius > 0 ? radius : default(int);
        }

        public Dot Center { get; private set; }

        public int Radius { get; private set; }

        public double GetLenght()
        {
            return 2 * Math.PI * this.Radius;
        }

        public void Draw()
        {
            string outputString = "Фигура: Окружность. Координаты X: {0} Y: {1} \nРадиус: {2}. \nДлина окружности: {3}";
            Console.WriteLine(string.Format(outputString, this.Center.X, this.Center.Y, this.Radius, this.GetLenght()));
        }
    }
}
