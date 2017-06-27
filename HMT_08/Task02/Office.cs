namespace Task02
{
    /// <summary>
    /// Base office class.
    /// </summary>
    public static class Office
    {
        /// <summary>
        /// Stores greeting messages.
        /// </summary>
        private static GreetMessage greetMessageList;

        /// <summary>
        /// Stores farewell messages.
        /// </summary>
        private static FarewellMessage farewellMessageList;

        /// <summary>
        /// Refers to a method that works with Person.
        /// </summary>
        /// <param name="person">Passes person value.</param>
        private delegate void GreetMessage(Person person);

        /// <summary>
        /// Refers to a method that works with Person.
        /// </summary>
        /// <param name="person">Passes person value.</param>
        private delegate void FarewellMessage(Person person);

        /// <summary>
        /// Adds person to the office.
        /// </summary>
        /// <param name="person">Passes person value.</param>
        public static void AddPerson(Person person)
        {
            greetMessageList += person.GreetPerson;
            farewellMessageList += person.FarewellPerson;
            if (greetMessageList != null)
            {
                greetMessageList.Invoke(person);
            }
        }

        /// <summary>
        /// Removes person from the office.
        /// </summary>
        /// <param name="person">Passes person value.</param>
        public static void RemovePerson(Person person)
        {
            greetMessageList -= person.GreetPerson;
            farewellMessageList -= person.FarewellPerson;
            if (farewellMessageList != null)
            {
                farewellMessageList.Invoke(person);
            }
        }
    }
}
