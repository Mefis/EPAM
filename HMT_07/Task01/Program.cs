namespace Task01
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    // Задание 1
    // В кругу стоят N человек, пронумерованных от 1 до N.
    // При ведении счета по кругу вычёркивается каждый второй человек, пока не останется один.
    // Составить программу, моделирующую процесс.

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
            Console.WriteLine("Введите количество человек в кругу.");
            var peopleNumber = ValidateInput(Console.ReadLine());

            var peopleList = new List<int>();
            
            for (int i = 0; i < peopleNumber; i++)
            {
                peopleList.Add(i + 1);
            }

            while (peopleList.Count > 1)
            {
                peopleList = peopleList.Where((x, i) => i % 2 == 0).ToList();
                foreach (int i in peopleList)
                {
                    Console.Write("{0} ", i);
                }

                Console.WriteLine();
            }

            Console.ReadKey();
        }

        /// <summary>
        /// Validates input string and returns integer value.
        /// </summary>
        /// <param name="inputString">Input string.</param>
        /// <returns>Correct Integer value.</returns>
        private static int ValidateInput(string inputString)
        {
            int peopleNumber;
            while (!int.TryParse(inputString, out peopleNumber))
            {
                Console.WriteLine("Введено некорректное значение. Повторите попытку.");
                inputString = Console.ReadLine();
            }

            return peopleNumber;
        }
    }
}
