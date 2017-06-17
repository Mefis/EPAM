namespace Task04.MapObjects.Monsters
{
  public class Wolf : Monster
  {
    public Wolf()
    {
      this.Speed = base.Speed * 2; 
    }
  }
}
