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

        private DateTime _exp = new DateTime(2000, 1, 1);

        private string _fabricator;

        private string _section;

        private int _count;

        private decimal _price;

        public int Id { get; set; }

        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                OnPropertyChanged(nameof(Title));
            }
        }

        public DateTime EXP
        {
            get => _exp;
            set
            {
                _exp = value;
                OnPropertyChanged(nameof(EXP));
            }
        }

        public string EXPText => EXP.ToShortDateString();

        public string Fabricator
        {
            get => _fabricator;
            set
            {
                _fabricator = value;
                OnPropertyChanged(nameof(Fabricator));
            }
        }

        public string Section
        {
            get => _section;
            set
            {
                _section = value;
                OnPropertyChanged(nameof(Section));
            }
        }

        public int Count
        {
            get => _count;
            set
            {
                _count = value;
                OnPropertyChanged(nameof(Count));
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

        public string PriceText => $"{Price:#,##0.00} р.";

        public event PropertyChangedEventHandler PropertyChanged;

        public void CopyFrom(CardViewModel model)
        {
            Id = model.Id;
            Title = model.Title;
            EXP = model.EXP;
            Fabricator = model.Fabricator;
            Section = model.Section;
            Count = model.Count;
            Price = model.Price;
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
