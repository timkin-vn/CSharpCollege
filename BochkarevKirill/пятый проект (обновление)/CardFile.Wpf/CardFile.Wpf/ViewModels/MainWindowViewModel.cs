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

        public bool IsEditEnabled => SelectedCard != null;
        public bool IsDeleteEnabled => SelectedCard != null;

        public string FileName { get; set; } = string.Empty;
        public string WindowTitle => string.IsNullOrEmpty(FileName) ? "Картотека авторемонта" : $"Картотека авторемонта: {Path.GetFileName(FileName)}";

        public MainWindowViewModel()
        {
            MapperRegistrator.Register();
        }

        public void Initialized()
        {
            Mapping.Initialize();
        }

        public void Loaded()
        {
            LoadData();
        }

        public CardViewModel GetNewCard()
        {
            return new CardViewModel
            {
                ArrivalDate = DateTime.Today
            };
        }

        // Добавленный метод для получения выбранной карточки
        public CardViewModel GetSelectedCard()
        {
            return SelectedCard;
        }

        // Добавленный метод для обработки изменения выбора
        public void SelectionChanged()
        {
            UpdateVisibility();
        }

        public void InsertNewCard(CardViewModel card)
        {
            var newCard = new CardViewModel();
            newCard.LoadFromViewModel(card, newCard.GetIsRepairCompleted());
            var id = _service.Save(ToBusiness(newCard));
            newCard.Id = id;
            Cards.Add(newCard);
        }

        public void UpdateCard(CardViewModel card)
        {
            var index = Cards.ToList().FindIndex(c => c.Id == card.Id);
            if (index < 0) return;

            var id = _service.Save(ToBusiness(card));
            if (id < 0)
            {
                Cards.RemoveAt(index);
            }
            else
            {
                Cards[index].LoadFromViewModel(card, Cards[index].GetIsRepairCompleted());
            }
        }

        public void DeleteSelectedCard()
        {
            if (SelectedCard == null) return;

            _service.Delete(SelectedCard.Id);
            Cards.Remove(SelectedCard);
            SelectedCard = null;
            UpdateVisibility();
        }

        public void OpenFile(string fileName)
        {
            _service.OpenFromFile(fileName);
            LoadData();
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
            if (!string.IsNullOrEmpty(FileName))
                _service.SaveToFile(FileName);
        }

        private void LoadData()
        {
            Cards.Clear();
            foreach (var card in _service.GetAll())
            {
                Cards.Add(FromBusiness(card));
            }
        }

        private CardViewModel FromBusiness(Card card)
        {
            return Mapping.Mapper.Map<CardViewModel>(card);
        }

        private Card ToBusiness(CardViewModel card)
        {
            return Mapping.Mapper.Map<Card>(card);
        }

        private void UpdateVisibility()
        {
            OnPropertyChanged(nameof(IsEditEnabled));
            OnPropertyChanged(nameof(IsDeleteEnabled));
        }
    }
}