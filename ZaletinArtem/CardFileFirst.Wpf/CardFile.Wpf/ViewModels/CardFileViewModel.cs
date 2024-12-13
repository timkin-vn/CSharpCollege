using CardFile.Business.Entities;
using CardFile.Business.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.IO;
using Newtonsoft.Json;
using System.Xml;
using CardFile.DataAccess.Dtos;
using System.Collections.Generic;

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
                Title = card.Title,
                Author = card.Author,
                PublicationDate = card.PublicationDate,
                Genre = card.Genre,
                PageCount = card.PageCount,
                Price = card.Price,
            };
        }

        private Card FromViewModel(CardViewModel cardViewModel)
        {
            return new Card
            {
                Id = cardViewModel.Id,
                Title = cardViewModel.Title,
                Author = cardViewModel.Author,
                PublicationDate = cardViewModel.PublicationDate,
                Genre = cardViewModel.Genre,
                PageCount = cardViewModel.PageCount,
                Price = cardViewModel.Price,
            };
        }
        public void SaveToFile(string filePath)
        {
            var cardList = Cards.Select(card => new CardDto
            {
                Id = card.Id,
                Title = card.Title,
                Author = card.Author,
                PublicationDate = card.PublicationDate,
                Genre = card.Genre,
                PageCount = card.PageCount,
                Price = card.Price
            }).ToList();

            var json = JsonConvert.SerializeObject(cardList, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(filePath, json);
        }
        public void LoadFromFile(string filePath)
        {
            if (!File.Exists(filePath)) return;

            var json = File.ReadAllText(filePath);
            var cardList = JsonConvert.DeserializeObject<List<CardDto>>(json);

            Cards.Clear();
            foreach (var cardDto in cardList)
            {
                Cards.Add(new CardViewModel
                {
                    Id = cardDto.Id,
                    Title = cardDto.Title,
                    Author = cardDto.Author,
                    PublicationDate = cardDto.PublicationDate,
                    Genre = cardDto.Genre,
                    PageCount = cardDto.PageCount,
                    Price = cardDto.Price
                });
            }
        }

        private void OnPropertyChanged(string  propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
