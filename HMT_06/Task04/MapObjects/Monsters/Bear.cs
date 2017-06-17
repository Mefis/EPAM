namespace Task04.MapObjects.Monsters
{
  public class Bear : Monster
  {
    public Bear()
    {
      this.Attack = base.Attack * 2;
    }
  }
}
