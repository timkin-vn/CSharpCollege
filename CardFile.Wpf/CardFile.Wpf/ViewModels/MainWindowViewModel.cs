using CardFile.Business.Models;
using CardFile.Business.Services;
using CardFile.Common.Infrastructure;
using CardFile.Wpf.Infrastructure;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CardFile.Wpf.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly CardFileService _service = new CardFileService();

        public ObservableCollection<CardViewModel> Cards { get; } = new ObservableCollection<CardViewModel>();

        public CardViewModel SelectedCard { get; set; }

        public bool IsEditEnabled => SelectedCard != null;

        public bool IsDeleteEnabled => SelectedCard != null;

        public string FileName { get; private set; } = string.Empty;

        public string WindowTitle => string.IsNullOrEmpty(FileName) ? "Картотека" : $"Картотека: {Path.GetFileName(FileName)}";

        public MainWindowViewModel()
        {
            InitializeMapper.Register();
        }
        
        public void Initialized()
        {
            Mapping.Initialize();
        }

        public void WindowLoaded()
        {
            LoadAllData();
        }

        public CardViewModel GetNewCard()
        {
            return new CardViewModel
            {
                YearOfProduction = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day),
                DeliveryDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day),
            };
        }

        public CardViewModel GetSelectedCard()
        {
            return SelectedCard;
        }

        public void SaveNewCard(CardViewModel card)
        {
            var newCard = new CardViewModel();
            newCard.LoadViewModel(card);

            var id = _service.Save(ToBusiness(newCard));
            if (id < 0)
            {
                return;
            }

            newCard.Id = id;
            Cards.Add(newCard);
        }

        public void SaveEditedCard(CardViewModel card)
        {
            var id = _service.Save(ToBusiness(card));
            var index = Cards.ToList().FindIndex(c => c.Id == card.Id);

            if (index < 0)
            {
                throw new Exception("Карточка с таким id отсутствует во вью-модели");
            }

            if (id < 0)
            {
                Cards.RemoveAt(index);
            }
            else
            {
                Cards[index].LoadViewModel(card);
            }
        }

        public void DeleteSelected()
        {
            if (SelectedCard == null)
            {
                return;
            }

            _service.Delete(SelectedCard.Id);
            var index = Cards.ToList().FindIndex(c => c.Id == SelectedCard.Id);

            if (index < 0)
            {
                throw new Exception("Карточка с таким id отсутствует во вью-модели");
            }

            Cards.RemoveAt(index);
            SelectedCard = null;

            OnPropertyChanged(nameof(SelectedCard));
        }

        public void SelectionChanged()
        {
            OnPropertyChanged(nameof(IsDeleteEnabled));
            OnPropertyChanged(nameof(IsEditEnabled));
        }

        public void OpenFromFile(string fileName)
        {
            _service.OpenFromFile(fileName);
            LoadAllData();

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

        private void LoadAllData()
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
    }
}
