using Nonogram.Common.BusinessModels;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media;

namespace Nonogram.Wpf.ViewModels
{
    public class CellViewModel : INotifyPropertyChanged
    {
        private int _row;
        private int _column;
        private CellState _state;
        private SolidColorBrush _backgroundColor = Brushes.White;
        private Visibility _crossVisibility = Visibility.Collapsed;

        public int Row
        {
            get => _row;
            set
            {
                _row = value;
                OnPropertyChanged(nameof(Row));
            }
        }

        public int Column
        {
            get => _column;
            set
            {
                _column = value;
                OnPropertyChanged(nameof(Column));
            }
        }

        public CellState State
        {
            get => _state;
            set
            {
                _state = value;
                OnPropertyChanged(nameof(State));
                UpdateVisuals();
            }
        }

        public SolidColorBrush BackgroundColor
        {
            get => _backgroundColor;
            set
            {
                _backgroundColor = value;
                OnPropertyChanged(nameof(BackgroundColor));
            }
        }

        public Visibility CrossVisibility
        {
            get => _crossVisibility;
            set
            {
                _crossVisibility = value;
                OnPropertyChanged(nameof(CrossVisibility));
            }
        }

        private bool _isInCompletedRow;
        public bool IsInCompletedRow
        {
            get => _isInCompletedRow;
            set
            {
                _isInCompletedRow = value;
                UpdateVisuals();
                OnPropertyChanged(nameof(IsInCompletedRow));
            }
        }

        private bool _isInCompletedColumn;
        public bool IsInCompletedColumn
        {
            get => _isInCompletedColumn;
            set
            {
                _isInCompletedColumn = value;
                UpdateVisuals();
                OnPropertyChanged(nameof(IsInCompletedColumn));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void UpdateVisuals()
        {
            if (State == CellState.Filled)
            {
                // ЗАПОЛНЕННАЯ КЛЕТКА
                CrossVisibility = Visibility.Collapsed;

                if (IsInCompletedRow || IsInCompletedColumn)
                {
                    // Ярко-синий для заполненных клеток в завершенных строках/столбцах
                    BackgroundColor = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF4169E1")); // Ярко-синий
                }
                else
                {
                    // Черный для обычных заполненных клеток
                    BackgroundColor = Brushes.Black;
                }
            }
            else if (State == CellState.Crossed)
            {
                // НЕПРАВИЛЬНАЯ КЛЕТКА - БЕЛЫЙ ФОН С КРАСНЫМ КРЕСТОМ
                BackgroundColor = Brushes.White;
                CrossVisibility = Visibility.Visible;
            }
            else // Empty
            {
                // ПУСТАЯ КЛЕТКА - всегда белая, независимо от завершенности строки/столбца
                BackgroundColor = Brushes.White;
                CrossVisibility = Visibility.Collapsed;
            }
        }

        public void UpdateCompletedStatus(bool isRowCompleted, bool isColumnCompleted)
        {
            IsInCompletedRow = isRowCompleted;
            IsInCompletedColumn = isColumnCompleted;
            // UpdateVisuals вызывается в сеттерах свойств
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}