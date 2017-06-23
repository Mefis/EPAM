namespace Task04.MapObjects.Obstacles
{
    public class Rock : Obstacle
    {
        public void Collide(MapObject movingObject)
        {
            if ((this.CoordinateX == movingObject.CoordinateX) && (this.CoordinateY == movingObject.CoordinateY))
            {
                movingObject.IsMovable = false;
            }
        }
    }
}
