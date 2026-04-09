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
        private readonly List<CardViewModel> _allCards = new List<CardViewModel>();
        private string _selectedDepartment = "Все подразделения";
        private string _selectedStatus = "Все";

        public ObservableCollection<CardViewModel> Cards { get; } = new ObservableCollection<CardViewModel>();

        public ObservableCollection<string> Departments { get; } = new ObservableCollection<string>();

        public ObservableCollection<string> Statuses { get; } = new ObservableCollection<string>
        {
            "Все",
            "Работают",
            "Уволены"
        };

        public CardViewModel SelectedCard { get; set; }

        public string SelectedDepartment
        {
            get => _selectedDepartment;
            set
            {
                _selectedDepartment = value ?? "Все подразделения";
                OnPropertyChanged(nameof(SelectedDepartment));
                ApplyFilter();
            }
        }

        public string SelectedStatus
        {
            get => _selectedStatus;
            set
            {
                _selectedStatus = value ?? "Все";
                OnPropertyChanged(nameof(SelectedStatus));
                ApplyFilter();
            }
        }

        public bool IsEditButtonEnabled => SelectedCard != null;

        public bool IsDeleteButtonEnabled => SelectedCard != null;

        public string FileName { get; private set; }

        public string WindowTitle => string.IsNullOrEmpty(FileName) ? "Картотека" : $"Картотека: {Path.GetFileName(FileName)}";

        public MainWindowViewModel()
        {
            MapperInitialization.PreRegister();
        }

        public void WindowLoaded()
        {
            Mapping.Initialize();
            LoadAllData();
        }

        public CardViewModel GetSelectedCard() => SelectedCard;

        public CardViewModel GetNewCard()
        {
            return new CardViewModel
            {
                BirthDate = new DateTime(2000, 6, 15),
                EmploymentDate = new DateTime(2020, 6, 15),
            };
        }

        public void SaveEditedCard(CardViewModel card)
        {
            var id = _service.Save(ToBusiness(card));
            if (id < 0)
            {
                return;
            }

            var item = _allCards.FirstOrDefault(c => c.Id == card.Id);
            if (item != null)
            {
                item.LoadViewModel(card);
            }

            UpdateDepartments();
            ApplyFilter();
        }

        public void SaveNewCard(CardViewModel card)
        {
            var id = _service.Save(ToBusiness(card));
            if (id < 0)
            {
                return;
            }

            var newCard = new CardViewModel();
            newCard.LoadViewModel(card);
            newCard.Id = id;

            _allCards.Add(newCard);
            UpdateDepartments();
            ApplyFilter();
        }

        public void DeleteSelectedCard()
        {
            if (SelectedCard == null)
            {
                return;
            }

            _service.Delete(SelectedCard.Id);
            _allCards.RemoveAll(c => c.Id == SelectedCard.Id);
            SelectedCard = null;
            UpdateDepartments();
            ApplyFilter();
            OnPropertyChanged(nameof(SelectedCard));
        }

        public void SelectionChanged()
        {
            OnPropertyChanged(nameof(IsDeleteButtonEnabled));
            OnPropertyChanged(nameof(IsEditButtonEnabled));
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

        public void OpenFromFile(string fileName)
        {
            _service.OpenFromFile(fileName);
            LoadAllData();
            FileName = fileName;
            OnPropertyChanged(nameof(WindowTitle));
        }

        private void LoadAllData()
        {
            _allCards.Clear();
            _allCards.AddRange(_service.GetAll().Select(ToViewModel));
            UpdateDepartments();
            ApplyFilter();
        }

        private void UpdateDepartments()
        {
            var previous = SelectedDepartment;
            Departments.Clear();
            Departments.Add("Все подразделения");

            foreach (var department in _allCards.Select(c => c.Department).Distinct().OrderBy(d => d))
            {
                Departments.Add(department);
            }

            if (Departments.Contains(previous))
            {
                _selectedDepartment = previous;
            }
            else
            {
                _selectedDepartment = "Все подразделения";
            }

            OnPropertyChanged(nameof(SelectedDepartment));
        }

        private void ApplyFilter()
        {
            var filtered = _allCards
                .Where(c => SelectedDepartment == "Все подразделения" || c.Department == SelectedDepartment)
                .Where(c =>
                    SelectedStatus == "Все" ||
                    (SelectedStatus == "Работают" && !c.DismissalDate.HasValue) ||
                    (SelectedStatus == "Уволены" && c.DismissalDate.HasValue))
                .ToList();

            Cards.Clear();
            foreach (var card in filtered)
            {
                Cards.Add(card);
            }
        }

        private CardViewModel ToViewModel(Card card)
        {
            return Mapping.Mapper.Map<CardViewModel>(card);
        }

        private Card ToBusiness(CardViewModel card)
        {
            return Mapping.Mapper.Map<Card>(card);
        }
    }
}
