namespace Task03
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;

    // Задание 3
    // Написать методы поиска элемента в массиве(например, поиск всех положительных элементов в массиве) в виде: 
    // 1. Метода, реализующего поиск напрямую; 
    // 2. Метода, которому условие поиска передаётся через делегат; 
    // 3. Метода, которому условие поиска передаётся через делегат в виде анонимного метода; 
    // 4. Метода, которому условие поиска передаётся через делегат в виде лямбда-выражения; 
    // 5. LINQ-выражения
    // Сравнить скорость выполнения вычислений.

    /// <summary>
    /// Main Task03 class.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// Random numbers generator.
        /// </summary>
        private static Random random = new Random((int)DateTime.Now.Ticks);

        /// <summary>
        /// Refers to a static method that checks for positive integer values.
        /// </summary>
        private static Predicate<int> isPositive = IsPositive;

        /// <summary>
        /// Gets anonymous method for positive values.
        /// </summary>
        private static IsPositiveAnonymous isPositiveAnonymous = delegate (int elem) { return elem > 0; };

        /// <summary>
        /// Gets lambda expression for positive values.
        /// </summary>
        private static IsPositiveLambda isPositiveLambda = x => x > 0;

        /// <summary>
        /// Refers to a static method that works with integer values.
        /// </summary>
        /// <param name="elem">Passes integer value.</param>
        /// <returns>Boolean value depending on the result of check.</returns>
        public delegate bool IsPositiveAnonymous(int elem);

        /// <summary>
        /// Refers to a static method that works with integer values.
        /// </summary>
        /// <param name="elem">Passes integer value.</param>
        /// <returns>Boolean value depending on the result of check.</returns>
        public delegate bool IsPositiveLambda(int elem);

        /// <summary>
        /// Main Task03 method.
        /// </summary>
        public static void Main()
        {
            List<int> integerArray = new List<int>();
            int intMinValue = int.MinValue;
            int intMaxValue = int.MaxValue;
            for (int i = 0; i < 1000; i++)
            {
                integerArray.Add(random.Next(intMinValue, intMaxValue));
            }

            Stopwatch stopWatch = new Stopwatch();

            stopWatch.Start();
            var positiveIntegerArray = ArrayManager.GetPositiveElements_01(integerArray);
            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;
            Console.WriteLine(string.Format("Метод №1: {0}", ts));

            stopWatch.Start();
            positiveIntegerArray = ArrayManager.GetPositiveElements_02(integerArray, isPositive);
            stopWatch.Stop();
            ts = stopWatch.Elapsed;
            Console.WriteLine(string.Format("Метод №2: {0}", ts));

            stopWatch.Start();
            positiveIntegerArray = ArrayManager.GetPositiveElements_03(integerArray, isPositiveAnonymous);
            stopWatch.Stop();
            ts = stopWatch.Elapsed;
            Console.WriteLine(string.Format("Метод №3: {0}", ts));

            stopWatch.Start();
            positiveIntegerArray = ArrayManager.GetPositiveElements_04(integerArray, isPositiveLambda);
            stopWatch.Stop();
            ts = stopWatch.Elapsed;
            Console.WriteLine(string.Format("Метод №4: {0}", ts));

            stopWatch.Start();
            positiveIntegerArray = ArrayManager.GetPositiveElements_05(integerArray);
            stopWatch.Stop();
            ts = stopWatch.Elapsed;
            Console.WriteLine(string.Format("Метод №5: {0}", ts));

            Console.ReadKey();
        }

        /// <summary>
        /// Checks whether the value is positive.
        /// </summary>
        /// <param name="elem">Passes integer value.</param>
        /// <returns>Boolean value depending on the result of positive value check.</returns>
        private static bool IsPositive(int elem)
        {
            return elem > 0;
        }
    }
}
