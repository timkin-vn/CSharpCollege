using System.Windows.Input;

namespace FifteenGame.Wpf.ViewModels
{
    public class CellViewModel : ViewModelBase
    {
        private string _text;
        public string Text
        {
            get => _text;
            set { _text = value; OnPropertyChanged(nameof(Text)); }
        }

        private bool _isWinningCell;
        public bool IsWinningCell
        {
            get => _isWinningCell;
            set { _isWinningCell = value; OnPropertyChanged(nameof(IsWinningCell)); }
        }

        public int Row { get; set; }
        public int Column { get; set; }

        public ICommand ClickCommand { get; set; }
    }
}