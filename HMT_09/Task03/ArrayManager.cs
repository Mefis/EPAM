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
        /// <param name="isPositive">Passes condition of positivity.</param>
        /// <returns>Array of all positive values.</returns>
        public static List<int> GetPositiveElements_02(List<int> array, Predicate<int> isPositive)
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
        /// <param name="isPositiveAnonymous">Passes condition of positivity.</param>
        /// <returns>Array of all positive values.</returns>
        public static List<int> GetPositiveElements_03(List<int> array, Program.IsPositiveAnonymous isPositiveAnonymous)
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
        /// <param name="isPositiveLambda">Passes condition of positivity.</param>
        /// <returns>Array of all positive values.</returns>
        public static List<int> GetPositiveElements_04(List<int> array, Program.IsPositiveLambda isPositiveLambda)
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
    }
}
