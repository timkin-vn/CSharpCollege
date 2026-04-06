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
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly CardFileService _service = new CardFileService();
        private string _fileName;

        public MainWindowViewModel()
        {
            // 1. Регистрируем маппинги из бизнес-слоя (Card <-> CardDto)
            CardFile.Business.Infrastructure.MapperInitialization.PreRegister();

            // 2. Регистрируем маппинги из WPF-слоя (Card <-> CardViewModel)
            CardFile.Wpf.Infrastructure.MapperInitialization.PreRegister();

            // 3. Только ТЕПЕРЬ вызываем финальную инициализацию
            if (Mapping.Mapper == null)
            {
                Mapping.Initialize();
            }

            // Инициализация данных
            Initialized();
        }


        public ObservableCollection<CardViewModel> Cards { get; set; } = new ObservableCollection<CardViewModel>();

        public CardViewModel SelectedCard { get; set; }

        public string FileName
        {
            get => _fileName;
            set { _fileName = value; OnPropertyChanged(); OnPropertyChanged(nameof(WindowTitle)); }
        }

        public string WindowTitle => string.IsNullOrEmpty(FileName) ? "Моя Медиатека" : $"Картотека - {Path.GetFileName(FileName)}";

        public bool IsEditButtonEnabled => SelectedCard != null;
        public bool IsDeleteButtonEnabled => SelectedCard != null;

        public void Initialized()
        {
            RefreshData();
        }

        private void RefreshData()
        {
            Cards.Clear();
            var data = _service.GetAll();
            foreach (var item in data)
            {
                Cards.Add(Mapping.Mapper.Map<CardViewModel>(item));
            }
        }
        public CardViewModel GetNewCard() => new CardViewModel();

        public CardViewModel GetSelectedCard() => SelectedCard;

        public void SaveNewCard(CardViewModel editVm)
        {
            var businessModel = Mapping.Mapper.Map<Card>(editVm);
            _service.Save(businessModel);
            RefreshData();
        }

        public void SaveEditedCard(CardViewModel editVm)
        {
            var businessModel = Mapping.Mapper.Map<Card>(editVm);
            businessModel.Id = SelectedCard.Id;
            _service.Save(businessModel);
            RefreshData();
        }
        public void DeleteSelectedCard()
        {
            if (SelectedCard != null)
            {
                _service.Delete(SelectedCard.Id);
                RefreshData();
            }
        }

        public void SaveToFile(string path = null)
        {
            var targetPath = path ?? FileName;
            _service.SaveToFile(targetPath);
            FileName = targetPath;
        }

        public void OpenFromFile(string path)
        {
            _service.OpenFromFile(path);
            FileName = path;
            RefreshData();
        }

        public void SelectionChanged()
        {
            OnPropertyChanged(nameof(IsEditButtonEnabled));
            OnPropertyChanged(nameof(IsDeleteButtonEnabled));
        }
        public void WindowLoaded()
        {
        }

    }
}
