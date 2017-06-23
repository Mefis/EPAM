namespace Task04.MapObjects.Monsters
{
    public class Wolf : Monster
    {
        public Wolf()
        {
            Wolf.Speed = Monster.Speed * Settings.SpeedBonus;
        }
    }
}
