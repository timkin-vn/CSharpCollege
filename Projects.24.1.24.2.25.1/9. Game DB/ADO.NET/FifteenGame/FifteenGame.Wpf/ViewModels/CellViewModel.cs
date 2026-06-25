using FifteenGame.Business.Models;

namespace FifteenGame.Wpf.ViewModels
{
    public class CellViewModel : ViewModelBase
    {
        private CellType _type;
        private string _displayText;
        private string _backgroundColor;
        private bool _isVisible;

        public int Row { get; set; }
        public int Column { get; set; }

        public CellType Type
        {
            get => _type;
            set
            {
                _type = value;
                OnPropertyChanged(nameof(Type));
                UpdateAppearance();
            }
        }

        public string DisplayText
        {
            get => _displayText;
            set { _displayText = value; OnPropertyChanged(nameof(DisplayText)); }
        }

        public string BackgroundColor
        {
            get => _backgroundColor;
            set { _backgroundColor = value; OnPropertyChanged(nameof(BackgroundColor)); }
        }

        public bool IsVisible
        {
            get => _isVisible;
            set { _isVisible = value; OnPropertyChanged(nameof(IsVisible)); }
        }

        public CellViewModel(int row, int col, CellType type)
        {
            Row = row;
            Column = col;
            Type = type;
            IsVisible = true;
            UpdateAppearance();
        }

        private void UpdateAppearance()
        {
            switch (Type)
            {
                case CellType.Wall:
                    DisplayText = "";
                    BackgroundColor = "#555555";
                    break;
                case CellType.Floor:
                    DisplayText = "";
                    BackgroundColor = "#EEEEEE";
                    break;
                case CellType.Key:
                    DisplayText = "🔑";
                    BackgroundColor = "#FFFF99";
                    break;
                case CellType.Door:
                    DisplayText = "🚪";
                    BackgroundColor = "#CC9966";
                    break;
                case CellType.Exit:
                    DisplayText = "🚪";
                    BackgroundColor = "#66FF66";
                    break;
                case CellType.Switch:
                    DisplayText = "⚡";
                    BackgroundColor = "#FFAA66";
                    break;
                default:
                    DisplayText = "";
                    BackgroundColor = "#FFFFFF";
                    break;
            }
        }
    }
}