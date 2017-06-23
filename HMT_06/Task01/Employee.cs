namespace Task01
{
    using System;

    /// <summary>
    /// Employee class.
    /// </summary>
    public class Employee : User
    {
        /// <summary>
        /// Initializes a new instance of the Employee class.
        /// </summary>
        /// <param name="lastName">Passes employee last name value.</param>
        /// <param name="name">Passes employee name value.</param>
        /// <param name="patronymic">Passes employee patronymic value.</param>
        /// <param name="dateOfBirth">Passes employee date of birth value.</param>
        /// <param name="age">Passes employee age value.</param>
        /// <param name="post">Passes employee post value.</param>
        /// <param name="experience">Passes employee experience value.</param>
        public Employee(string lastName, string name, string patronymic, DateTime dateOfBirth, int age, string post, int experience) : base(lastName, name, patronymic, dateOfBirth, age)
        {
            this.Post = post;
            this.Experience = experience;
        }

        /// <summary>
        /// Gets employee's post.
        /// </summary>
        public string Post { get; private set; }

        /// <summary>
        /// Gets employee's experience.
        /// </summary>
        public int Experience { get; private set; }
    }
}
