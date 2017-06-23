namespace Task04.MapObjects.Monsters
{
    public class Monster : MapObject
    {
        public Monster()
        {
            this.IsMovable = true;
        }

        public int Health { get; set; }

        public static int Speed { get; set; }

        public static int Attack { get; set; }

        public bool IsAlive { get; set; }
    }
}
