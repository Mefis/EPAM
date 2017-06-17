namespace Task04.MapObjects.Monsters
{
  public class Monster : MapObject
  {
    public int Health { get; set; }
    public int Speed { get; set; }
    public int Attack { get; set; }
    public bool IsAlive { get; set; }

    public Monster()
    {
      this.IsMovable = true;
    }
  }
}
