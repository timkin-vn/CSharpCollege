using CardFile.Business.Models;
using CardFile.Business.Services;
using CardFile.Common.Infrastructure;
using CardFile.Wpf.Infrastructure;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;

namespace CardFile.Wpf.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly CardFileService _service = new CardFileService();
        private List<CardViewModel> _allCards = new List<CardViewModel>();
        private string _searchText;

        public ObservableCollection<CardViewModel> Cards { get; } = new ObservableCollection<CardViewModel>();

        public CardViewModel SelectedCard { get; set; }

        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                OnPropertyChanged(nameof(SearchText));
                ApplyFilter();
            }
        }

        public bool IsEditButtonEnabled => SelectedCard != null;
        public bool IsDeleteButtonEnabled => SelectedCard != null;
        public string FileName { get; private set; }
        public string WindowTitle => string.IsNullOrEmpty(FileName) ? "Картотека" : $"Картотека: {Path.GetFileName(FileName)}";

        public MainWindowViewModel()
        {
            MapperInitialization.PreRegister();
        }

        public void WindowLoaded()
        {
            LoadAllData();
        }

        public void Initialized()
        {
            Mapping.Initialize();
        }

        public CardViewModel GetSelectedCard() => SelectedCard;

        public CardViewModel GetNewCard()
        {
            return new CardViewModel
            {
                BirthDate = new DateTime(2000, 6, 15),
                EmploymentDate = DateTime.Today,
            };
        }

        public void SaveEditedCard(CardViewModel card)
        {
            var index = _allCards.FindIndex(c => c.Id == card.Id);
            if (index < 0) throw new Exception("Карточка с таким Id не существует");

            var id = _service.Save(ToBusiness(card));
            if (id < 0)
            {
                _allCards.RemoveAt(index);
            }
            else
            {
                _allCards[index].LoadViewModel(card);
            }
            ApplyFilter();
        }

        public void SaveNewCard(CardViewModel card)
        {
            var newCard = new CardViewModel();
            newCard.LoadViewModel(card);

            var id = _service.Save(ToBusiness(card));
            if (id < 0) return;

            newCard.Id = id;
            _allCards.Add(newCard);
            ApplyFilter();
        }

        public void SelectionChanged()
        {
            OnPropertyChanged(nameof(IsDeleteButtonEnabled));
            OnPropertyChanged(nameof(IsEditButtonEnabled));
        }

        private void LoadAllData()
        {
            var cards = _service.GetAll();
            _allCards.Clear();
            foreach (var card in cards)
            {
                _allCards.Add(ToViewModel(card));
            }
            ApplyFilter();
        }

        private void ApplyFilter()
        {
            Cards.Clear();
            var filtered = string.IsNullOrWhiteSpace(SearchText)
                ? _allCards
                : _allCards.Where(c => c.Fio.IndexOf(SearchText, StringComparison.OrdinalIgnoreCase) >= 0 ||
                                       (c.Position != null && c.Position.IndexOf(SearchText, StringComparison.OrdinalIgnoreCase) >= 0));

            foreach (var item in filtered)
            {
                Cards.Add(item);
            }
        }

        public void DeleteSelectedCard()
        {
            if (SelectedCard == null) return;

            _service.Delete(SelectedCard.Id);
            var index = _allCards.FindIndex(c => c.Id == SelectedCard.Id);
            if (index >= 0) _allCards.RemoveAt(index);

            SelectedCard = null;
            OnPropertyChanged(nameof(SelectedCard));
            ApplyFilter();
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

        private CardViewModel ToViewModel(Card card) => Mapping.Mapper.Map<CardViewModel>(card);
        private Card ToBusiness(CardViewModel card) => Mapping.Mapper.Map<Card>(card);
    }
}