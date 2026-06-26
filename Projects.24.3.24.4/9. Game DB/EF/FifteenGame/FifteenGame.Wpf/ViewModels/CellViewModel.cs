namespace FifteenGame.Wpf.ViewModels
{
    public class CellViewModel : ViewModelBase
    {
        private bool _hasShop;
        private bool _isCovered;
        private bool _isVeggie;
        private bool _isRevealed;
        private int _veggieNeighborsCount;

        public int Row { get; set; }
        public int Column { get; set; }
        public int PeopleCount { get; set; }

        public bool HasShop
        {
            get => _hasShop;
            set { _hasShop = value; OnPropertyChanged(nameof(HasShop)); OnPropertyChanged(nameof(Text)); }
        }

        public bool IsCovered
        {
            get => _isCovered;
            set { _isCovered = value; OnPropertyChanged(nameof(IsCovered)); OnPropertyChanged(nameof(CellBackground)); OnPropertyChanged(nameof(Text)); }
        }

        public bool IsVeggie
        {
            get => _isVeggie;
            set { _isVeggie = value; OnPropertyChanged(nameof(IsVeggie)); OnPropertyChanged(nameof(CellBackground)); OnPropertyChanged(nameof(Text)); }
        }

        public bool IsRevealed
        {
            get => _isRevealed;
            set { _isRevealed = value; OnPropertyChanged(nameof(IsRevealed)); OnPropertyChanged(nameof(CellBackground)); OnPropertyChanged(nameof(Text)); }
        }

        public int VeggieNeighborsCount
        {
            get => _veggieNeighborsCount;
            set { _veggieNeighborsCount = value; OnPropertyChanged(nameof(VeggieNeighborsCount)); OnPropertyChanged(nameof(Text)); }
        }

        public string Text
        {
            get
            {
                if (HasShop) return "🏪";

                // Если клетка попала в радиус или под ларек
                if (IsRevealed)
                {
                    if (IsVeggie)
                    {
                        return $"{PeopleCount} чел\n(ЗОЖ)";
                    }

                    // Если обычная но рядом есть зожники выводим индикатор
                    if (VeggieNeighborsCount > 0)
                    {
                        string radars = new string('з', VeggieNeighborsCount);
                        return $"{PeopleCount} чел\n{radars}";
                    }
                }

                // В базовом состоянии просто показывает людей
                return $"{PeopleCount} чел";
            }
        }

        public string CellBackground
        {
            get
            {
                // Если ларёк встал на клетку ЗОЖ
                if (HasShop && IsVeggie)
                {
                    return "Purple";
                }

                // Если на обычной клетке стоит ларёк
                if (HasShop)
                {
                    return "Gold";
                }

                // Если клетка ЗОЖ попала в радиус но на ней самой нет ларька
                if (IsCovered && IsVeggie)
                {
                    return "Gray"; // Или "LightGray"
                }

                // Если обычная клетка попала в радиус ларька но на ней нет ларька
                if (IsCovered)
                {
                    return "#A3E4D7";
                }

                return "LightCoral"; // Голодная клетка
            }
        }
    }
}