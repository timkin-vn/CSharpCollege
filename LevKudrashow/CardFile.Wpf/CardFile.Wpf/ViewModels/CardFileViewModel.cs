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
    internal class CardFileViewModel : INotifyPropertyChanged
    {
        private readonly CardFileDataService _service = new CardFileDataService();

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
            return new CardViewModel
            {
                Id = card.Id,
                Name = card.Name,
                Description = card.Description,
                Street = card.Street,
                House = card.House,
                City = card.City,
                MailIndex = card.MailIndex,
                Rating = card.Rating,
                CounterReviews = card.CounterReviews,
                Status = card.Status,
            };
        }

        private Card FromViewModel(CardViewModel cardViewModel)
        {
            return new Card
            {
                Id = cardViewModel.Id,
                Name = cardViewModel.Name,
                Description = cardViewModel.Description,
                Street = cardViewModel.Street,
                House = cardViewModel.House,
                City = cardViewModel.City,
                MailIndex = cardViewModel.MailIndex,
                Rating = cardViewModel.Rating,
                CounterReviews = cardViewModel.CounterReviews,
                Status = cardViewModel.Status,
            };
        }

        private void OnPropertyChanged(string  propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
