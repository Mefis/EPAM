namespace Task01
{
    using System;

    /// <summary>
    /// Base user class.
    /// </summary>
    public class User
    {
        /// <summary>
        /// Initializes a new instance of the User class.
        /// </summary>
        /// <param name="lastName">Passes user last name value.</param>
        /// <param name="name">Passes user name value.</param>
        /// <param name="patronymic">Passes user patronymic value.</param>
        /// <param name="dateOfBirth">Passes user date of birth value.</param>
        /// <param name="age">Passes user age value.</param>
        public User(string lastName, string name, string patronymic, DateTime dateOfBirth, int age)
        {
            this.LastName = lastName;
            this.Name = name;
            this.Patronymic = patronymic;
            this.DateOfBirth = dateOfBirth;
            this.Age = age;
        }

        /// <summary>
        /// Gets user last name.
        /// </summary>
        public string LastName { get; private set; }

        /// <summary>
        /// Gets user name.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets user patronymic 
        /// </summary>
        public string Patronymic { get; private set; }

        /// <summary>
        /// Gets user date of birth.
        /// </summary>
        public DateTime DateOfBirth { get; private set; }

        /// <summary>
        /// Gets user age.
        /// </summary>
        public int Age { get; private set; }
    }
}
