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
        private string _title;
        private string _genre;
        private double _globalRating;
        private int _myScore = 5;
        private DateTime _releaseDate = DateTime.Now;

        public int Id { get; set; }

        public string Title
        {
            get => _title;
            set { _title = value; OnPropertyChanged(); }
        }

        public string Genre
        {
            get => _genre;
            set { _genre = value; OnPropertyChanged(); }
        }

        public double GlobalRating
        {
            get => _globalRating;
            set { _globalRating = value; OnPropertyChanged(); }
        }

        public int MyScore
        {
            get => _myScore;
            set { _myScore = value; OnPropertyChanged(); }
        }

        public DateTime ReleaseDate
        {
            get => _releaseDate;
            set { _releaseDate = value; OnPropertyChanged(); }
        }

        public void LoadViewModel(CardViewModel source)
        {
            this.Id = source.Id;
            this.Title = source.Title;
            this.Genre = source.Genre;
            this.GlobalRating = source.GlobalRating;
            this.MyScore = source.MyScore;
            this.ReleaseDate = source.ReleaseDate;
        }
    }
}
