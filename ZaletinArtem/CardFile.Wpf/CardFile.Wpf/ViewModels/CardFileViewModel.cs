using CardFile.Business.Entities;
using CardFile.Business.Services;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace CardFile.Wpf.ViewModels
{
    public class CardFileViewModel : INotifyPropertyChanged
    {
        private CardViewModel _selectedCard;

        public ObservableCollection<CardViewModel> Cards { get; set; }

        public CardViewModel SelectedCard
        {
            get => _selectedCard;
            set
            {
                _selectedCard = value;
                OnPropertyChanged(nameof(SelectedCard));
            }
        }

        public CardFileViewModel()
        {
            Cards = new ObservableCollection<CardViewModel>
            {
                new CardViewModel
                {
                    Id = 1,
                    Title = "Война и мир",
                    Author = "Лев Толстой",
                    PublicationDate = new DateTime(1869, 1, 1),
                    Genre = "Роман",
                    PageCount = 1225,
                    Price = 500m
                },
                new CardViewModel
                {
                    Id = 2,
                    Title = "Преступление и наказание",
                    Author = "Федор Достоевский",
                    PublicationDate = new DateTime(1866, 1, 1),
                    Genre = "Роман",
                    PageCount = 671,
                    Price = 450m
                },
                new CardViewModel
                {
                    Id = 3,
                    Title = "Мастер и Маргарита",
                    Author = "Михаил Булгаков",
                    PublicationDate = new DateTime(1967, 1, 1),
                    Genre = "Фантастика",
                    PageCount = 470,
                    Price = 600m
                }
            };
        }

        public void AddCard(CardViewModel card)
        {
            if (card != null)
                Cards.Add(card);
        }

        public void UpdateCard(CardViewModel updatedCard)
        {
            if (updatedCard == null || SelectedCard == null) return;

            var index = Cards.IndexOf(SelectedCard);
            if (index >= 0)
                Cards[index] = updatedCard;
        }

        public void RemoveCard(CardViewModel card)
        {
            if (card != null)
                Cards.Remove(card);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}