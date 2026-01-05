using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Media;

namespace Nonogram.Wpf.ViewModels
{
    public class RowClueViewModel : INotifyPropertyChanged
    {
        public int RowIndex { get; set; }
        public List<int> Clues { get; set; } = new List<int>();

        private bool _isCompleted;
        public bool IsCompleted
        {
            get => _isCompleted;
            set
            {
                _isCompleted = value;
                OnPropertyChanged(nameof(IsCompleted));
                OnPropertyChanged(nameof(BackgroundColor));
            }
        }

        public SolidColorBrush BackgroundColor
        {
            get
            {
                if (IsCompleted)
                    return (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFADD8E6")); // Светло-голубой
                return Brushes.LightGray;
            }
        }

        public string ClueText => string.Join(" ", Clues);

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class ColumnClueViewModel : INotifyPropertyChanged
    {
        public int ColumnIndex { get; set; }
        public List<int> Clues { get; set; } = new List<int>();

        private bool _isCompleted;
        public bool IsCompleted
        {
            get => _isCompleted;
            set
            {
                _isCompleted = value;
                OnPropertyChanged(nameof(IsCompleted));
                OnPropertyChanged(nameof(BackgroundColor));
            }
        }

        public SolidColorBrush BackgroundColor
        {
            get
            {
                if (IsCompleted)
                    return (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFADD8E6")); // Светло-голубой
                return Brushes.LightGray;
            }
        }

        public string VerticalClueText
        {
            get
            {
                if (Clues.Count == 0) return "";

                var result = new StringBuilder();
                for (int i = 0; i < Clues.Count; i++)
                {
                    result.Append(Clues[i]);
                    if (i < Clues.Count - 1)
                    {
                        result.AppendLine();
                    }
                }

                return result.ToString();
            }
        }

        public string ClueText => VerticalClueText;

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}