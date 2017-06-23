namespace Task04.MapObjects.Bonuses
{
    public class MedKit : Bonus
    {
        public void AddHealth(Player player)
        {
            player.Health++;
        }
    }
}
