namespace Task02
{
    using System;

    /*
    Задание 2
    Создать  класс  Ring  (кольцо),  описываемое  координатами  центра, 
    внешним и внутренним радиусами, а также свойствами, 
    позволяющими узнать  площадь  кольца  и  суммарную  длину  внешней  и  внутренней границ кольца.
    Обеспечить нахождение класса в заведомо корректном состоянии.
    */

    /// <summary>
    /// Main Task02 class.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Main Task02 method.
        /// </summary>
        public static void Main()
        {
            Console.WriteLine("Введите координаты X и Y центра, внутренний и внешний радиусы кольца.");
            var centerX = InputCheck(Console.ReadLine());
            var centerY = InputCheck(Console.ReadLine());
            var innerRadius = InputCheck(Console.ReadLine());
            var outerRadius = InputCheck(Console.ReadLine());

            var ring = new Ring(centerX, centerY, innerRadius, outerRadius);

            string outputString = "Координаты X: {0} Y: {1} \nВнутренний радиус: {2}. \nВнешний радиус: {3}. \nПлощадь кольца: {4} \nДлинна внутренней окружности: {5} \nДлинна внешней окружности {6}";
            Console.WriteLine(string.Format(outputString, ring.CenterX, ring.CenterY, ring.InnerRadius, ring.OuterRadius, ring.RingArea, ring.InnerCircumference, ring.OuterCircumference));
        }

        /// <summary>
        /// Validates input value.
        /// </summary>
        /// <param name="input">String value.</param>
        /// <returns>Integer value.</returns>
        private static int InputCheck(string input)
        {
            int n;
            if (int.TryParse(input, out n))
            {
                return n;
            }
            else
            {
                return default(int);
            }
        }
    }
}
