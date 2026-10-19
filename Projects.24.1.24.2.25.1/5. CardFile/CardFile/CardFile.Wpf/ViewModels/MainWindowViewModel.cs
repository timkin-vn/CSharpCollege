
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

        public ObservableCollection<CardViewModel> Cards { get; } =
            new ObservableCollection<CardViewModel>();

        private CardViewModel _selectedCard;

        public CardViewModel SelectedCard
        {
            get => _selectedCard;
            set
            {
                _selectedCard = value;
                OnPropertyChanged(nameof(SelectedCard));
                OnPropertyChanged(nameof(IsEditButtonEnabled));
                OnPropertyChanged(nameof(IsDeleteButtonEnabled));
            }
        }

        public bool IsEditButtonEnabled => SelectedCard != null;

        public bool IsDeleteButtonEnabled => SelectedCard != null;

        public string FileName { get; private set; }

        public string WindowTitle =>
            string.IsNullOrEmpty(FileName)
                ? "Картотека автомобилей"
                : $"Картотека автомобилей: {Path.GetFileName(FileName)}";

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
                AddedDate = DateTime.Today,
                SoldDate = null,
                Year = DateTime.Today.Year,
                Mileage = 0,
                Price = 0,
                EngineVolume = 1.6m,
                HasAirConditioning = false
            };
        }

        public void SaveEditedCard(CardViewModel card)
        {
            int index = Cards.ToList().FindIndex(c => c.Id == card.Id);

            if (index < 0)
                throw new Exception("Карточка с таким Id не существует.");

            int id = _service.Save(ToBusiness(card));

            if (id < 0)
                throw new Exception("Не удалось сохранить изменения карточки.");

            Cards[index].LoadViewModel(card);
            SelectedCard = Cards[index];
        }

        public void SaveNewCard(CardViewModel card)
        {
            int id = _service.Save(ToBusiness(card));

            if (id < 0)
                throw new Exception("Не удалось создать карточку автомобиля.");

            var newCard = new CardViewModel();
            newCard.LoadViewModel(card);
            newCard.Id = id;

            Cards.Add(newCard);
            SelectedCard = newCard;
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

            foreach (Card card in cards)
                Cards.Add(ToViewModel(card));
        }

        public void DeleteSelectedCard()
        {
            if (SelectedCard == null)
                return;

            int id = SelectedCard.Id;

            if (!_service.Delete(id))
                throw new Exception("Не удалось удалить карточку автомобиля.");

            CardViewModel item = Cards.FirstOrDefault(c => c.Id == id);

            if (item != null)
                Cards.Remove(item);

            SelectedCard = null;
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

            SelectedCard = null;
            FileName = fileName;
            OnPropertyChanged(nameof(WindowTitle));
        }

        public void SaveToFile()
        {
            if (string.IsNullOrWhiteSpace(FileName))
                throw new InvalidOperationException(
                    "Сначала выберите файл для сохранения.");

            _service.SaveToFile(FileName);
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
