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
        public CardViewModel SelectedCard { get; set; }

        public bool IsEditButtonEnabled => SelectedCard != null;
        public bool IsDeleteButtonEnabled => SelectedCard != null;
        public string FileName { get; private set; }

        // Изменяем заголовок окна: вместо "Картотека" пишем "Книжный магазин"
        public string WindowTitle => string.IsNullOrEmpty(FileName) ?
            "Книжный магазин" : $"Книжный магазин: {Path.GetFileName(FileName)}";

        public MainWindowViewModel()
        {
            MapperInitialization.PreRegister();
        }

        public void WindowLoaded() { LoadAllData(); }
        public void Initialized() { Mapping.Initialize(); }
        public CardViewModel GetSelectedCard() => SelectedCard;

        // Настраиваем данные для новой книги по умолчанию
        public CardViewModel GetNewCard()
        {
            return new CardViewModel
            {
                FirstName = "Название книги", // Вместо Имени
                LastName = "Автор",           // Вместо Фамилии
                Department = "Жанр",         // Вместо Отдела
                Salary = 0,                  // Цена 0
                BirthDate = DateTime.Today,   // Дата издания сегодня
                EmploymentDate = DateTime.Today
            };
        }

        public void SaveEditedCard(CardViewModel card)
        {
            var index = Cards.ToList().FindIndex(c => c.Id == card.Id);
            if (index < 0) throw new Exception("Книга с таким Id не найдена");

            var id = _service.Save(ToBusiness(card));
            if (id < 0) Cards.RemoveAt(index);
            else Cards[index].LoadViewModel(card);
        }

        public void SaveNewCard(CardViewModel card)
        {
            var newCard = new CardViewModel();
            newCard.LoadViewModel(card);
            var id = _service.Save(ToBusiness(card));
            if (id < 0) return;
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
            foreach (var card in cards) Cards.Add(ToViewModel(card));
        }

        public void DeleteSelectedCard()
        {
            if (SelectedCard == null) return;
            _service.Delete(SelectedCard.Id);
            var index = Cards.ToList().FindIndex(c => c.Id == SelectedCard.Id);
            if (index >= 0) Cards.RemoveAt(index);
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
            FileName = fileName;
            OnPropertyChanged(nameof(WindowTitle));
        }

        private CardViewModel ToViewModel(Card card) => Mapping.Mapper.Map<CardViewModel>(card);
        private Card ToBusiness(CardViewModel card) => Mapping.Mapper.Map<Card>(card);
    }
}
