using FifteenGame.Common.BusinessModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FifteenGame.Wpf.ViewModels
{
    public class CellViewModel : INotifyPropertyChanged
    {
        private bool _isRevealed;
        private bool _isFlagged;
        private string _imagePath;
        private bool _ismine { get; set; }

        public int Num { get; set; }

        public string Text => Num.ToString();



        public int Row { get; set; }

        public int Column { get; set; }

        public  CellViewModel()
        {

            _imagePath = UpdateImagePath();
        }
        public bool IsmIne
        {
            get => _ismine;
            set
            {
                if (_ismine != value)
                {
                    _ismine = value;
                    OnPropertyChanged(nameof(IsmIne));
                    ImagePath = UpdateImagePath();
                }
            }
        }
        public bool IsRevealed
        {
            get => _isRevealed;
            set
            {
                if (_isRevealed != value)
                {
                    _isRevealed = value;
                    OnPropertyChanged(nameof(IsRevealed));
                    ImagePath = UpdateImagePath();
                }
            }
        }

        public bool IsFlagged
        {
            get => _isFlagged;
            set
            {
                if (_isFlagged != value)
                {
                    _isFlagged = value;
                    OnPropertyChanged(nameof(IsFlagged));
                    ImagePath = UpdateImagePath();
                }
            }
        }

        public string ImagePath
        {
            get => _imagePath;
            private set
            {
                if (_imagePath != value)
                {
                    _imagePath = value;
                    OnPropertyChanged(nameof(ImagePath));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public string UpdateImagePath()
        {
            if (IsFlagged)
            {
                return "pack://application:,,,/ViewModels/CellImages/redFlag.png";
            }
            else if (IsRevealed)
            {
                if (IsmIne)
                {
                    return "pack://application:,,,/ViewModels/CellImages/Mine.png";
                }
                else
                {
                    return $"pack://application:,,,/ViewModels/CellImages/neightbort{Num}.png";
                }
            }
            else
            {
                return "pack://application:,,,/ViewModels/CellImages/freeCell.png";
            }
        }
    }
}
