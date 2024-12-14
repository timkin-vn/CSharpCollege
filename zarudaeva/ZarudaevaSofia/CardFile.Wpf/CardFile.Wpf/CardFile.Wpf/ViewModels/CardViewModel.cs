using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardFile.Wpf.ViewModels
{
    public class CardViewModel : INotifyPropertyChanged
    {
        private string _movieName;

        private string _movieType;

        private DateTime _dateReles = new DateTime(2000, 6, 15);

        private TimeSpan _timeGoes = new TimeSpan(12,50,57);
        private decimal _priseOneTickets;
        private int _countTickets; 
        private short _linePlace;
        private short _place;  
       
      

        public int Id { get; set; }

        public string Seats_Hall => $"ряд -{LinePlace} место - {Place}";

        public string MovieName
        {
            get => _movieName;
            set
            {
                _movieName = value;
               
                OnPropertyChanged(nameof(MovieName));
            }
        }

        public string MovieType
        {
            get => _movieType;
            set
            {
                _movieType = value;
   
                OnPropertyChanged(nameof(MovieType));
            }
        }

        public DateTime DateReles
        {
            get => _dateReles;
            set
            {
                _dateReles = value;
                OnPropertyChanged(nameof(DateReles));
            }
        }
        public string DateStarteText => DateReles.ToShortDateString();
        public TimeSpan TimeGoes
        {
            get => _timeGoes;
            set
            {
                _timeGoes = value;
                OnPropertyChanged(nameof(TimeGoes));
            }
        }
        public string TimeGoesText =>$"{TimeGoes.Hours}:{TimeGoes.Minutes}:{TimeGoes.Seconds}";



        public decimal PriseOneTickets
        {
            get => _priseOneTickets;
            set
            {
                _priseOneTickets = value;
                OnPropertyChanged(nameof(PriseOneTickets));
            }
        }
        public decimal Prise => PriseOneTickets * CountTickets;

        public string PrisуeText => Prise.ToString("#,##0.00 р\\.");

        public int CountTickets
        {
            get => _countTickets;
            set
            {
                _countTickets = value;
                OnPropertyChanged(nameof(CountTickets));
            }
        } 
        public short LinePlace
        {
            get => _linePlace;
            set
            {
                _linePlace = value;
                OnPropertyChanged(nameof(LinePlace));
            }
        } 
        public short Place
        {
            get => _place;
            set
            {
                _place = value;
                OnPropertyChanged(nameof(Place));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void CopyFrom(CardViewModel model)
        {
            Id = model.Id;
            MovieName = model.MovieName;
            MovieType = model.MovieType;
            DateReles = model.DateReles;
            TimeGoes = model.TimeGoes;
            PriseOneTickets= model.PriseOneTickets;
            CountTickets= model.CountTickets;
            LinePlace = model.LinePlace;
            Place = model.Place;


        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
