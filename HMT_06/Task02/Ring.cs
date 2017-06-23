namespace Task02
{
    using System;

    /// <summary>
    /// Ring class.
    /// </summary>
    public class Ring
    {
        /// <summary>
        /// Initializes a new instance of the Ring class.
        /// </summary>
        /// <param name="centerX">Passes ring center X coordinate.</param>
        /// <param name="centerY">Passes ring center Y coordinate</param>
        /// <param name="innerRadius">Passes ring inner radius.</param>
        /// <param name="outerRadius">Passes ring outer radius.</param>
        public Ring(int centerX, int centerY, int innerRadius, int outerRadius)
        {
            if (innerRadius < outerRadius && innerRadius > 0)
            {
                var pi = Math.PI;
                this.InnerRadius = innerRadius;
                this.OuterRadius = outerRadius;
                this.RingArea = pi * (Math.Pow(outerRadius, 2) - Math.Pow(innerRadius, 2));
                this.InnerCircumference = 2 * pi * innerRadius;
                this.OuterCircumference = 2 * pi * outerRadius;
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

        /// <summary>
        /// Gets ring center X coordinate.
        /// </summary>
        public int CenterX { get; private set; }

        /// <summary>
        /// Gets ring center Y coordinate.
        /// </summary>
        public int CenterY { get; private set; }

        /// <summary>
        /// Gets ring inner radius.
        /// </summary>
        public int InnerRadius { get; private set; }

        /// <summary>
        /// Gets ring outer radius.
        /// </summary>
        public int OuterRadius { get; private set; }

        /// <summary>
        /// Gets ring area.
        /// </summary>
        public double RingArea { get; private set; }

        /// <summary>
        /// Gets inner ring circumference.
        /// </summary>
        public double InnerCircumference { get; private set; }

        /// <summary>
        /// Gets outer ring circumference.
        /// </summary>
        public double OuterCircumference { get; private set; }
    }
}
