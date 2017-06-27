namespace Taks01
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    // Задание 1
    // Написать программу, выполняющую сортировку массива строк по возрастанию длины.
    // Если строки состоят из равного числа символов, их следует отсортировать по алфавиту.
    // Реализовать метод сравнения строк отдельным методом, передаваемым в сортировку через делегат.

    /// <summary>
    /// Main Task01 class.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// Refers to a static method that works with strings.
        /// </summary>
        /// <param name="a">Passes first string.</param>
        /// <param name="b">Passes second string.</param>
        /// <returns>Integer value.</returns>
        private delegate int Function(string a, string b);

        /// <summary>
        /// Main Task01 method.
        /// </summary>
        public static void Main()
        {
            Console.WriteLine("Введите массив строк. (Разделителем служит пробел)");
            string arrayString = Console.ReadLine();
            while (string.IsNullOrEmpty(arrayString))
            {
                Console.WriteLine("Массив не должен быть пустым.");
                arrayString = Console.ReadLine();
            }
            
            string[] separator = { " " };
            List<string> array = arrayString.Split(separator, StringSplitOptions.RemoveEmptyEntries).ToList();

            SortArray(array);

            Console.WriteLine("Отсортированный массив:");
            foreach (string elem in array)
            {
                Console.Write("{0} ", elem);
            }

            Console.WriteLine();
            Console.ReadKey();
        }

        /// <summary>
        /// Sort string array by length and alphabetically.
        /// </summary>
        /// <param name="array">Passes string array.</param>
        private static void SortArray(List<string> array)
        {
            Function function = new Function(CompareStrings);
            array.Sort((x, y) => function.Invoke(x, y));
        }

        /// <summary>
        /// Compares two strings by length and alphabetically.
        /// </summary>
        /// <param name="a">Passes first string.</param>
        /// <param name="b">Passes second string.</param>
        /// <returns>New strings position in integer.</returns>
        private static int CompareStrings(string a, string b)
        {
            if (a.Length < b.Length)
            {
                return -1;
            }
            else if (a.Length > b.Length)
            {
                return 1;
            }
            else
            {
                return string.Compare(a, b);
            }
        }
    }
}
