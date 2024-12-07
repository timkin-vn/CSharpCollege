using CardFile.Business.Entities;
using CardFile.Business.Services;
using CardFile.Common.Infrastructure;
using CardFile.Wpf.Infrastructure;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardFile.Wpf.ViewModels
{
    public class CardProductsFileViewModel : INotifyPropertyChanged
    {
        private readonly CardProductsFileService _service = new CardProductsFileService();

        private CardProductsViewModel _selectedCard;

        private string _fileName;

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<CardProductsViewModel> Cards { get; set; } = new ObservableCollection<CardProductsViewModel>();

        public CardProductsViewModel SelectedCard
        {
            get => _selectedCard;
            set
            {
                _selectedCard = value;
                OnPropertyChanged(nameof(IsEditButtonEnabled));
            }
        }

        public bool IsEditButtonEnabled => SelectedCard != null;

        public string FileName
        {
            get => _fileName;
            set
            {
                _fileName = value;
                OnPropertyChanged(nameof(WindowHeader));
            }
        }

        public string WindowHeader => string.IsNullOrEmpty(FileName) ?
            "Storage" :
            $"Storage: {Path.GetFileName(FileName)}";

        public CardProductsFileViewModel()
        {
            MapperInitialize.Initialize();
        }

        public void Initialized()
        {
           Mapping.Initialize();
           ShowAll();
        }

        public CardProductsViewModel GetNewCard()
        {
            return new CardProductsViewModel();
        }

        public CardProductsViewModel GetSelectedCard()
        {
            return SelectedCard;
        }

        public bool Save(CardProductsViewModel cardViewModel)
        {
            var card = FromViewModel(cardViewModel);
            card = _service.Save(card);
            var result = card != null;
            if (result)
            {
                ShowAll();
                var selectedViewModel = Cards.FirstOrDefault(c => c.Id == card.Id);
                if (selectedViewModel != null)
                {
                    SelectedCard = selectedViewModel;
                    OnPropertyChanged(nameof(SelectedCard));
                }
            }

            return result;
        }

        public bool DeleteSelected()
        {
            if (SelectedCard == null)
            {
                return false;
            }

            if (!_service.Delete(SelectedCard.Id))
            {
                return false;
            }

            ShowAll();
            return true;
        }

        public void SaveToFileAs(string fileName)
        {
            FileName = fileName;
            SaveToFileImplementation();
        }

        public void SaveToFile()
        {
            SaveToFileImplementation();
        }

        private void SaveToFileImplementation()
        {
            _service.SaveToFile(FileName);
        }

        public void OpenFile(string fileName)
        {
            _service.OpenFile(fileName);
            FileName = fileName;
            ShowAll();
        }

        private CardProductsViewModel ToViewModel(CardProducts card)
        {
            return Mapping.Mapper.Map<CardProductsViewModel>(card);
            /*{
                Id = card.Id,

                NameProducts = card.NameProducts,

                TypeProducts = card.TypeProducts,

                ShirtProducts = card.ShirtProducts,

                SectionProducts = card.SectionProducts,

                DateExpiration = card.DateExpiration,

                DateManufacture = card.DateManufacture,

                PriceOneProducts = card.PriceOneProducts,

                CountProducts = card.CountProducts,
            };*/
        }

        private CardProducts FromViewModel(CardProductsViewModel card)
        {
            return Mapping.Mapper.Map<CardProducts>(card);
            /*{
                Id = card.Id,

                NameProducts = card.NameProducts,

                TypeProducts = card.TypeProducts,

                ShirtProducts = card.ShirtProducts,

                SectionProducts = card.SectionProducts,

                DateExpiration = card.DateExpiration,

                DateManufacture = card.DateManufacture,

                PriceOneProducts = card.PriceOneProducts,

                CountProducts = card.CountProducts
            };*/
        }

        private void ShowAll()
        {
            Cards.Clear();
            foreach (var card in _service.GetAll())
            {
                Cards.Add(ToViewModel(card));
            }
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
