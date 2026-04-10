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

        public MainWindowViewModel()
        {
            MapperInitialization.PreRegister();
        }

        public void WindowLoaded()
        {
            Mapping.Initialize();
            LoadDefaultFileOrSeedData();
        }

        public CardViewModel GetSelectedCard()
        {
            return SelectedCard;
        }

        public CardViewModel GetNewCard()
        {
            return new CardViewModel
            {
                ReleaseDate = new DateTime(2020, 1, 1),
                PurchaseDate = DateTime.Today,
                PersonalRating = 8,
                Price = 1999m,
                IsDigital = true,
                IsNotCompleted = true
            };
        }

        public void SaveEditedCard(CardViewModel card)
        {
            var index = Cards.ToList().FindIndex(c => c.Id == card.Id);

            if (index < 0)
            {
                throw new Exception("Карточка с таким Id не существует");
            }

            var id = _service.Save(ToBusiness(card));

            if (id < 0)
            {
                Cards.RemoveAt(index);
                return;
            }

            Cards[index].LoadViewModel(card);
            SaveToCurrentOrDefaultFile();
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
            SaveToCurrentOrDefaultFile();
        }

        public void DeleteSelectedCard()
        {
            if (SelectedCard == null)
            {
                return;
            }

            var index = Cards.ToList().FindIndex(c => c.Id == SelectedCard.Id);

            if (index < 0)
            {
                throw new Exception("Карточка с таким Id не существует");
            }

            if (!_service.Delete(SelectedCard.Id))
            {
                return;
            }

            Cards.RemoveAt(index);
            SelectedCard = null;
            SaveToCurrentOrDefaultFile();

            OnPropertyChanged(nameof(SelectedCard));
        }

        public void SelectionChanged()
        {
            OnPropertyChanged(nameof(IsDeleteButtonEnabled));
            OnPropertyChanged(nameof(IsEditButtonEnabled));
        }

        public void OpenFile(string filePath)
        {
            _service.LoadFromFile(filePath);
            LoadAllData();
        }

        public void SaveFile()
        {
            SaveToFile(GetCurrentFileOrDefaultPath());
        }

        public void SaveFileAs(string filePath)
        {
            SaveToFile(filePath);
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

        private CardViewModel ToViewModel(Card card)
        {
            return Mapping.Mapper.Map<CardViewModel>(card);
        }

        private Card ToBusiness(CardViewModel card)
        {
            return Mapping.Mapper.Map<Card>(card);
        }

        private void LoadDefaultFileOrSeedData()
        {
            var defaultFilePath = GetDefaultFilePath();
            if (File.Exists(defaultFilePath))
            {
                _service.LoadFromFile(defaultFilePath);
            }

            LoadAllData();
        }

        private void SaveToCurrentOrDefaultFile()
        {
            SaveToFile(GetCurrentFileOrDefaultPath());
        }

        private void SaveToFile(string filePath)
        {
            _service.SaveToFile(filePath);
        }

        private string GetCurrentFileOrDefaultPath()
        {
            return _service.GetCurrentFilePath() ?? GetDefaultFilePath();
        }

        private string GetDefaultFilePath()
        {
            var folderPath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                "CardFile");

            return Path.Combine(folderPath, "cards.json");
        }
    }
}
