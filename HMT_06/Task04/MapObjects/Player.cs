namespace Task04.MapObjects
{
    public class Player : MapObject
    {
        public Player()
        {
            this.IsMovable = true;
        }

        public int Health { get; set; }

        public int Stamina { get; set; }

        public int Coins { get; set; }

        public bool IsAlive { get; set; }
    }
}
