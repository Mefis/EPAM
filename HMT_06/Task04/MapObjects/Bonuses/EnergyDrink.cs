namespace Task04.MapObjects.Bonuses
{
    public class EnergyDrink : Bonus
    {
        public void AddStamina(Player player)
        {
            player.Stamina++;
        }
    }
}
