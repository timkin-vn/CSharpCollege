using CardFile.Common.Infrastructure;
using System;

namespace CardFile.Wpf.ViewModels
{
    public class BookViewModel : ViewModelBase
    {
        private int _id;
        private string _title;
        private string _author;
        private string _genre;
        private int _year;
        private int _copies;
        private DateTime _addedDate;
        private DateTime? _deletedDate;
        private bool _isWorkingTillNow;

        public int Id
        {
            get => _id;
            set
            {
                _id = value;
                OnPropertyChanged(nameof(Id));
            }
        }

        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                OnPropertyChanged(nameof(Title));
                OnPropertyChanged(nameof(DisplayText));
            }
        }

        public string Author
        {
            get => _author;
            set
            {
                _author = value;
                OnPropertyChanged(nameof(Author));
                OnPropertyChanged(nameof(DisplayText));
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

        public int Year
        {
            get => _year;
            set
            {
                _year = value;
                OnPropertyChanged(nameof(Year));
                OnPropertyChanged(nameof(DisplayText));
            }
        }

        public int Copies
        {
            get => _copies;
            set
            {
                _copies = value;
                OnPropertyChanged(nameof(Copies));
                OnPropertyChanged(nameof(Status));
                OnPropertyChanged(nameof(IsAvailable));
            }
        }

        public DateTime AddedDate
        {
            get => _addedDate;
            set
            {
                _addedDate = value;
                OnPropertyChanged(nameof(AddedDate));
                OnPropertyChanged(nameof(AddedDateText));
            }
        }

        public DateTime? DeletedDate
        {
            get => _deletedDate;
            set
            {
                _deletedDate = value;
                OnPropertyChanged(nameof(DeletedDate));
                OnPropertyChanged(nameof(IsDeleted));
                OnPropertyChanged(nameof(Status));
            }
        }

        public bool IsWorkingTillNow
        {
            get => _isWorkingTillNow;
            set
            {
                _isWorkingTillNow = value;
                OnPropertyChanged(nameof(IsWorkingTillNow));
                OnPropertyChanged(nameof(IsDismissalDateEnabled));
            }
        }

        public bool IsDeleted => DeletedDate != null;

        public string Status
        {
            get
            {
                if (IsDeleted) return "Списана";
                if (Copies > 0) return $"В наличии: {Copies}";
                return "Нет в наличии";
            }
        }

        public bool IsAvailable => !IsDeleted && Copies > 0;

        public bool IsDismissalDateEnabled => !IsWorkingTillNow;

        public string DisplayText => $"{Title} - {Author} ({Year})";

        public string AddedDateText => AddedDate.ToShortDateString();

        public void LoadViewModel(BookViewModel model)
        {
            if (model == null) return;

            Id = model.Id;
            Title = model.Title;
            Author = model.Author;
            Genre = model.Genre;
            Year = model.Year;
            Copies = model.Copies;
            AddedDate = model.AddedDate;
            DeletedDate = model.DeletedDate;
            IsWorkingTillNow = model.IsWorkingTillNow;
        }

        public void BorrowBook()
        {
            if (Copies > 0)
            {
                Copies--;
            }
        }

        public void ReturnBook()
        {
            Copies++;
        }

        public void IsWorkingTillNowChecked()
        {
            IsWorkingTillNow = true;
            DeletedDate = null;
            OnPropertyChanged(nameof(DeletedDate));
            OnPropertyChanged(nameof(IsDismissalDateEnabled));
        }

        public void IsWorkingTillNowUnchecked()
        {
            IsWorkingTillNow = false;
            DeletedDate = DateTime.Today;
            OnPropertyChanged(nameof(DeletedDate));
            OnPropertyChanged(nameof(IsDismissalDateEnabled));
        }
    }
}