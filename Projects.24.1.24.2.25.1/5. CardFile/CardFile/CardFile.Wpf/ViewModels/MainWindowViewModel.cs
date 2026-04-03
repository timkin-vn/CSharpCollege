using CardFile.Business.Models;
using CardFile.Business.Services;
using CardFile.Common.Infrastructure;
using CardFile.Wpf.Infrastructure;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CardFile.Wpf.ViewModels
{
    /// <summary>
    /// Вьюмодель главного окна.
    /// </summary>
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly CardFileService _service = new CardFileService();

        public ObservableCollection<CardViewModel> Cards { get; } = new ObservableCollection<CardViewModel>();

        private CardViewModel _selectedCard;
        public CardViewModel SelectedCard
        {
            get => _selectedCard;
            set
            {
                if (_selectedCard != value)
                {
                    _selectedCard = value;
                    OnPropertyChanged(nameof(SelectedCard));
                    OnPropertyChanged(nameof(IsEditButtonEnabled));
                    OnPropertyChanged(nameof(IsDeleteButtonEnabled));
                }
            }
        }

        public bool IsEditButtonEnabled => SelectedCard != null;
        public bool IsDeleteButtonEnabled => SelectedCard != null;

        public MainWindowViewModel()
        {
            // Регистрируем конфигурацию маппингов для WPF
            MapperInitialization.PreRegister();
        }

        /// <summary>
        /// Вызывается после инициализации окна (из Window_Initialized).
        /// Создаёт экземпляр маппера на основе зарегистрированных конфигураций.
        /// </summary>
        public void InitializeMapper()
        {
            Mapping.Initialize();
        }

        public void WindowLoaded()
        {
            LoadAllData();
        }

        public CardViewModel GetSelectedCard() => SelectedCard;

        public CardViewModel GetNewCard()
        {
            return new CardViewModel
            {
                ManufactureDate = new DateTime(2024, 1, 1),
                ReceiptDate = DateTime.Today,
            };
        }

        public void SaveEditedCard(CardViewModel card)
        {
            if (card == null) throw new ArgumentNullException(nameof(card));

            var index = Cards.ToList().FindIndex(c => c.Id == card.Id);
            if (index < 0) throw new Exception("Карточка с таким Id не существует");

            var id = _service.Save(ToBusiness(card));
            if (id < 0)
                Cards.RemoveAt(index);
            else
                Cards[index].LoadViewModel(card);
        }

        public void SaveNewCard(CardViewModel card)
        {
            if (card == null) throw new ArgumentNullException(nameof(card));

            var newCard = new CardViewModel();
            newCard.LoadViewModel(card);

            var id = _service.Save(ToBusiness(card));
            if (id < 0) return;

            newCard.Id = id;
            Cards.Add(newCard);
        }

        public void DeleteSelectedCard()
        {
            if (SelectedCard == null) return;
            _service.Delete(SelectedCard.Id);
            var index = Cards.ToList().FindIndex(c => c.Id == SelectedCard.Id);
            if (index < 0) throw new Exception("Карточка с таким Id не существует");
            Cards.RemoveAt(index);
            SelectedCard = null;
        }

        private void LoadAllData()
        {
            var cards = _service.GetAll();
            Cards.Clear();
            foreach (var card in cards)
                Cards.Add(ToViewModel(card));
        }

        // Приватные методы маппинга через AutoMapper
        private CardViewModel ToViewModel(Card card) => Mapping.Mapper.Map<CardViewModel>(card);
        private Card ToBusiness(CardViewModel card) => Mapping.Mapper.Map<Card>(card);
    }
}