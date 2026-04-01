using CardFile.Business.Models;
using CardFile.Business.Services;
using CardFile.Common.Infrastructure;
using CardFile.Wpf.Infrastructure;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;

namespace CardFile.Wpf.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly LibraryService _service = new LibraryService();
        private string _searchText;
        private bool _showDeleted;

        public ObservableCollection<BookViewModel> Books { get; } = new ObservableCollection<BookViewModel>();
        public BookViewModel SelectedBook { get; set; }

        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                OnPropertyChanged(nameof(SearchText));
                Refresh();
            }
        }

        public bool ShowDeleted
        {
            get => _showDeleted;
            set
            {
                _showDeleted = value;
                OnPropertyChanged(nameof(ShowDeleted));
                Refresh();
            }
        }

        public bool IsEditEnabled => SelectedBook != null && !SelectedBook.IsDeleted;
        public bool IsDeleteEnabled => SelectedBook != null && !SelectedBook.IsDeleted;
        public bool IsRestoreEnabled => SelectedBook != null && SelectedBook.IsDeleted;
        public bool IsIssueEnabled => SelectedBook != null && !SelectedBook.IsDeleted && SelectedBook.Copies > 0;

        public string FileName { get; private set; }
        public string WindowTitle => string.IsNullOrEmpty(FileName) ? "Библиотечный каталог" : $"Библиотека: {Path.GetFileName(FileName)}";

        public MainWindowViewModel()
        {
            MapperInitialization.PreRegister();
        }

        public void Initialize()
        {
            Mapping.Initialize();
            Refresh();
        }

        public void Refresh()
        {
            System.Collections.Generic.IEnumerable<Book> books;

            if (ShowDeleted)
            {
                books = _service.GetDeleted();
            }
            else
            {
                books = _service.GetAll();
            }

            if (!string.IsNullOrWhiteSpace(SearchText))
            {
                books = _service.Search(SearchText);
            }

            Books.Clear();
            foreach (var book in books)
            {
                Books.Add(ToViewModel(book));
            }
        }

        public BookViewModel NewBook()
        {
            return new BookViewModel
            {
                AddedDate = DateTime.Today,
                Year = DateTime.Today.Year,
                Copies = 1
            };
        }

        public void SaveBook(BookViewModel book)
        {
            if (book.Id == 0)
            {
                int id = _service.Save(ToBusiness(book));
                if (id > 0)
                {
                    book.Id = id;
                    Books.Add(book);
                }
            }
            else
            {
                _service.Save(ToBusiness(book));
                for (int i = 0; i < Books.Count; i++)
                {
                    if (Books[i].Id == book.Id)
                    {
                        Books[i].LoadViewModel(book);
                        break;
                    }
                }
            }
            Refresh();
        }

        public void DeleteBook()
        {
            if (SelectedBook == null) return;
            _service.Delete(SelectedBook.Id);
            Refresh();
            SelectedBook = null;
            OnPropertyChanged(nameof(SelectedBook));
        }

        public void RestoreBook()
        {
            if (SelectedBook == null) return;
            _service.Restore(SelectedBook.Id);
            Refresh();
            SelectedBook = null;
            OnPropertyChanged(nameof(SelectedBook));
        }

        public void IssueBook()
        {
            if (SelectedBook == null) return;
            if (_service.Issue(SelectedBook.Id))
            {
                Refresh();
            }
        }

        public void ReturnBook()
        {
            if (SelectedBook == null) return;
            if (_service.Return(SelectedBook.Id))
            {
                Refresh();
            }
        }

        public void SaveToFile(string fileName)
        {
            _service.SaveToFile(fileName);
            FileName = fileName;
            OnPropertyChanged(nameof(WindowTitle));
        }

        public void OpenFromFile(string fileName)
        {
            _service.OpenFromFile(fileName);
            FileName = fileName;
            Refresh();
            OnPropertyChanged(nameof(WindowTitle));
        }

        public void SaveToFile()
        {
            try
            {
                _service.SaveToFile(FileName);
            }
            catch (Exception)
            {
                FileName = null;
                throw;
            }
        }

        public void SelectionChanged()
        {
            OnPropertyChanged(nameof(IsEditEnabled));
            OnPropertyChanged(nameof(IsDeleteEnabled));
            OnPropertyChanged(nameof(IsRestoreEnabled));
            OnPropertyChanged(nameof(IsIssueEnabled));
        }

        private BookViewModel ToViewModel(Book book)
        {
            return Mapping.Mapper.Map<BookViewModel>(book);
        }

        private Book ToBusiness(BookViewModel book)
        {
            return Mapping.Mapper.Map<Book>(book);
        }
    }
}