using CardFile.Business.Entities;
using CardFile.Business.Services;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

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
            return new CardViewModel
            {
                Title = string.Empty,
                Author = string.Empty,
                PublicationDate = DateTime.Now,
                Genre = string.Empty,
                Price = 0
            };
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
                Title = card.Title,
                Author = card.Author,
                PublicationDate = card.PublicationDate,
                Genre = card.Genre,
                PageCount = card.PageCount,
                Price = card.Price
            };
        }

        private Card FromViewModel(CardViewModel viewModel)
        {
            return new Card
            {
                Id = viewModel.Id,
                Title = viewModel.Title,
                Author = viewModel.Author,
                PublicationDate = viewModel.PublicationDate,
                Genre = viewModel.Genre,
                PageCount = viewModel.PageCount,
                Price = viewModel.Price
            };
        }

        private void OnPropertyChanged(string  propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
