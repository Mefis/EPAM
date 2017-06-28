namespace Task02
{
    using System;

    // Задание 2
    // Написать программу, описывающую небольшой офис, в котором работают сотрудники – объекты класса Person, обладающие полем имя(Name). 
    // Каждый из сотрудников содержит пару методов: приветствие сотрудника, пришедшего на работу(принимает в качестве аргументов объект сотрудника и время его прихода) 
    // и прощание с ним(принимает только  объект сотрудника).
    // В зависимости от времени суток, приветствие может быть различным:
    // до 12 часов – «Доброе утро», с 12 до 17 – «Добрый день», начиная с 17 часов – «Добрый вечер».
    // Каждый раз при входе очередного сотрудника в офис, все пришедшие ранее его приветствуют.
    // При уходе сотрудника домой с ним также прощаются все присутствующие.
    // Вызов процедуры приветствия/прощания производить через групповые  делегаты.
    // Факт прихода и ухода сотрудника отслеживается через генерируемые им события.
    // Событие прихода описывается делегатом, передающим в числе параметров наследника EventArgs, явно содержащего поле с временем прихода.
    // Продемонстрировать работу офиса при последовательном приходе и уходе сотрудников.

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
            Person john = new Person { Name = "John" };
            Person paul = new Person { Name = "Paul" };
            Person george = new Person { Name = "George" };
            Person ringo = new Person { Name = "Ringo" };

            DateTime morningTime = new DateTime(2017, 6, 27, 10, 0, 0);
            DateTime afternoonTime = new DateTime(2017, 6, 27, 13, 0, 0);
            DateTime eveningTime = new DateTime(2017, 6, 27, 17, 0, 0);

            MessageSenderEventArgs eventArgs = new MessageSenderEventArgs(morningTime);
            
            john.Arrived(eventArgs);
            paul.Arrived(eventArgs);
            eventArgs = new MessageSenderEventArgs(afternoonTime);
            george.Arrived(eventArgs);
            eventArgs = new MessageSenderEventArgs(eveningTime);
            ringo.Arrived(eventArgs);

            john.Left();
            paul.Left();
            george.Left();
            ringo.Left();

            Console.ReadKey();
        }
    }
}
