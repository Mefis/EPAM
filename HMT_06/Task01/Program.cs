namespace Task01
{
    using System;
    using System.Globalization;

    /*
    Задание 1
    На основе класса User, создать класс Employee, описывающий  сотрудника фирмы.
    В  дополнение к полям пользователя  добавить поля  «стаж работы»  и  «должность». 
    Обеспечить нахождение класса в заведомо корректном состоянии.
    */

    /// <summary>
    /// Main Task01 class. 
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Stores date format in string.
        /// </summary>
        public const string DateFormat = "dd.MM.yyyy";

        /// <summary>
        /// Main Task01 method.
        /// </summary>
        public static void Main()
        {
            Console.WriteLine("Введите Ф.И.О., дату рождения, должность и опыт работы.");
            var lastName = Console.ReadLine();
            var name = Console.ReadLine();
            var patronymic = Console.ReadLine();

            var dateOfBirth = default(DateTime);
            var userAge = default(int);
            var dateOfBirthInput = Console.ReadLine();
            GetAge(dateOfBirthInput, ref dateOfBirth, ref userAge);

            var post = Console.ReadLine();
            var experience = Console.ReadLine();
            var experienceResult = GetExperience(experience, userAge);

            var user = new Employee(lastName, name, patronymic, dateOfBirth, userAge, post, experienceResult);

            string outputString = "Пользователь: {0} {1} {2}. \nДата рождения: {3}. \nВозраст: {4} лет. \nДолжность: {5}. \nОпыт работы: {6} лет.";
            Console.WriteLine(string.Format(outputString, user.LastName, user.Name, user.Patronymic, user.DateOfBirth.Date.ToString("dd.MM.yyyy"), user.Age, user.Post, user.Experience));
        }

        /// <summary>
        /// Gets user's age.
        /// </summary>
        /// <param name="dateOfBirthInput">User's date of birth in String.</param>
        /// <param name="dateOfBirth">User's date of birth in DateTime.</param>
        /// <param name="userAge">User's age.</param>
        private static void GetAge(string dateOfBirthInput, ref DateTime dateOfBirth, ref int userAge)
        {
            if (DateTime.TryParseExact(dateOfBirthInput, DateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out dateOfBirth) && (DateTime.Now > dateOfBirth))
            {
                DateTime now = DateTime.Today;
                int age = now.Year - dateOfBirth.Year;
                if (dateOfBirth > now.AddYears(-age))
                {
                    age--;
                }

                userAge = age;
            }
        }

        /// <summary>
        /// Validates experience value.
        /// </summary>
        /// <param name="experience">Experience value in string.</param>
        /// <param name="userAge">Age of the employee.</param>
        /// <returns>Experience value in integer.</returns>
        private static int GetExperience(string experience, int userAge)
        {
            int exp;
            if (int.TryParse(experience, out exp) && exp < userAge)
            {
                return exp;
            }
            else
            {
                return default(int);
            }
        }
    }
}
