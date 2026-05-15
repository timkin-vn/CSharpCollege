using FifteenGame.Business.Models;

namespace FifteenGame.Wpf.ViewModels
{
    public class SnakeCellViewModel : ViewModelBase
    {
        private string _text;
        private bool _isHead;

        public SnakePoint Position { get; set; }
        public CellType Type { get; set; }

        public string Text
        {
            get => _text;
            set { _text = value; OnPropertyChanged(nameof(Text)); }
        }

        public bool IsHead
        {
            get => _isHead;
            set { _isHead = value; OnPropertyChanged(nameof(IsHead)); }
        }

        public int Row => Position.Y;
        public int Column => Position.X;
    }

    public enum CellType
    {
        Empty,
        Snake,
        Food
    }
}