namespace Pacman.Business.Models
{
    public abstract class GameObject
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Size { get; set; }

        protected GameObject(int x, int y, int size)
        {
            X = x;
            Y = y;
            Size = size;
        }
    }
}