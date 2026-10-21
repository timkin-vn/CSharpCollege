
using FifteenGame.Business.Models;

namespace FifteenGame.Wpf.ViewModels
{
    public class CellViewModel : ViewModelBase
    {
        public int Value { get; set; }

        public string Text =>
            Value == 0 ? string.Empty : Value.ToString();

        public int Row { get; set; }

        public int Column { get; set; }

        public MoveDirection Direction { get; set; }
    }
}
