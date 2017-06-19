namespace Task01
{
  /// <summary>
  /// Класс сотрудник.
  /// </summary>
  public class Employee : User
  {
    /// <summary>
    /// Должность.
    /// </summary>
    public string Post { get; private set; }

    /// <summary>
    /// Стаж работы.
    /// </summary>
    public int Experience { get; private set; }

    public Employee(string lastName, string name, string patronymic, string dateOfBirth, string post, string experience) : base(lastName, name, patronymic, dateOfBirth)//todo pn опять же experience хочется видеть int, а не string
		{
      this.Post = post;

      int exp;
      if (int.TryParse(experience, out exp) && exp < base.Age)
      {
        this.Experience = exp;
      }
      else
      {
        this.Experience = default(int);
      }
    }
  }
}
