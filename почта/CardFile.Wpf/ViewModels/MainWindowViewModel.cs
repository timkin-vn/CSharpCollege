using CardFile.Business.Services;
using CardFile.Common.Infrastructure;
using CardFile.Wpf.Infrastructure;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Data;

namespace CardFile.Wpf.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly MailService _service = new MailService();
        public ObservableCollection<LetterViewModel> Letters { get; } = new ObservableCollection<LetterViewModel>();

        // Представление коллекции для DataGrid (поддерживает сортировку и фильтрацию)
        public ICollectionView LettersView { get; private set; }

        public LetterViewModel SelectedLetter { get; set; }
        public bool IsEditButtonEnabled => SelectedLetter != null;
        public bool IsDeleteButtonEnabled => SelectedLetter != null;
        public string FileName { get; private set; }
        public string WindowTitle => string.IsNullOrEmpty(FileName) ? "Почта" : $"Почта: {Path.GetFileName(FileName)}";

        // --- Свойства для поиска и сортировки ---
        private string _searchSender = string.Empty;
        public string SearchSender
        {
            get => _searchSender;
            set
            {
                _searchSender = value;
                OnPropertyChanged(nameof(SearchSender));
                ApplyFilter();
            }
        }

        private string _sortBy = "Дата";
        public string SortBy
        {
            get => _sortBy;
            set
            {
                _sortBy = value;
                OnPropertyChanged(nameof(SortBy));
                ApplySort();
            }
        }

        private bool _sortAscending = true;
        public bool SortAscending
        {
            get => _sortAscending;
            set
            {
                _sortAscending = value;
                OnPropertyChanged(nameof(SortAscending));
                ApplySort();
            }
        }

        public MainWindowViewModel()
        {
            MapperInitialization.PreRegister();
            // Создаем представление над коллекцией
            LettersView = CollectionViewSource.GetDefaultView(Letters);
        }

        public void WindowLoaded()
        {
            LoadAllData();
            ApplySort(); // Применяем сортировку по умолчанию после загрузки
        }

        public void Initialized() => Mapping.Initialize();

        public LetterViewModel GetSelectedLetter() => SelectedLetter;

        public LetterViewModel GetNewLetter()
        {
            return new LetterViewModel { Date = DateTime.Today, IsRead = false };
        }

        public void SaveEditedLetter(LetterViewModel letter)
        {
            var index = Letters.ToList().FindIndex(c => c.Id == letter.Id);
            if (index < 0) throw new Exception("Письмо с таким Id не существует");

            var id = _service.Save(ToBusiness(letter));
            if (id < 0) Letters.RemoveAt(index);
            else Letters[index].LoadViewModel(letter);
        }

        public void SaveNewLetter(LetterViewModel letter)
        {
            var newLetter = new LetterViewModel();
            newLetter.LoadViewModel(letter);
            var id = _service.Save(ToBusiness(letter));
            if (id >= 0)
            {
                newLetter.Id = id;
                Letters.Add(newLetter);
            }
        }

        public void SelectionChanged()
        {
            OnPropertyChanged(nameof(IsDeleteButtonEnabled));
            OnPropertyChanged(nameof(IsEditButtonEnabled));
        }

        private void LoadAllData()
        {
            var letters = _service.GetAll();
            Letters.Clear();
            foreach (var letter in letters) Letters.Add(ToViewModel(letter));
        }

        public void DeleteSelectedLetter()
        {
            if (SelectedLetter == null) return;
            _service.Delete(SelectedLetter.Id);
            var index = Letters.ToList().FindIndex(c => c.Id == SelectedLetter.Id);
            if (index < 0) throw new Exception("Письмо с таким Id не существует");
            Letters.RemoveAt(index);
            SelectedLetter = null;
            OnPropertyChanged(nameof(SelectedLetter));
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
            LoadAllData();
            FileName = fileName;
            OnPropertyChanged(nameof(WindowTitle));
            ApplySort();
        }

        public void SaveToFile()
        {
            try { _service.SaveToFile(FileName); }
            catch { FileName = null; throw; }
        }

        // --- Логика фильтрации и сортировки ---
        private void ApplyFilter()
        {
            if (LettersView == null) return;

            LettersView.Filter = item =>
            {
                if (string.IsNullOrWhiteSpace(SearchSender)) return true;

                var letter = item as LetterViewModel;
                return letter?.Sender?.IndexOf(SearchSender, StringComparison.OrdinalIgnoreCase) >= 0;
            };
        }

        private void ApplySort()
        {
            if (LettersView == null) return;

            LettersView.SortDescriptions.Clear();

            string propertyName;
            if (SortBy == "Отправитель") propertyName = nameof(LetterViewModel.Sender);
            else if (SortBy == "Тема") propertyName = nameof(LetterViewModel.Subject);
            else propertyName = nameof(LetterViewModel.Date); // По умолчанию: Дата

            var direction = SortAscending ? ListSortDirection.Ascending : ListSortDirection.Descending;
            LettersView.SortDescriptions.Add(new SortDescription(propertyName, direction));
            LettersView.Refresh();
        }

        private LetterViewModel ToViewModel(Business.Models.Letter letter) => Mapping.Mapper.Map<LetterViewModel>(letter);
        private Business.Models.Letter ToBusiness(LetterViewModel letter) => Mapping.Mapper.Map<Business.Models.Letter>(letter);
    }
}