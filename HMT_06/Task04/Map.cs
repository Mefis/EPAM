namespace Task04
{
  public class Map
  {
    public int Height { get; private set; }
    public int Width { get; private set; }

    public Map(int height, int width)
    {
      this.Height = height;
      this.Width = width;
    }
  }
}
