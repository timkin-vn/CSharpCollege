using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardFile.Wpf.ViewModels
{
    internal class CardViewModel : INotifyPropertyChanged
    {
        private string _title;

        private DateTime _dateRelease=new DateTime(2000, 1, 1);

        private string _director;

        private string _film_genre;

        private decimal _price;

        //private bool _is_sell;

        private int _count_actor;
        

        public int Id { get; set; }

        public string Title {
            get => _title;
            set {
                _title = value;
                OnPropertyChanged(nameof(Title));

            }
        }

       public int Count_actor {
            get => _count_actor;
            set {

                _count_actor = value;
                OnPropertyChanged(nameof(Count_actor));
            }
        }

        public DateTime DateRelease
        {
            get => _dateRelease;
            set
            {
                _dateRelease = value;
                OnPropertyChanged(nameof(DateRelease));
            }
        }

        public string DateReleaseText => DateRelease.ToShortDateString();

        public string Director
        {
            get => _director;
            set
            {
                _director = value;
                OnPropertyChanged(nameof(Director));
            }
        }

        public string FilmReuge
        {
            get => _film_genre;
            set
            {
                _film_genre = value;
                OnPropertyChanged(nameof(FilmReuge));
            }
        }


        public decimal Price
        {
            get => _price;
            set
            {
                _price = value;
                OnPropertyChanged(nameof(Price));
            }
        }

        public string PaymentAmountText => $"{Price:#,##0.00} р.";

        public event PropertyChangedEventHandler PropertyChanged;

        public void CopyFrom(CardViewModel model)
        {
            Id = model.Id;
            Title = model.Title;
            DateRelease = model.DateRelease;
            Director = model.Director;
            FilmReuge = model.FilmReuge;
            Price = model.Price;
            
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
