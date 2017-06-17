namespace Task04.MapObjects.Bonuses
{
  public class Coin : Bonus
  {
    public void AddCoin(Player player)
    {
      player.Coins++;
    }
  }
}
