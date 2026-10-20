using CardFile.Common.Infrastructure;
using System.ComponentModel;

namespace CardFile.Wpf.ViewModels
{
    public class CardViewModel : ViewModelBase
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Название фильма
        /// </summary>
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// Режиссёр
        /// </summary>
        public string Director { get; set; } = string.Empty;

        /// <summary>
        /// Год выпуска
        /// </summary>
        public int ReleaseYear { get; set; }

        /// <summary>
        /// Жанр
        /// </summary>
        public string Genre { get; set; } = string.Empty;

        /// <summary>
        /// Длительность в минутах
        /// </summary>
        public decimal DurationMinutes { get; set; }

        /// <summary>
        /// Рейтинг (1–10)
        /// </summary>
        public int Rating
        {
            get => _rating;
            set
            {
                if (value < 1) value = 1;
                if (value > 10) value = 10;
                _rating = value;
                OnPropertyChanged(nameof(Rating));
            }
        }
        private int _rating;

        /// <summary>
        /// В наличии
        /// </summary>
        public bool IsAvailable { get; set; } = true;

        public string Fio => Title;
        public string BirthDateText => ReleaseYear.ToString();
        public string Department => Genre;
        public string Position => Director;
        public string EmploymentDateText => $"{DurationMinutes} мин";
        public string DismissalDateText => IsAvailable ? "В наличии" : "Выдан/Недоступен";
        public string SalaryText => Rating.ToString();


        public void LoadViewModel(CardViewModel model)
        {
            Title = model.Title;
            Director = model.Director;
            ReleaseYear = model.ReleaseYear;
            Genre = model.Genre;
            DurationMinutes = model.DurationMinutes;
            Rating = model.Rating;
            IsAvailable = model.IsAvailable;

            UpdateAll();
        }

        private void UpdateAll()
        {
            OnPropertyChanged(nameof(Id));
            OnPropertyChanged(nameof(Title));
            OnPropertyChanged(nameof(Director));
            OnPropertyChanged(nameof(ReleaseYear));
            OnPropertyChanged(nameof(Genre));
            OnPropertyChanged(nameof(DurationMinutes));
            OnPropertyChanged(nameof(Rating));
            OnPropertyChanged(nameof(IsAvailable));

            OnPropertyChanged(nameof(Fio));
            OnPropertyChanged(nameof(BirthDateText));
            OnPropertyChanged(nameof(Department));
            OnPropertyChanged(nameof(Position));
            OnPropertyChanged(nameof(EmploymentDateText));
            OnPropertyChanged(nameof(DismissalDateText));
            OnPropertyChanged(nameof(SalaryText));
        }
    }
}
