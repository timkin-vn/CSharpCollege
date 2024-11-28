using CardFile.Business.Entities;
using CardFile.Business.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardFile.Wpf.ViewModels
{
    public class CardFileViewModel : INotifyPropertyChanged
    {
        private readonly CardFileService _service = new CardFileService();

        private CardViewModel _selectedCard;

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

        public CardFileViewModel()
        {
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

        private CardViewModel ToViewModel(Card card)
        {
            return new CardViewModel
            {
                Id = card.Id,
                FirstName = card.FirstName,
                MiddleName = card.MiddleName,
                LastName = card.LastName,
                BirthDate = card.BirthDate,
                PaymentAmount = card.PaymentAmount,
                ChildrenCount = card.ChildrenCount,
                LicenseNumber = card.LicenseNumber,
                LicenseName = card.LicenseName,
                IssuedLicense = card.IssuedLicense,
            };
        }

        private Card FromViewModel(CardViewModel card)
        {
            return new Card
            {
                Id = card.Id,
                FirstName = card.FirstName,
                MiddleName = card.MiddleName,
                LastName = card.LastName,
                BirthDate = card.BirthDate,
                PaymentAmount = card.PaymentAmount,
                ChildrenCount = card.ChildrenCount,
                LicenseNumber = card.LicenseNumber,
                LicenseName = card.LicenseName,
                IssuedLicense = card.IssuedLicense,
            };
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
