using System;
using System.ComponentModel;

namespace CardFile.Wpf.ViewModels
{
    public class CardViewModel : INotifyPropertyChanged
    {
        private string _title;
        private string _author;
        private DateTime _publicationDate = DateTime.Now;
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

        public void CopyFrom(CardViewModel source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            Id = source.Id;
            Title = source.Title;
            Author = source.Author;
            PublicationDate = source.PublicationDate;
            Genre = source.Genre;
            PageCount = source.PageCount;
            Price = source.Price;
        }


        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
