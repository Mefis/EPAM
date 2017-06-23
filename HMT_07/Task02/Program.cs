namespace Task02
{
    using System;
    using System.Collections.Generic;

    // Задание 2
    // Задан английский текст. Выделить отдельные слова и для каждого посчитать частоту встречаемости.
    // Слова, отличающиеся регистром, считать одинаковыми. В качестве разделителей считать пробел и точку.

    /// <summary>
    /// Main Task02 class.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// Main Task02 method.
        /// </summary>
        public static void Main()
        {
            string inputString = string.Empty;
            while (string.IsNullOrEmpty(inputString))
            {
                Console.WriteLine("Введите текст для проверки.");
                inputString = Console.ReadLine();
            }

            string[] separators = { ".", " " };
            string[] words = inputString.Split(separators, StringSplitOptions.RemoveEmptyEntries);
            Dictionary<string, int> wordsCount = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
            foreach (var word in words)
            {
                if (wordsCount.ContainsKey(word))
                {
                    wordsCount[word]++;
                }
                else
                {
                    wordsCount.Add(word, 1);
                }
            }

            foreach (var word in wordsCount)
            {
                Console.WriteLine("{0}: {1}", word.Key, word.Value);
            }

            Console.ReadKey();
        }
    }
}
