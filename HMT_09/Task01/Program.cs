namespace Task01
{
    using System;

    // Задание 1
    // Напишите расширяющий метод, который определяет сумму элементов массива.
        
    /// <summary>
    /// Main Task01 class.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// Main Task01 method.
        /// </summary>
        public static void Main()
        {
            Console.WriteLine("Введите массив целых чисел. (Разделителем служит пробел)");
            string arrayString = Console.ReadLine();
            while (string.IsNullOrEmpty(arrayString))
            {
                Console.WriteLine("Массив не должен быть пустым.");
                arrayString = Console.ReadLine();
            }

            string[] separator = { " " };
            var array = arrayString.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            double sum = 0;

            if (array.TrySum(out sum))
            {
                Console.WriteLine(string.Format("Сумма элементов массива: {0}.", sum));
            }

            Console.ReadKey();
        }

        /// <summary>
        /// Tries to find the sum of array elements.
        /// </summary>
        /// <typeparam name="T">Generic type.</typeparam>
        /// <param name="array">Passes array.</param>
        /// <param name="sum">Passes sum value.</param>
        /// <returns>Boolean value depending on the success of finding the sum.</returns>
        private static bool TrySum<T>(this T[] array, out double sum)
        {
            double tempSum = 0;
            double tempElem = 0;
            var arrayIsValid = false;

            for (int i = 0; i < array.Length; i++)
            {
                if (!double.TryParse(array[i].ToString(), out tempElem))
                {
                    Console.WriteLine("Введены некорректные значения.");
                    tempSum = default(double);
                    arrayIsValid = false;
                    break;
                }

                tempSum += tempElem;
                arrayIsValid = true;
            }

            sum = tempSum;
            return arrayIsValid;
        }
    }
}
