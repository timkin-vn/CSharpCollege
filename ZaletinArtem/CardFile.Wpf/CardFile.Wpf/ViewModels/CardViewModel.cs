using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardFile.Wpf.ViewModels
{
    internal class CardViewModel : INotifyPropertyChanged
    {
        private string _title;

        private string _author;

        private DateTime _publicationDate = new DateTime(2000, 1, 1);

        private string _genre;

        private int _pageCount;

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

        public string Author
        {
            get => _author;
            set
            {
                _author = value;
                OnPropertyChanged(nameof(Author));
            }
        }

        public DateTime PublicationDate
        {
            get => _publicationDate;
            set
            {
                _publicationDate = value;
                OnPropertyChanged(nameof(PublicationDate));
            }
        }

        public string PublicationDateText => PublicationDate.ToShortDateString();

        public string Genre
        {
            get => _genre;
            set
            {
                _genre = value;
                OnPropertyChanged(nameof(Genre));
            }
        }

        public int PageCount
        {
            get => _pageCount;
            set
            {
                _pageCount = value;
                OnPropertyChanged(nameof(PageCount));
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
            Author = model.Author;
            PublicationDate = model.PublicationDate;
            Genre = model.Genre;
            PageCount = model.PageCount;
            Price = model.Price;
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
