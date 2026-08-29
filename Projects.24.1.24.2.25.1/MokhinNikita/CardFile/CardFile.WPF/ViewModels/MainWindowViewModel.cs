using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardFile.Business.Models;
using CardFile.Business.Services;
using CardFile.Common.Infrastructure;
using CardFile.WPF.Infrastructure;

namespace CardFile.WPF.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly CardFileService _service = new CardFileService();
        public ObservableCollection<CardViewModel> Cards { get; set; } = new ObservableCollection<CardViewModel>();
        public ObservableCollection<string> Items { get; set; }
        public CardViewModel SelectedCard { get; set; }
        public bool IsEditButtonEnabled => SelectedCard != null;
        public bool IsDeleteButtonEnabled => SelectedCard != null;
        public bool Changed { get; private set; } = false;
        public string FileName { get; private set; }
        public string SearchText { get; set; }
        private string _selected;
        public string OptionSelected
        {
            get => _selected;
            set
            {
                _selected = value;
                OnPropertyChanged(nameof(OptionSelected));
            }
        }
        public string WindowTitle => string.IsNullOrEmpty(FileName) ? "Картотека" : $"Картотека: {Path.GetFileName(FileName)}";
        public MainWindowViewModel()
        {
            Items = new ObservableCollection<string>()
            {
                "Имя", "Фамилия", "Отчество", "Подразделение", "Должность"
            };
            OptionSelected = Items[0];
            MapperInitialization.Preregister();
        }
        public void WindowLoaded()
        {
            LoadAllData();
        }
        public CardViewModel GetSelectedCard()
        {
            return SelectedCard;
        }
        public void SelectionCard()
        {
            OnPropertyChanged(nameof(IsDeleteButtonEnabled));
            OnPropertyChanged(nameof(IsEditButtonEnabled));

        }
        public void LoadAllData()
        {
            var cards = _service.GetAll();
            Cards.Clear();
            foreach (var card in cards)
            {
                Cards.Add(ToViewModel(card));
            }
        }
        public void LoadSearchedData(string box)
        {
            var cards = _service.GetAll();
            Cards.Clear();
            foreach (var card in cards)
            {
                string field;
                switch (OptionSelected)
                {
                    case "Имя":
                        field = card.FirstName;
                        break;
                    case "Фамилия":
                        field = card.LastName;
                        break;
                    case "Отчество":
                        field = card.MiddleName;
                        break;
                    case "Подразделение":
                        field = card.Department;
                        break;
                    case "Должность":
                        field = card.Position;
                        break;
                    default:
                        throw new InvalidOperationException("Тип этого меню не существует");
                }
                if(field.StartsWith(box))
                {
                    Cards.Add(ToViewModel(card));
                }
            }
        }
        public void SearchCard(string box)
        {
            if(!string.IsNullOrWhiteSpace(box))
            {
                LoadSearchedData(box);
            }
            else
            {
                LoadAllData();
            }
            OnPropertyChanged(nameof(Cards));
        }
        public void SaveEditedCard(CardViewModel card)
        {
            var index = Cards.ToList().FindIndex(c => c.Id == card.Id);
            if(index < 0)
            {
                throw new IndexOutOfRangeException("Карточка с таким Id не существует");
            }
            var id = _service.Save(ToBusiness(card));
            // System.Diagnostics.Debug.WriteLine(id);
            if(id < 0)
            {
                Cards.RemoveAt(index);
            }
            else
            {
                Cards[index].LoadViewModel(card);
                //System.Diagnostics.Debug.WriteLine($"После маппинга: Cards[index].FirstName = {Cards[index].FirstName}");
                //System.Diagnostics.Debug.WriteLine($"После маппинга: Cards[index].Salary = {Cards[index].Salary}");
            }
            Changed = true;
        }
        public CardViewModel GetNewCard()
        {
            return new CardViewModel()
            {
                BirthDate = new DateTime(2000, 6, 15),
                EmploymentDate = new DateTime(2020, 6, 15)
            };
        }
        public void SaveNewCard(CardViewModel card)
        {
            var newCard = new CardViewModel();
            newCard.LoadViewModel(card);
            var id = _service.Save(ToBusiness(card));
            if(id < 0)
            {
                return;
            }
            
            newCard.Id = id;
            Cards.Add(newCard);
            Changed = true;
        }
        public void DeleteSelectedCard()
        {
            if(SelectedCard == null)
            {
                return;
            }
            _service.Delete(SelectedCard.Id);
            var index = Cards.ToList().FindIndex(c => c.Id == SelectedCard.Id);
            if(index < 0)
            {
                throw new IndexOutOfRangeException("Карточка с таким Id не существует");
            }
            Cards.RemoveAt(index);
            SelectedCard = null;
            OnPropertyChanged(nameof(SelectedCard));
            Changed = true;
        }

        public void SaveToFile(string fileName)
        {
            _service.SaveToFile(fileName);
            FileName = fileName;
            OnPropertyChanged(nameof(WindowTitle));
            Changed = false;
        }
        public void SaveToFile()
        {
            try
            {
                _service.SaveToFile(FileName);
            }catch (Exception)
            {
                FileName = null;
                throw;
            }
            
        }
        public void OpenFormFile(string fileName)
        {
            _service.OpenFromFile(fileName);
            LoadAllData();
            FileName = fileName;
            OnPropertyChanged(nameof(WindowTitle));
            Changed = false;
        }
        public void Initialized()
        {
            Mapping.Initialize();
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
            //    Salary = card.Salary
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
            //    Salary = card.Salary
            //};
        }
    }
}
