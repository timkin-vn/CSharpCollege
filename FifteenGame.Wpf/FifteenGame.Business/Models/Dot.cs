namespace Pacman.Business.Models
{
    public class Dot : GameObject
    {
        public bool Collected { get; set; } = false;
        public bool IsEnergizer { get; set; }

        public Dot(int x, int y, int size, bool isEnergizer = false)
            : base(x, y, size)
        {
            IsEnergizer = isEnergizer;
        }
    }
}