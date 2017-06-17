using System;

namespace Task01
{
  /*
  Задание 1
  На основе класса User, создать класс Employee, описывающий  сотрудника фирмы.
  В  дополнение к полям пользователя  добавить поля  «стаж работы»  и  «должность». 
  Обеспечить нахождение класса в заведомо корректном состоянии.
  */
  public class Program
  {
    public static void Main()
    {
      Console.WriteLine("Введите Ф.И.О., дату рождения, должность и опыт работы.");
      var lastName = Console.ReadLine();
      var name = Console.ReadLine();
      var patronymic = Console.ReadLine();
      var dateOfBirth = Console.ReadLine();
      var post = Console.ReadLine();
      var experience = Console.ReadLine();

      var user = new Employee(lastName, name, patronymic, dateOfBirth, post, experience);

      Console.WriteLine(string.Format("Пользователь: {0} {1} {2}. \nДата рождения: {3}. \nВозраст: {4} лет. \nДолжность: {5}. \nОпыт работы: {6} лет.",
        user.LastName, user.Name, user.Patronymic, user.DateOfBirth.Date.ToString("dd.MM.yyyy"), user.Age, user.Post, user.Experience));
    }
  }
}
