using Minesweeper.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Minesweeper.Wpf.ViewModels
{
    public class CellViewModel : ViewModelBase
    {
        private CellModel _model;
        private string _displayText;
        private SolidColorBrush _backgroundColor;
        private SolidColorBrush _foregroundColor;

        public int Row => _model.Row;
        public int Column => _model.Column;
        public bool IsMine => _model.IsMine;
        public bool IsRevealed => _model.IsRevealed;
        public bool IsFlagged => _model.IsFlagged;

        public string DisplayText
        {
            get => _displayText;
            set => SetProperty(ref _displayText, value, nameof(DisplayText));
        }

        public SolidColorBrush BackgroundColor
        {
            get => _backgroundColor;
            set => SetProperty(ref _backgroundColor, value, nameof(BackgroundColor));
        }

        public SolidColorBrush ForegroundColor
        {
            get => _foregroundColor;
            set => SetProperty(ref _foregroundColor, value, nameof(ForegroundColor));
        }

        public CellViewModel(CellModel model)
        {
            _model = model;
            UpdateDisplay();
        }

        public void UpdateDisplay()
        {
            if (_model.IsRevealed)
            {
                if (_model.IsMine)
                {
                    DisplayText = "💣";
                    BackgroundColor = new SolidColorBrush(Colors.Red);
                    ForegroundColor = new SolidColorBrush(Colors.Black);
                }
                else if (_model.AdjacentMinesCount > 0)
                {
                    DisplayText = _model.AdjacentMinesCount.ToString();
                    BackgroundColor = new SolidColorBrush(Colors.LightGray);
                    ForegroundColor = GetNumberColor(_model.AdjacentMinesCount);
                }
                else
                {
                    DisplayText = "";
                    BackgroundColor = new SolidColorBrush(Colors.LightGray);
                    ForegroundColor = new SolidColorBrush(Colors.Transparent);
                }
            }
            else
            {
                if (_model.IsFlagged)
                {
                    DisplayText = "🚩";
                    BackgroundColor = new SolidColorBrush(Colors.LightBlue);
                    ForegroundColor = new SolidColorBrush(Colors.Red);
                }
                else
                {
                    DisplayText = "";
                    BackgroundColor = new SolidColorBrush(Colors.LightBlue);
                    ForegroundColor = new SolidColorBrush(Colors.Transparent);
                }
            }

            OnPropertyChanged(nameof(IsRevealed));
            OnPropertyChanged(nameof(IsFlagged));
        }

        private SolidColorBrush GetNumberColor(int count)
        {
            switch (count)
            {
                case 1: return new SolidColorBrush(Colors.Blue);
                case 2: return new SolidColorBrush(Colors.Green);
                case 3: return new SolidColorBrush(Colors.Red);
                case 4: return new SolidColorBrush(Colors.DarkBlue);
                case 5: return new SolidColorBrush(Colors.Brown);
                case 6: return new SolidColorBrush(Colors.Cyan);
                case 7: return new SolidColorBrush(Colors.Black);
                case 8: return new SolidColorBrush(Colors.Gray);
                default: return new SolidColorBrush(Colors.Black);
            }
        }

        public void Refresh()
        {
            UpdateDisplay();
        }
    }
}