﻿using System;
using System.Globalization;

namespace Task01
{
  /// <summary>
  /// Базовый класс пользователя.
  /// </summary>
  public class User
  {
    /// <summary>
    /// Фамилия.
    /// </summary>
    public string LastName { get; private set; }

    /// <summary>
    /// Имя.
    /// </summary>
    public string Name { get; private set; }

    /// <summary>
    /// Отчество.
    /// </summary>
    public string Patronymic { get; private set; }

    /// <summary>
    /// Дата рождения.
    /// </summary>
    public DateTime DateOfBirth { get; private set; }

    /// <summary>
    /// Возраст.
    /// </summary>
    public int Age { get; private set; }

    public User(string lastName, string name, string patronymic, string dateOfBirth)
    {
      this.LastName = lastName;
      this.Name = name;
      this.Patronymic = patronymic;

      DateTime dateResult;
      string[] formats = { "dd.MM.yyyy" };
      if (DateTime.TryParseExact(dateOfBirth, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out dateResult) && (DateTime.Now > dateResult))
      {
        this.DateOfBirth = dateResult;
        DateTime now = DateTime.Today;
        int age = now.Year - dateResult.Year;
        if (dateResult > now.AddYears(-age))
          age--;
        this.Age = age;
      }
      else
      {
        this.DateOfBirth = default(DateTime);
        this.Age = default(int);
      }
    }
  }
}