using CardFile.Business.Entities;
using CardFile.Business.Services;
using CardFile.Common.Infrastructure;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardFile.Wpf.ViewModels
{
    internal class CardFileViewModel : INotifyPropertyChanged
    {
        private readonly CardFileDataService _service = new CardFileDataService();

        private CardViewModel _selectedCard;

        private string _fileName;

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<CardViewModel> Cards { get; set; } = new ObservableCollection<CardViewModel>();

        public CardViewModel SelectedCard 
        { 
            get => _selectedCard; 
            set
            {
                _selectedCard = value;
                OnPropertyChanged(nameof(IsEditButtonEnabled));
            }
        }

        public bool IsEditButtonEnabled => SelectedCard != null;

        public string FileName
        {
            get => _fileName;
            set
            {
                _fileName = value;
                OnPropertyChanged(nameof(WindowTitle));
            }
        }

        public string WindowTitle => string.IsNullOrEmpty(FileName) ?
            "Картотека" :
            $"{Path.GetFileName(FileName)} - Картотека";

        public CardViewModel GetNewCardViewModel()
        {
            return new CardViewModel();
        }

        public CardViewModel GetSelectedCardViewModel()
        {
            return SelectedCard;
        }

        public void DeleteSelectedCard()
        {
            if (SelectedCard == null)
            {
                return;
            }

            _service.Delete(SelectedCard.Id);
            ShowAll();
        }

        public void Save(CardViewModel cardViewModel)
        {
            var card = FromViewModel(cardViewModel);
            card = _service.Save(card);
            var cardId = card?.Id ?? 0;
            ShowAll();

            if (cardId <= 0)
            {
                return;
            }

            var selectedCardViewModel = Cards.FirstOrDefault(c => c.Id == cardId);
            if (selectedCardViewModel == null)
            {
                return;
            }

            SelectedCard = selectedCardViewModel;
            OnPropertyChanged(nameof(SelectedCard));
        }

        public void SaveToFile(string fileName)
        {
            FileName = fileName;
            SaveImplementation();
        }

        public void SaveToFile()
        {
            SaveImplementation();
        }

        public void OpenFromFile(string fileName)
        {
            _service.OpenFromFile(fileName);
            FileName = fileName;
            ShowAll();
        }

        public void Initialized()
        {
            Mapping.Initialize();
        }

        public void Loaded()
        {
            ShowAll();
        }

        private void SaveImplementation()
        {
            _service.SaveToFile(FileName);
        }

        private void ShowAll()
        {
            Cards.Clear();

            foreach (var card in _service.GetAll())
            {
                Cards.Add(ToViewModel(card));
            }
        }

        private CardViewModel ToViewModel(Card card)
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
            //    SubordinatesCount = card.SubordinatesCount,
            //    PaymentAmount = card.PaymentAmount,
            //};
        }

        private Card FromViewModel(CardViewModel cardViewModel)
        {
            return Mapping.Mapper.Map<Card>(cardViewModel);
            //return new Card
            //{
            //    Id = cardViewModel.Id,
            //    FirstName = cardViewModel.FirstName,
            //    LastName = cardViewModel.LastName,
            //    MiddleName = cardViewModel.MiddleName,
            //    BirthDate = cardViewModel.BirthDate,
            //    Department = cardViewModel.Department,
            //    Position = cardViewModel.Position,
            //    SubordinatesCount = cardViewModel.SubordinatesCount,
            //    PaymentAmount = cardViewModel.PaymentAmount,
            //};
        }

        private void OnPropertyChanged(string  propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
