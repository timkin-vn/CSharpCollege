using CardFile.Business.Models;
using CardFile.Business.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
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
            return new CardViewModel
            {
                Id = card.Id,
                FirstName = card.FirstName,
                LastName = card.LastName,
                MiddleName = card.MiddleName,
                BirthDate = card.BirthDate,
                Department = card.Department,
                Position = card.Position,
                EmploymentDate = card.EmploymentDate,
                DismissalDate = card.DismissalDate,
                Salary = card.Salary,
            };
        }

        private Card ToBusiness(CardViewModel card)
        {
            return new Card
            {
                Id = card.Id,
                FirstName = card.FirstName,
                LastName = card.LastName,
                MiddleName = card.MiddleName,
                BirthDate = card.BirthDate,
                Department = card.Department,
                Position = card.Position,
                EmploymentDate = card.EmploymentDate,
                DismissalDate = card.DismissalDate,
                Salary = card.Salary,
            };
        }

        private void UpdateVisibility()
        {
            OnPropertyChanged(nameof(IsEditEnabled));
            OnPropertyChanged(nameof(IsDeleteEnabled));
        }
    }
}
