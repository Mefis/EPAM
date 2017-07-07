namespace Task03
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Class working with arrays.
    /// </summary>
    public static class ArrayManager
    {
        /// <summary>
        /// Gets anonymous method for positive values.
        /// </summary>
        private static IsPositiveAnonymous isPositiveAnonymous = delegate (int elem) { return elem > 0; };

        /// <summary>
        /// Gets lambda expression for positive values.
        /// </summary>
        private static IsPositiveLambda isPositiveLambda = x => x > 0;

        /// <summary>
        /// Refers to a static method that checks for positive integer values.
        /// </summary>
        private static Predicate<int> isPositive = IsPositive;

        /// <summary>
        /// Refers to a static method that works with integer values.
        /// </summary>
        /// <param name="elem">Passes integer value.</param>
        /// <returns>Boolean value depending on the result of check.</returns>
        private delegate bool IsPositiveAnonymous(int elem);

        /// <summary>
        /// Refers to a static method that works with integer values.
        /// </summary>
        /// <param name="elem">Passes integer value.</param>
        /// <returns>Boolean value depending on the result of check.</returns>
        private delegate bool IsPositiveLambda(int elem);

        /// <summary>
        /// Find all positive array values.
        /// </summary>
        /// <param name="array">Passes integer array.</param>
        /// <returns>Array of all positive values.</returns>
        public static List<int> GetPositiveElements_01(List<int> array)
        {
            List<int> positiveIntegerArray = new List<int>();
            foreach (int elem in array)
            {
                if (elem > 0)
                {
                    positiveIntegerArray.Add(elem);
                }
            }

            return positiveIntegerArray;
        }

        /// <summary>
        /// Find all positive array values using delegate with method.
        /// </summary>
        /// <param name="array">Passes integer array.</param>
        /// <returns>Array of all positive values.</returns>
        public static List<int> GetPositiveElements_02(List<int> array)//todo pn не совсем верно понял задание, "передается", значит в качестве входного параметра, а не в качестве глобальной переменной (здесь и везде ниже).
        {
            List<int> positiveIntegerArray = new List<int>();
            foreach (int elem in array)
            {
                if (isPositive.Invoke(elem))
                {
                    positiveIntegerArray.Add(elem);
                }
            }

            return positiveIntegerArray;
        }

        /// <summary>
        /// Find all positive array values using delegate with anonymous method.
        /// </summary>
        /// <param name="array">Passes integer array.</param>
        /// <returns>Array of all positive values.</returns>
        public static List<int> GetPositiveElements_03(List<int> array)
        {
            List<int> positiveIntegerArray = new List<int>();
            foreach (int elem in array)
            {
                if (isPositiveAnonymous.Invoke(elem))
                {
                    positiveIntegerArray.Add(elem);
                }
            }

            return positiveIntegerArray;
        }

        /// <summary>
        /// Find all positive array values using delegate with lambda expression.
        /// </summary>
        /// <param name="array">Passes integer array.</param>
        /// <returns>Array of all positive values.</returns>
        public static List<int> GetPositiveElements_04(List<int> array)
        {
            List<int> positiveIntegerArray = new List<int>();
            foreach (int elem in array)
            {
                if (isPositiveLambda.Invoke(elem))
                {
                    positiveIntegerArray.Add(elem);
                }
            }

            return positiveIntegerArray;
        }

        /// <summary>
        /// Find all positive array values using LINQ.
        /// </summary>
        /// <param name="array">Passes integer array.</param>
        /// <returns>Array of all positive values.</returns>
        public static List<int> GetPositiveElements_05(IEnumerable<int> array)
        {
            var positiveIntegerArray = from item in array
                                       where item > 0
                                       select item;
            return positiveIntegerArray.ToList();
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
