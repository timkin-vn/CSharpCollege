using CardFile.Business.Models;
using CardFile.Business.Services;
using CardFile.Common.Infrastructure;
using CardFile.Wpf.Infrastructure;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace CardFile.Wpf.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private CardFileService _service = new CardFileService();

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
            LoadAllData();
        }

        public CardViewModel GetSelectedCard()
        {
            return SelectedCard;
        }

        public CardViewModel GetNewCard()
        {
            return new CardViewModel
            {
                Year = DateTime.Now.Year,
                LastServiceDate = DateTime.Now
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

            var id = _service.Save(ToBusiness(card));

            if (id < 0)
            {
                return;
            }

            newCard.Id = id;
            Cards.Add(newCard);
        }

        public void DeleteSelectedCard()
        {
            if (SelectedCard == null) return;

            _service.Delete(SelectedCard.Id);
            var index = Cards.ToList().FindIndex(c => c.Id == SelectedCard.Id);

            if (index >= 0)
            {
                Cards.RemoveAt(index);
                SelectedCard = null;
                OnPropertyChanged(nameof(SelectedCard));
                SelectionChanged();
            }
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

        private CardViewModel ToViewModel(Card card)
        {
            return Mapping.Mapper.Map<CardViewModel>(card);
        }

        private Card ToBusiness(CardViewModel card)
        {
            return Mapping.Mapper.Map<Card>(card);
        }

        public void ExportToFile(string path)
        {
            _service.SaveToFile(path, 1);
        }

        public void ImportFromFile(string path)
        {
            _service.LoadFromFile(path);
            LoadAllData(); 
        }

        public void SaveAs()
        {
            var sfd = new Microsoft.Win32.SaveFileDialog();

            sfd.Filter = "XML файл (*.xml)|*.xml|JSON файл (*.json)|*.json|Текстовый файл (*.txt)|*.txt";

            if (sfd.ShowDialog() == true)
            {
                _service.SaveToFile(sfd.FileName, sfd.FilterIndex);
            }
        }

        public void Open()
        {
            var ofd = new Microsoft.Win32.OpenFileDialog();
            ofd.Filter = "Данные автосалона (*.xml;*.json)|*.xml;*.json";

            if (ofd.ShowDialog() == true)
            {
                _service.LoadFromFile(ofd.FileName);

                WindowLoaded();
            }
        }

    }
}