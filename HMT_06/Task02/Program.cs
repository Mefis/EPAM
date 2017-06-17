using System;

namespace Task02
{
  /*
  Задание 2
  Создать  класс  Ring  (кольцо),  описываемое  координатами  центра, 
  внешним и внутренним радиусами, а также свойствами, 
  позволяющими узнать  площадь  кольца  и  суммарную  длину  внешней  и  внутренней границ кольца.
  Обеспечить нахождение класса в заведомо корректном состоянии.
  */
  public class Program
  {
    public static void Main()
    {
      Console.WriteLine("Введите координаты X и Y центра, внутренний и внешний радиусы кольца.");
      var centerX = Console.ReadLine();
      var centerY = Console.ReadLine();
      var innerRadius = Console.ReadLine();
      var outerRadius = Console.ReadLine();

      var ring = new Ring(centerX, centerY, innerRadius, outerRadius);

      Console.WriteLine(string.Format("Координаты X: {0} Y: {1} \nВнутренний радиус: {2}. \nВнешний радиус: {3}. \nПлощадь кольца:"
        + " {4} \nДлинна внутренней окружности: {5} \nДлинна внешней окружности {6}",
        ring.CenterX, ring.CenterY, ring.InnerRadius, ring.OuterRadius, ring.RingArea, ring.InnerCircumference, ring.OuterCircumference));
    }
  }
}
