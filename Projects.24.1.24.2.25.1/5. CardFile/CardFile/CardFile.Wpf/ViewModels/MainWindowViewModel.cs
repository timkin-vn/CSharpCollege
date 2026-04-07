using CardFile.Business.Models;
using CardFile.Business.Services;
using CardFile.Common.Infrastructure;
using CardFile.Wpf.Infrastructure;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;

namespace CardFile.Wpf.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly CardFileService _service = new CardFileService();

        public ObservableCollection<CardViewModel> Cards { get; } = new ObservableCollection<CardViewModel>();

        public ObservableCollection<string> Heroes { get; } = new ObservableCollection<string>();

        public ObservableCollection<string> Items { get; } = new ObservableCollection<string>();

        public ObservableCollection<string> Neutrals { get; } = new ObservableCollection<string>();

        public CardViewModel SelectedCard { get; set; }

        public bool IsEditButtonEnabled => SelectedCard != null;

        public bool IsDeleteButtonEnabled => SelectedCard != null;

        public string FileName { get; private set; }

        public string WindowTitle => string.IsNullOrEmpty(FileName) ? "Сборки Dota" : $"Сборки Dota: {Path.GetFileName(FileName)}";

        public MainWindowViewModel()
        {
            MapperInitialization.PreRegister();
        }

        public void WindowLoaded()
        {
            LoadAllData();
            LoadCatalogs();
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
            var hero = Heroes.FirstOrDefault();
            var neutral = Neutrals.FirstOrDefault();
            var item = Items.FirstOrDefault();

            return new CardViewModel
            {
                Hero = hero,
                Slot1 = item,
                Slot2 = item,
                Slot3 = item,
                Slot4 = item,
                Slot5 = item,
                Slot6 = item,
                Neutral = neutral,
            };
        }

        public void SaveEditedCard(CardViewModel card)
        {
            var index = Cards.ToList().FindIndex(c => c.Id == card.Id);

            if (index < 0)
            {
                throw new Exception("Карточка с таким Id не существует");
            }

            EnsureCatalogContains(card);
            var id = _service.Save(ToBusiness(card));

            if (id < 0)
            {
                Cards.RemoveAt(index);
            }
            else
            {
                Cards[index].LoadViewModel(card);
            }
        }

        public void SaveNewCard(CardViewModel card)
        {
            var newCard = new CardViewModel();
            newCard.LoadViewModel(card);

            EnsureCatalogContains(card);
            var id = _service.Save(ToBusiness(card));

            if (id < 0)
            {
                return;
            }

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
            var cards = _service.GetAll();
            Cards.Clear();
            foreach (var card in cards)
            {
                Cards.Add(ToViewModel(card));
            }
        }

        private void LoadCatalogs()
        {
            Heroes.Clear();
            foreach (var hero in _service.GetHeroes().OrderBy(x => x))
            {
                Heroes.Add(hero);
            }

            Items.Clear();
            foreach (var item in _service.GetItems().OrderBy(x => x))
            {
                Items.Add(item);
            }

            Neutrals.Clear();
            foreach (var neutral in _service.GetNeutrals().OrderBy(x => x))
            {
                Neutrals.Add(neutral);
            }
        }

        public void AddHeroToDatabase(string name)
        {
            if (_service.AddHero(name) && !Heroes.Contains(name))
            {
                Heroes.Add(name);
            }
        }

        public void AddItemToDatabase(string name)
        {
            if (_service.AddItem(name) && !Items.Contains(name))
            {
                Items.Add(name);
            }
        }

        public void AddNeutralToDatabase(string name)
        {
            if (_service.AddNeutral(name) && !Neutrals.Contains(name))
            {
                Neutrals.Add(name);
            }
        }

        public void DeleteSelectedCard()
        {
            if (SelectedCard == null)
            {
                return;
            }

            _service.Delete(SelectedCard.Id);
            var index = Cards.ToList().FindIndex(c => c.Id == SelectedCard.Id);

            if (index < 0)
            {
                throw new Exception("Карточка с таким Id не существует");
            }

            Cards.RemoveAt(index);
            SelectedCard = null;

            OnPropertyChanged(nameof(SelectedCard));
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
            LoadCatalogs();

            FileName = fileName;
            OnPropertyChanged(nameof(WindowTitle));
        }

        public void SaveToFile()
        {
            try
            {
                _service.SaveToFile(FileName);
            }
            catch
            {
                FileName = null;
                throw;
            }
        }

        private void EnsureCatalogContains(CardViewModel card)
        {
            _service.AddHero(card.Hero);

            _service.AddItem(card.Slot1);
            _service.AddItem(card.Slot2);
            _service.AddItem(card.Slot3);
            _service.AddItem(card.Slot4);
            _service.AddItem(card.Slot5);
            _service.AddItem(card.Slot6);

            _service.AddNeutral(card.Neutral);

            LoadCatalogs();
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
            //    MiddleName = card.MiddleName,
            //    LastName = card.LastName,
            //    BirthDate = card.BirthDate,
            //    Department = card.Department,
            //    Position = card.Position,
            //    EmploymentDate = card.EmploymentDate,
            //    DismissalDate = card.DismissalDate,
            //    Salary = card.Salary,
            //};
        }
    }
}
