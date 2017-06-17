using System;

namespace Task02
{
  /// <summary>
  /// Класс кольцо
  /// </summary>
  public class Ring
  {
    /// <summary>
    /// Координата X центра кольца.
    /// </summary>
    public int CenterX { get; private set; }

    /// <summary>
    /// Координата Y центра кольца.
    /// </summary>
    public int CenterY { get; private set; }

    /// <summary>
    /// Внутренний радиус.
    /// </summary>
    public int InnerRadius { get; private set; }

    /// <summary>
    /// Внешний радиус.
    /// </summary>
    public int OuterRadius { get; private set; }

    /// <summary>
    /// Площадь кольца.
    /// </summary>
    public double RingArea { get; private set; }

    /// <summary>
    /// Длинна внутренней окружности.
    /// </summary>
    public double InnerCircumference { get; private set; }

    /// <summary>
    /// Длинна внешней окружности.
    /// </summary>
    public double OuterCircumference { get; private set; }

    public Ring(string centerX, string centerY, string innerRadius, string outerRadius)
    {
      int x, y, inRad, outRad;
      if (int.TryParse(centerX, out x) && int.TryParse(centerY, out y))
      {
        this.CenterX = x;
        this.CenterY = y;
      }
      else
      {
        this.CenterX = default(int);
        this.CenterY = default(int);
      }
      if (int.TryParse(innerRadius, out inRad) && int.TryParse(outerRadius, out outRad) && inRad < outRad && inRad > 0)
      {
        var pi = Math.PI;
        this.InnerRadius = inRad;
        this.OuterRadius = outRad;
        this.RingArea = pi * (Math.Pow(outRad, 2) - Math.Pow(inRad, 2));
        this.InnerCircumference = 2 * pi * inRad;
        this.OuterCircumference = 2 * pi * outRad;
      }
      else
      {
        this.InnerRadius = default(int);
        this.OuterRadius = default(int);
        this.RingArea = default(double);
        this.InnerCircumference = default(double);
        this.OuterCircumference = default(double);
      }
    }
  }
}
