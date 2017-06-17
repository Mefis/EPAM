namespace Task04.MapObjects.Obstacles
{
  public class DeathTrap : Obstacle
  {
    public void Collide(Player player)
    {
      if ((this.CoordinateX == player.CoordinateX) && (this.CoordinateY == player.CoordinateY))
      {
        player.Health--;
      }
    }
  }
}
