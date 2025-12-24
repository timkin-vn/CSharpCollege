using System.ComponentModel;

namespace FifteenGame.Wpf.ViewModels
{
    /// <summary>
    /// Одна клетка поля 3x3.
    /// Оставили имена свойств (TextButoon/BruhButon/ColorButtonPen/TextColor),
    /// чтобы XAML не приходилось усложнять.
    /// </summary>
    public class CellViewModel : INotifyPropertyChanged
    {
        // === Новые поля (крестики-нолики) ===
        private string _symbol = string.Empty; // "X", "O" или пусто
        private bool _isWinningCell;
        private bool _isEnabled = true;

        // === Поля, которые были в старой игре (оставляем, чтобы ничего не сломалось при сборке) ===
        private bool _isOpen;
        private bool _isMatched;
        private string _nameComponents;

        public int Row { get; set; }
        public int Column { get; set; }
        public int[] ColumnRow { get; set; }

        // ===== Совместимость со старым кодом (Memory/15) =====
        // Старый ViewModel использовал NameСomponents/IsOpen/IsMatched.
        // Сейчас они не применяются в новой главной форме, но оставлены, чтобы проект собирался.
        public string NameСomponents
        {
            get => _nameComponents;
            set
            {
                if (_nameComponents == value) return;
                _nameComponents = value;
                OnPropertyChanged(nameof(NameСomponents));
                OnPropertyChanged(nameof(TextButoon));
            }
        }

        public bool IsOpen
        {
            get => _isOpen;
            set
            {
                if (_isOpen == value) return;
                _isOpen = value;
                OnPropertyChanged(nameof(IsOpen));
                OnPropertyChanged(nameof(TextButoon));
            }
        }

        public bool IsMatched
        {
            get => _isMatched;
            set
            {
                if (_isMatched == value) return;
                _isMatched = value;
                OnPropertyChanged(nameof(IsMatched));
                OnPropertyChanged(nameof(TextButoon));
            }
        }

        public string Symbol
        {
            get => _symbol;
            set
            {
                if (_symbol == value) return;
                _symbol = value ?? string.Empty;
                OnPropertyChanged(nameof(Symbol));
                OnPropertyChanged(nameof(TextButoon));
                OnPropertyChanged(nameof(TextColor));
                OnPropertyChanged(nameof(BruhButon));
                OnPropertyChanged(nameof(ColorButtonPen));
            }
        }

        public bool IsWinningCell
        {
            get => _isWinningCell;
            set
            {
                if (_isWinningCell == value) return;
                _isWinningCell = value;
                OnPropertyChanged(nameof(IsWinningCell));
                OnPropertyChanged(nameof(BruhButon));
                OnPropertyChanged(nameof(ColorButtonPen));
            }
        }

        public bool IsEnabled
        {
            get => _isEnabled;
            set
            {
                if (_isEnabled == value) return;
                _isEnabled = value;
                OnPropertyChanged(nameof(IsEnabled));
                OnPropertyChanged(nameof(BruhButon));
                OnPropertyChanged(nameof(ColorButtonPen));
                OnPropertyChanged(nameof(TextColor));
            }
        }

        // === Имена для биндингов (используются и в новом, и в старом XAML) ===
        public string TextButoon
        {
            get
            {
                // Для крестиков-ноликов показываем Symbol.
                if (!string.IsNullOrEmpty(Symbol)) return Symbol;

                // Если вдруг откроют старые экраны — вернём старое поведение.
                if ((_isOpen || _isMatched) && !string.IsNullOrEmpty(NameСomponents))
                    return NameСomponents;

                return string.Empty;
            }
        }

        public string BruhButon
        {
            get
            {
                if (IsWinningCell) return "#233B6E";   // подсветка победной линии
                if (!IsEnabled) return "#1A233A";      // клетка уже занята
                return "#0F1630";                      // обычная
            }
        }

        public string ColorButtonPen => IsWinningCell ? "#5B7CFF" : "#2A3557";

        public string TextColor => Symbol == "X" ? "#EAF0FF" : (Symbol == "O" ? "#A9B6DA" : "#EAF0FF");

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
