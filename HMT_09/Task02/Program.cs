namespace Task02
{
    using System;

    // Задание 2
    // Напишите расширяющий метод, который определяет, является ли строка положительным целым числом.
    // Методы Parse и TryParse не использовать.

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
            Console.WriteLine("Введите строку.");
            var inputString = Console.ReadLine();

            while (string.IsNullOrEmpty(inputString))
            {
                Console.WriteLine("Строка не должна быть пустой.");
                inputString = Console.ReadLine();
            }

            string outputString = "Строка {0}является целым положительным числом.";
            if (inputString.IsPositiveInteger())
            {
                Console.WriteLine(string.Format(outputString, string.Empty));
            }
            else
            {
                Console.WriteLine(outputString, "не ");
            }

            Console.ReadKey();
        }

        /// <summary>
        /// Determines whether the string is a positive integer.
        /// </summary>
        /// <param name="inputString">Passes input string value.</param>
        /// <returns>Boolean value depending on the result of positive integer check.</returns>
        private static bool IsPositiveInteger(this string inputString)
        {
            var isPositiveInteger = false;
            try
            {
                int integerValue = Convert.ToInt32(inputString);//todo pn всегда нужно ещё учитывать культуру, поскольку разделители дробной части мб разными в зависимости от языка машины
                if (integerValue > 0)
                {
                    isPositiveInteger = true;
                }
            }
            catch
            {
                isPositiveInteger = false;
            }

            return isPositiveInteger;
        }
    }
}
