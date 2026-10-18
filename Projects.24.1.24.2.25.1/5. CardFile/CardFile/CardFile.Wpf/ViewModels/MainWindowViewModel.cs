
using CardFile.Business.Models;
using CardFile.Business.Services;
using CardFile.Common.Infrastructure;
using CardFile.Wpf.Infrastructure;
using System;
using System.Collections.Generic;
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
        private string _searchText = "";

        public ObservableCollection<CardViewModel> Cards { get; }
            = new ObservableCollection<CardViewModel>();

        public ICollectionView CardsView { get; }

        public CardViewModel SelectedCard { get; set; }

        public bool IsEditButtonEnabled => SelectedCard != null;

        public bool IsDeleteButtonEnabled => SelectedCard != null;

        public string FileName { get; private set; }

        public string WindowTitle =>
            string.IsNullOrEmpty(FileName)
                ? "Картотека"
                : $"Картотека: {Path.GetFileName(FileName)}";

        public string SearchText
        {
            get { return _searchText; }
            set
            {
                if (_searchText == value)
                    return;

                _searchText = value ?? "";
                OnPropertyChanged(nameof(SearchText));

                CardsView.Refresh();
            }
        }

        public MainWindowViewModel()
        {
            MapperInitialization.PreRegister();

            CardsView = CollectionViewSource.GetDefaultView(Cards);
            CardsView.Filter = FilterCard;
        }

        private bool FilterCard(object item)
        {
            CardViewModel card = item as CardViewModel;

            if (card == null)
                return false;

            string query = (SearchText ?? "").Trim();

            if (query.Length == 0)
                return true;

            return Contains(card.Id.ToString(), query)
                || Contains(card.FirstName, query)
                || Contains(card.MiddleName, query)
                || Contains(card.LastName, query)
                || Contains(card.Fio, query)
                || Contains(card.Department, query)
                || Contains(card.Position, query);
        }

        private static bool Contains(string value, string query)
        {
            return !string.IsNullOrEmpty(value)
                && value.IndexOf(query, StringComparison.CurrentCultureIgnoreCase) >= 0;
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
                BirthDate = new DateTime(2000, 6, 15),
                EmploymentDate = new DateTime(2020, 6, 15),
            };
        }

        public void SaveEditedCard(CardViewModel card)
        {
            int index = Cards.ToList().FindIndex(c => c.Id == card.Id);

            if (index < 0)
                throw new Exception("Карточка с таким Id не существует");

            int id = _service.Save(ToBusiness(card));

            if (id < 0)
            {
                Cards.RemoveAt(index);
            }
            else
            {
                Cards[index].LoadViewModel(card);
                CardsView.Refresh();
            }
        }

        public void SaveNewCard(CardViewModel card)
        {
            var newCard = new CardViewModel();
            newCard.LoadViewModel(card);

            int id = _service.Save(ToBusiness(card));

            if (id < 0)
                return;

            newCard.Id = id;
            Cards.Add(newCard);
        }

        public void SelectionChanged()
        {
            OnPropertyChanged(nameof(IsDeleteButtonEnabled));
            OnPropertyChanged(nameof(IsEditButtonEnabled));
        }

        private void LoadAllData()
        {
            IEnumerable<Card> cards = _service.GetAll();

            Cards.Clear();

            foreach (Card card in cards)
                Cards.Add(ToViewModel(card));

            CardsView.Refresh();
        }

        public void DeleteSelectedCard()
        {
            if (SelectedCard == null)
                return;

            int selectedId = SelectedCard.Id;

            _service.Delete(selectedId);

            int index = Cards.ToList().FindIndex(c => c.Id == selectedId);

            if (index < 0)
                throw new Exception("Карточка с таким Id не существует");

            Cards.RemoveAt(index);
            SelectedCard = null;

            OnPropertyChanged(nameof(SelectedCard));
            SelectionChanged();
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
