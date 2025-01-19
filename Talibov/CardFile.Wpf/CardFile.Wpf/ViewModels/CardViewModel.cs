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
        private string _name;

        private string _description;

        private string _street;

        private string _house;

        private string _city;

        private int _mailIndex;

        private double _rating;

        private int _counterReviews;

        private string _status;

        public int Id { get; set; }

        public string Address => $"{Street}, {House}, {City}, {MailIndex}";

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public string Description
        {
            get => _description;
            set
            {
                _description = value;
                OnPropertyChanged(nameof(Description));
            }
        }

        public string Street
        {
            get => _street;
            set
            {
                _street = value;
                OnPropertyChanged(nameof(Address));
                OnPropertyChanged(nameof(Street));
            }
        }

        public string House
        {
            get => _house;
            set
            {
                _house = value;
                OnPropertyChanged(nameof(Address));
                OnPropertyChanged(nameof(House));
            }
        }

        public string City
        {
            get => _city;
            set
            {
                _city = value;
                OnPropertyChanged(nameof(Address));
                OnPropertyChanged(nameof(City));
            }
        }

        public int MailIndex
        {
            get => _mailIndex;
            set
            {
                _mailIndex = value;
                OnPropertyChanged(nameof(Address));
                OnPropertyChanged(nameof(MailIndex));
            }
        }

        public double Rating
        {
            get => _rating;
            set
            {
                _rating = value;
                OnPropertyChanged(nameof(Rating));
            }
        }

        public int CounterReviews
        {
            get => _counterReviews;
            set
            {
                _counterReviews = value;
                OnPropertyChanged(nameof(CounterReviews));
            }
        }

        public string Status
        {
            get => _status;
            set
            {
                _status = value;
                OnPropertyChanged(nameof(Status));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void CopyFrom(CardViewModel model)
        {
            Id = model.Id;
            Name = model.Name;
            Description = model.Description;
            Street = model.Street;
            House = model.House;
            City = model.City;
            MailIndex = model.MailIndex;
            Rating = model.Rating;
            CounterReviews = model.CounterReviews;
            Status = model.Status;
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
