using CardFile.Business.Models;
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
        private readonly CardFileService _service = new CardFileService();

        private string _searchText;
        private string _selectedCategory = "Все категории";
        private bool _showPinnedOnly;

        public ObservableCollection<CardViewModel> Cards { get; } =
            new ObservableCollection<CardViewModel>();

        public ObservableCollection<string> Categories { get; } =
            new ObservableCollection<string>();

        public ICollectionView CardsView { get; private set; }

        public CardViewModel SelectedCard { get; set; }

        public bool IsEditButtonEnabled => SelectedCard != null;
        public bool IsDeleteButtonEnabled => SelectedCard != null;
        public bool IsToggleDoneButtonEnabled => SelectedCard != null;
        public bool IsTogglePinnedButtonEnabled => SelectedCard != null;

        public string ToggleDoneButtonText
            => SelectedCard == null
                ? "Выполнено"
                : (SelectedCard.IsDone ? "Снять выполнение" : "Отметить выполненной");

        public string TogglePinnedButtonText
            => SelectedCard == null
                ? "Закрепить"
                : (SelectedCard.IsPinned ? "Открепить" : "Закрепить");

        public string FileName { get; private set; }

        public string WindowTitle
            => string.IsNullOrEmpty(FileName)
                ? "Менеджер заметок"
                : $"Менеджер заметок: {Path.GetFileName(FileName)}";

        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                OnPropertyChanged(nameof(SearchText));
                RefreshCards();
            }
        }

        public string SelectedCategory
        {
            get => _selectedCategory;
            set
            {
                _selectedCategory = value;
                OnPropertyChanged(nameof(SelectedCategory));
                RefreshCards();
            }
        }

        public bool ShowPinnedOnly
        {
            get => _showPinnedOnly;
            set
            {
                _showPinnedOnly = value;
                OnPropertyChanged(nameof(ShowPinnedOnly));
                RefreshCards();
            }
        }

        public string StatisticsText
            => $"Всего: {Cards.Count}   Выполнено: {Cards.Count(c => c.IsDone)}   Закреплено: {Cards.Count(c => c.IsPinned)}";

        public MainWindowViewModel()
        {
            MapperInitialization.PreRegister();

            CardsView = CollectionViewSource.GetDefaultView(Cards);
            CardsView.Filter = FilterCard;
        }

        public void WindowLoaded()
        {
            LoadAllData();
        }

        public void Initialized()
        {
            Mapping.Initialize();
        }

        public CardViewModel GetSelectedCard()
        {
            return SelectedCard;
        }

        public CardViewModel GetNewCard()
        {
            return new CardViewModel
            {
                CreatedAt = DateTime.Today,
                Category = "Общее",
                IsDone = false,
                IsPinned = false,
            };
        }

        public void SaveEditedCard(CardViewModel card)
        {
            var index = Cards.ToList().FindIndex(c => c.Id == card.Id);

            if (index < 0)
            {
                throw new Exception("Заметка с таким Id не существует");
            }

            var id = _service.Save(ToBusiness(card));

            if (id < 0)
            {
                Cards.RemoveAt(index);
            }
            else
            {
                Cards[index].LoadViewModel(card);
                OnPropertyChanged(nameof(ToggleDoneButtonText));
                OnPropertyChanged(nameof(TogglePinnedButtonText));
            }

            UpdateCategories();
            RefreshCards();
            OnPropertyChanged(nameof(StatisticsText));
        }

        public void SaveNewCard(CardViewModel card)
        {
            var newCard = new CardViewModel();
            newCard.LoadViewModel(card);

            var id = _service.Save(ToBusiness(card));

            if (id < 0)
            {
                return;
            }

            newCard.Id = id;
            Cards.Add(newCard);

            UpdateCategories();
            RefreshCards();
            OnPropertyChanged(nameof(StatisticsText));
        }

        public void SelectionChanged()
        {
            OnPropertyChanged(nameof(IsDeleteButtonEnabled));
            OnPropertyChanged(nameof(IsEditButtonEnabled));
            OnPropertyChanged(nameof(IsToggleDoneButtonEnabled));
            OnPropertyChanged(nameof(IsTogglePinnedButtonEnabled));
            OnPropertyChanged(nameof(ToggleDoneButtonText));
            OnPropertyChanged(nameof(TogglePinnedButtonText));
        }

        private void LoadAllData()
        {
            var cards = _service.GetAll();

            Cards.Clear();

            foreach (var card in cards)
            {
                Cards.Add(ToViewModel(card));
            }

            UpdateCategories();
            RefreshCards();
            OnPropertyChanged(nameof(StatisticsText));
        }

        public void DeleteSelectedCard()
        {
            if (SelectedCard == null)
            {
                return;
            }

            _service.Delete(SelectedCard.Id);

            var index = Cards.ToList().FindIndex(c => c.Id == SelectedCard.Id);

            if (index < 0)
            {
                throw new Exception("Заметка с таким Id не существует");
            }

            Cards.RemoveAt(index);
            SelectedCard = null;

            UpdateCategories();
            RefreshCards();

            OnPropertyChanged(nameof(SelectedCard));
            OnPropertyChanged(nameof(StatisticsText));
            SelectionChanged();
        }

        public void ToggleDoneSelectedCard()
        {
            if (SelectedCard == null)
            {
                return;
            }

            SelectedCard.ToggleDone();
            _service.Save(ToBusiness(SelectedCard));

            RefreshCards();
            OnPropertyChanged(nameof(ToggleDoneButtonText));
            OnPropertyChanged(nameof(StatisticsText));
        }

        public void TogglePinnedSelectedCard()
        {
            if (SelectedCard == null)
            {
                return;
            }

            SelectedCard.TogglePinned();
            _service.Save(ToBusiness(SelectedCard));

            RefreshCards();
            OnPropertyChanged(nameof(TogglePinnedButtonText));
            OnPropertyChanged(nameof(StatisticsText));
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

        public void ResetFilters()
        {
            SearchText = string.Empty;
            SelectedCategory = "Все категории";
            ShowPinnedOnly = false;
        }

        private bool FilterCard(object item)
        {
            var card = item as CardViewModel;

            if (card == null)
            {
                return false;
            }

            if (ShowPinnedOnly && !card.IsPinned)
            {
                return false;
            }

            if (!string.IsNullOrWhiteSpace(SelectedCategory) &&
                SelectedCategory != "Все категории" &&
                !string.Equals(card.Category, SelectedCategory, StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }

            if (string.IsNullOrWhiteSpace(SearchText))
            {
                return true;
            }

            var search = SearchText.Trim();

            return (card.Title ?? string.Empty).IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0
                || (card.Text ?? string.Empty).IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0
                || (card.Category ?? string.Empty).IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0;
        }

        private void RefreshCards()
        {
            CardsView?.Refresh();
        }

        private void UpdateCategories()
        {
            var currentCategory = SelectedCategory;

            Categories.Clear();
            Categories.Add("Все категории");

            foreach (var category in Cards
                .Select(c => c.Category)
                .Where(c => !string.IsNullOrWhiteSpace(c))
                .Distinct()
                .OrderBy(c => c))
            {
                Categories.Add(category);
            }

            if (Categories.Contains(currentCategory))
            {
                _selectedCategory = currentCategory;
            }
            else
            {
                _selectedCategory = "Все категории";
            }

            OnPropertyChanged(nameof(SelectedCategory));
        }

        private CardViewModel ToViewModel(Card card)
        {
            return Mapping.Mapper.Map<CardViewModel>(card);
        }

        private Card ToBusiness(CardViewModel card)
        {
            return Mapping.Mapper.Map<Card>(card);
        }
    }
}
