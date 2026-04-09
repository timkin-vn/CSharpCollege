using CardFile.Business.Models;
using CardFile.Business.Services;
using CardFile.Common.Infrastructure;
using CardFile.Wpf.Infrastructure;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;

namespace CardFile.Wpf.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly CardFileService _service = new CardFileService();

        public ObservableCollection<CardViewModel> Cards { get; } = new ObservableCollection<CardViewModel>();

        public CardViewModel SelectedCard { get; set; }

        public bool IsEditButtonEnabled => SelectedCard != null;

        public bool IsDeleteButtonEnabled => SelectedCard != null;

        public bool IsToggleDoneButtonEnabled => SelectedCard != null;

        public bool IsTogglePinnedButtonEnabled => SelectedCard != null;

        public string ToggleDoneButtonText
            => SelectedCard == null
                ? "Выполнено"
                : (SelectedCard.IsDone ? "Снять выполнение" : "Отметить выполненной");

        public string TogglePinnedButtonText
            => SelectedCard == null
                ? "Закрепить"
                : (SelectedCard.IsPinned ? "Открепить" : "Закрепить");

        public string FileName { get; private set; }

        public string WindowTitle => string.IsNullOrEmpty(FileName) ? "Менеджер заметок" : $"Менеджер заметок: {Path.GetFileName(FileName)}";

        public MainWindowViewModel()
        {
            MapperInitialization.PreRegister();
        }

        public void WindowLoaded()
        {
            LoadAllData();
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
            return new CardViewModel
            {
                CreatedAt = DateTime.Today,
                Category = "Общее",
                IsDone = false,
                IsPinned = false,
            };
        }

        public void SaveEditedCard(CardViewModel card)
        {
            var index = Cards.ToList().FindIndex(c => c.Id == card.Id);

            if (index < 0)
            {
                throw new Exception("Заметка с таким Id не существует");
            }

            var id = _service.Save(ToBusiness(card));

            if (id < 0)
            {
                Cards.RemoveAt(index);
            }
            else
            {
                Cards[index].LoadViewModel(card);
                OnPropertyChanged(nameof(ToggleDoneButtonText));
                OnPropertyChanged(nameof(TogglePinnedButtonText));
            }
        }

        public void SaveNewCard(CardViewModel card)
        {
            var newCard = new CardViewModel();
            newCard.LoadViewModel(card);

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
            OnPropertyChanged(nameof(IsToggleDoneButtonEnabled));
            OnPropertyChanged(nameof(IsTogglePinnedButtonEnabled));
            OnPropertyChanged(nameof(ToggleDoneButtonText));
            OnPropertyChanged(nameof(TogglePinnedButtonText));
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
                throw new Exception("Заметка с таким Id не существует");
            }

            Cards.RemoveAt(index);
            SelectedCard = null;

            OnPropertyChanged(nameof(SelectedCard));
            SelectionChanged();
        }

        public void ToggleDoneSelectedCard()
        {
            if (SelectedCard == null)
            {
                return;
            }

            SelectedCard.ToggleDone();
            _service.Save(ToBusiness(SelectedCard));

            OnPropertyChanged(nameof(ToggleDoneButtonText));
        }

        public void TogglePinnedSelectedCard()
        {
            if (SelectedCard == null)
            {
                return;
            }

            SelectedCard.TogglePinned();
            _service.Save(ToBusiness(SelectedCard));

            OnPropertyChanged(nameof(TogglePinnedButtonText));
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

            FileName = fileName;
            OnPropertyChanged(nameof(WindowTitle));
        }

        public void SaveToFile()
        {
            try
            {
                _service.SaveToFile(FileName);
            }
            catch (Exception)
            {
                FileName = null;
                throw;
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