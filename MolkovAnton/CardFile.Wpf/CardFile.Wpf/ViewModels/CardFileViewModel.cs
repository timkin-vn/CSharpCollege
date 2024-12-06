using CardFile.Business.Entities;
using CardFile.Business.Services;
using CardFile.Common.Infrastructure;
using CardFile.Wpf.Infrastructure;
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
    public class CardFileViewModel : INotifyPropertyChanged
    {
        private readonly CardFileService _service = new CardFileService();

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
                OnPropertyChanged(nameof(WindowHeader));
            }
        }

        public string WindowHeader => string.IsNullOrEmpty(FileName) ?
            "Картотека" :
            $"Картотека: {Path.GetFileName(FileName)}";

        public CardFileViewModel()
        {
            MapperInitialize.Initialize();
        }

        public void Initialized()
        {
            Mapping.Initialize();
            ShowAll();
        }

        public CardViewModel GetNewCard()
        {
            return new CardViewModel();
        }

        public CardViewModel GetSelectedCard()
        {
            return SelectedCard;
        }

        public bool Save(CardViewModel cardViewModel)
        {
            var card = FromViewModel(cardViewModel);
            card = _service.Save(card);
            var result = card != null;
            if (result)
            {
                ShowAll();
                var selectedViewModel = Cards.FirstOrDefault(c => c.Id == card.Id);
                if (selectedViewModel != null)
                {
                    SelectedCard = selectedViewModel;
                    OnPropertyChanged(nameof(SelectedCard));
                }
            }

            return result;
        }

        public bool DeleteSelected()
        {
            if (SelectedCard == null)
            {
                return false;
            }

            if (!_service.Delete(SelectedCard.Id))
            {
                return false;
            }

            ShowAll();
            return true;
        }

        public void SaveToFileAs(string fileName)
        {
            FileName = fileName;
            SaveToFileImplementation();
        }

        public void SaveToFile()
        {
            SaveToFileImplementation();
        }

        private void SaveToFileImplementation()
        {
            _service.SaveToFile(FileName);
        }

        public void OpenFile(string fileName)
        {
            _service.OpenFile(fileName);
            FileName = fileName;
            ShowAll();
        }

        private CardViewModel ToViewModel(Card card)
        {
            return Mapping.Mapper.Map<CardViewModel>(card);
            //return new CardViewModel
            //{
            //    Id = card.Id,
            //    FirstName = card.FirstName,
            //    MiddleName = card.MiddleName,
            //    LastName = card.LastName,
            //    BirthDate = card.BirthDate,
            //    PaymentAmount = card.PaymentAmount,
            //    ChildrenCount = card.ChildrenCount,
            //};
        }

        private Card FromViewModel(CardViewModel card)
        {
            return Mapping.Mapper.Map<Card>(card);
            //return new Card
            //{
            //    Id = card.Id,
            //    FirstName = card.FirstName,
            //    MiddleName = card.MiddleName,
            //    LastName = card.LastName,
            //    BirthDate = card.BirthDate,
            //    PaymentAmount = card.PaymentAmount,
            //    ChildrenCount = card.ChildrenCount,
            //};
        }

        private void ShowAll()
        {
            Cards.Clear();
            foreach (var card in _service.GetAll())
            {
                Cards.Add(ToViewModel(card));
            }
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
