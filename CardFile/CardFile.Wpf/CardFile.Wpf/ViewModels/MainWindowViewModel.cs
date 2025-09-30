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
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Diagnostics;

namespace CardFile.Wpf.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly CardFileService _service = new CardFileService();

        public ObservableCollection<CardViewModel> Cards { get; } = new ObservableCollection<CardViewModel>();

        public CardViewModel SelectedCard { get; set; }

        public bool IsEditEnabled => SelectedCard != null;

        public bool IsDeleteEnabled => SelectedCard != null;

        public string FileName { get; set; } = string.Empty;

        public string WindowTitle => string.IsNullOrEmpty(FileName) ? "Картотека" : $"Картотека: {Path.GetFileName(FileName)}";

        private readonly ICollectionView CardsView;
        public Collection<KeyValuePair<string, string>> SearchColumns { get; } = new Collection<KeyValuePair<string, string>>();

        private string _searchColumn = string.Empty;    

        private string _searchText = string.Empty;
        public string SearchColumn
        {
            get => _searchColumn;
            set
            {
                if (_searchColumn == value)
                {
                    return;
                }
                _searchColumn = value;
                OnPropertyChanged(nameof(SearchColumn));
                CardsView.Refresh();

            }
        }
        public string SearchText
        {
            get => _searchText;
            set
            {
                if (_searchText == value)
                {
                    return;
                }
                _searchText = value;
                OnPropertyChanged(nameof(SearchText));
                CardsView.Refresh();
            }
        }

        private bool FilterCards(object obj)
        {
            if (obj is CardViewModel cvm)
            {
                if (string.IsNullOrWhiteSpace(SearchText)) return true;

                var text = SearchText.ToLower();
                var propInfo = typeof(CardViewModel).GetProperty(SearchColumn);
                var value = propInfo?.GetValue(cvm)?.ToString()?.ToLower() ?? string.Empty;

                return value.Contains(text);
            }
            return false;
        }

        public MainWindowViewModel()
        {
            MapperRegistrator.Register();
            CardsView = CollectionViewSource.GetDefaultView(Cards);
            CardsView.Filter = FilterCards;
        }

        public void Initialized()
        {
            Mapping.Initialize();
        }

        public void Loaded()
        {
            LoadData();
        }

        public CardViewModel GetNewCard()
        {
            return new CardViewModel
            {
                BirthDate = new DateTime(2000, 1, 7),
                EmploymentDate = new DateTime(2020, 1, 7),
            };
        }

        public void InsertNewCard(CardViewModel card)
        {
            var newCard = new CardViewModel();
            newCard.LoadFromViewModel(card);
            var id = _service.Save(ToBusiness(newCard));
            newCard.Id = id;
            Cards.Add(newCard);
        }

        public CardViewModel GetSelectedCard()
        {
            return SelectedCard;
        }

        public void UpdateCard(CardViewModel card)
        {
            var index = Cards.ToList().FindIndex(c => c.Id == card.Id);
            if (index < 0)
            {
                return;
            }

            var id = _service.Save(ToBusiness(card));
            if (id < 0)
            {
                Cards.RemoveAt(index);
            }
            else
            {
                Cards[index].LoadFromViewModel(card);
            }
        }

        public void SelectionChanged()
        {
            UpdateVisibility();
        }

        public void DeleteSelectedCard()
        {
            if (SelectedCard == null)
            {
                return;
            }

            _service.Delete(SelectedCard.Id);
            Cards.Remove(SelectedCard);
            SelectedCard = null;
            UpdateVisibility();
        }

        public void OpenFile(string fileName)
        {
            _service.OpenFromFile(fileName);
            LoadData();

            FileName = fileName;
            OnPropertyChanged(nameof(WindowTitle));
        }

        public void SaveToFile(string fileName)
        {
            _service.SaveToFile(fileName);

            FileName = fileName;
            OnPropertyChanged(nameof(WindowTitle));
        }

        public void SaveToFile()
        {
            _service.SaveToFile(FileName);
        }

        private void LoadData()
        {
            var cards = _service.GetAll();
            Cards.Clear();

            foreach (var card in cards)
            {
                Cards.Add(FromBusiness(card));
            }
        }

        private CardViewModel FromBusiness(Card card)
        {
            return Mapping.Mapper.Map<CardViewModel>(card);
            //return new CardViewModel
            //{
            //    Id = card.Id,
            //    FirstName = card.FirstName,
            //    LastName = card.LastName,
            //    MiddleName = card.MiddleName,
            //    BirthDate = card.BirthDate,
            //    Department = card.Department,
            //    Position = card.Position,
            //    EmploymentDate = card.EmploymentDate,
            //    DismissalDate = card.DismissalDate,
            //    Salary = card.Salary,
            //};
        }

        private Card ToBusiness(CardViewModel card)
        {
            return Mapping.Mapper.Map<Card>(card);
            //return new Card
            //{
            //    Id = card.Id,
            //    FirstName = card.FirstName,
            //    LastName = card.LastName,
            //    MiddleName = card.MiddleName,
            //    BirthDate = card.BirthDate,
            //    Department = card.Department,
            //    Position = card.Position,
            //    EmploymentDate = card.EmploymentDate,
            //    DismissalDate = card.DismissalDate,
            //    Salary = card.Salary,
            //};
        }

        private void UpdateVisibility()
        {
            OnPropertyChanged(nameof(IsEditEnabled));
            OnPropertyChanged(nameof(IsDeleteEnabled));
        }
    }
}
