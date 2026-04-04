using CardFile.Business.Models;
using CardFile.Business.Services;
using CardFile.Common.Infrastructure;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows.Controls;

namespace CardFile.Wpf.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly CardFileService _service = new CardFileService();

        public ObservableCollection<CardViewModel> Cards { get; set; } = new ObservableCollection<CardViewModel>();
        public ObservableCollection<CardViewModel> FilteredCards { get; set; } = new ObservableCollection<CardViewModel>();

        public CardViewModel SelectedCard { get; set; }

        public string FileName { get; set; }

        public string WindowTitle => string.IsNullOrEmpty(FileName) ? "Морг" : $"Морг: {Path.GetFileName(FileName)}";

        private string _searchText = "";
        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                OnPropertyChanged(nameof(SearchText));
                ApplyFilters();
            }
        }

        private ComboBoxItem _selectedFilter;
        public ComboBoxItem SelectedFilter
        {
            get => _selectedFilter;
            set
            {
                _selectedFilter = value;
                OnPropertyChanged(nameof(SelectedFilter));
                ApplyFilters();
            }
        }

        private ComboBoxItem _selectedSort;
        public ComboBoxItem SelectedSort
        {
            get => _selectedSort;
            set
            {
                _selectedSort = value;
                OnPropertyChanged(nameof(SelectedSort));
                ApplyFilters();
            }
        }

        public bool IsEditButtonEnabled => SelectedCard != null;
        public bool IsDeleteButtonEnabled => SelectedCard != null;

        public void Initialized()
        {
            LoadCards();
        }

        public void WindowLoaded()
        {
            UpdateButtons();
        }

        public void SelectionChanged()
        {
            UpdateButtons();
        }

        public void UpdateButtons()
        {
            OnPropertyChanged(nameof(IsEditButtonEnabled));
            OnPropertyChanged(nameof(IsDeleteButtonEnabled));
        }

        public void LoadCards()
        {
            Cards.Clear();

            foreach (var card in _service.GetAll())
            {
                Cards.Add(ToViewModel(card));
            }

            ApplyFilters();
        }

        private Card FromViewModel(CardViewModel card)
        {
            if (card == null)
                return null;

            return new Card
            {
                Id = card.Id,
                FirstName = card.FirstName,
                MiddleName = card.MiddleName,
                LastName = card.LastName,
                BirthDate = card.BirthDate,
                DeathDate = card.DeathDate,
                CauseOfDeath = card.CauseOfDeath,
                PlaceOfDeath = card.PlaceOfDeath,
                AdmissionDate = card.AdmissionDate,
                SectionNumber = card.SectionNumber,
                IsReleased = card.IsReleased
            };
        }

        public CardViewModel GetSelectedCard()
        {
            return SelectedCard;
        }

        public void SaveNewCard(CardViewModel card)
        {
            var model = FromViewModel(card);
            _service.Save(model);
            LoadCards();
        }

        public void SaveEditedCard(CardViewModel card)
        {
            var model = FromViewModel(card);
            _service.Save(model);
            LoadCards();
        }

        public void DeleteSelectedCard()
        {
            if (SelectedCard == null)
                return;

            _service.Delete(SelectedCard.Id);
            LoadCards();
        }

        public void OpenFromFile(string fileName)
        {
            _service.OpenFromFile(fileName);
            FileName = fileName;
            OnPropertyChanged(nameof(FileName));
            OnPropertyChanged(nameof(WindowTitle));
            LoadCards();
        }

        public void SaveToFile()
        {
            if (string.IsNullOrEmpty(FileName))
                return;

            _service.SaveToFile(FileName);
        }

        public void SaveToFile(string fileName)
        {
            _service.SaveToFile(fileName);
            FileName = fileName;
            OnPropertyChanged(nameof(FileName));
            OnPropertyChanged(nameof(WindowTitle));
        }

        private CardViewModel ToViewModel(Card card)
        {
            if (card == null)
                return null;

            return new CardViewModel
            {
                Id = card.Id,
                FirstName = card.FirstName,
                MiddleName = card.MiddleName,
                LastName = card.LastName,
                BirthDate = card.BirthDate,
                DeathDate = card.DeathDate,
                CauseOfDeath = card.CauseOfDeath,
                PlaceOfDeath = card.PlaceOfDeath,
                AdmissionDate = card.AdmissionDate,
                SectionNumber = card.SectionNumber,
                IsReleased = card.IsReleased
            };
        }

        public CardViewModel GetNewCard()
        {
            return new CardViewModel
            {
                BirthDate = new DateTime(1980, 1, 1),
                DeathDate = DateTime.Today,
                AdmissionDate = DateTime.Today,
                CauseOfDeath = "",
                PlaceOfDeath = "",
                SectionNumber = "",
                IsReleased = false
            };
        }

        private void ApplyFilters()
        {
            var query = Cards.AsEnumerable();

            if (!string.IsNullOrWhiteSpace(SearchText))
            {
                var text = SearchText.ToLower();
                query = query.Where(c =>
                    (c.Fio ?? "").ToLower().Contains(text) ||
                    (c.CauseOfDeath ?? "").ToLower().Contains(text) ||
                    (c.PlaceOfDeath ?? "").ToLower().Contains(text) ||
                    (c.SectionNumber ?? "").ToLower().Contains(text));
            }

            var filter = SelectedFilter?.Content?.ToString();
            switch (filter)
            {
                case "Только в морге":
                    query = query.Where(c => !c.IsReleased);
                    break;
                case "Только выданные":
                    query = query.Where(c => c.IsReleased);
                    break;
                case "Поступившие сегодня":
                    query = query.Where(c => c.AdmissionDate.Date == DateTime.Today);
                    break;
            }

            var sort = SelectedSort?.Content?.ToString();
            switch (sort)
            {
                case "По дате смерти":
                    query = query.OrderByDescending(c => c.DeathDate);
                    break;
                case "По дате поступления":
                    query = query.OrderByDescending(c => c.AdmissionDate);
                    break;
                case "По секции":
                    query = query.OrderBy(c => c.SectionNumber);
                    break;
                default:
                    query = query.OrderBy(c => c.LastName).ThenBy(c => c.FirstName).ThenBy(c => c.MiddleName);
                    break;
            }

            FilteredCards.Clear();
            foreach (var item in query)
            {
                FilteredCards.Add(item);
            }

            OnPropertyChanged(nameof(FilteredCards));
        }
    }
}