namespace Task04.MapObjects.Monsters
{
    public class Bear : Monster
    {
        public Bear()
        {
            Bear.Attack = Monster.Attack * Settings.MonsterAttackBonus;
        }
    }
}
