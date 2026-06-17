using CardFile.Common.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardFile.Wpf.ViewModels
{
    public class CardViewModel : ViewModelBase
    {
        private int _id;
        private string _title;
        private string _director;
        private int _year;
        private string _genre;
        private int _duration;
        private decimal _rating;

        public int Id
        {
            get => _id;
            set { _id = value; OnPropertyChanged(nameof(Id)); }
        }

        public string Title
        {
            get => _title;
            set { _title = value; OnPropertyChanged(nameof(Title)); }
        }

        public string Director
        {
            get => _director;
            set { _director = value; OnPropertyChanged(nameof(Director)); }
        }

        public int Year
        {
            get => _year;
            set { _year = value; OnPropertyChanged(nameof(Year)); }
        }

        public string Genre
        {
            get => _genre;
            set { _genre = value; OnPropertyChanged(nameof(Genre)); }
        }

        public int Duration
        {
            get => _duration;
            set { _duration = value; OnPropertyChanged(nameof(Duration)); }
        }

        public decimal Rating
        {
            get => _rating;
            set { _rating = value; OnPropertyChanged(nameof(Rating)); }
        }

        // Вычисляемые свойства для удобного вывода в DataGrid
        public string DisplayInfo => $"{Title} ({Year})";
        public string DurationText => $"{Duration} мин.";
        public string RatingText => Rating.ToString("0.0");

        public void LoadViewModel(CardViewModel model)
        {
            Mapping.Mapper.Map(model, this);
        }
    }
}
