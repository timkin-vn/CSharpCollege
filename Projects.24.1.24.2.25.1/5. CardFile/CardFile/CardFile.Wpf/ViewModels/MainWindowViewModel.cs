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
        private readonly CarFileService _service = new CarFileService();

        public ObservableCollection<CardViewModel> Cars { get; } = new ObservableCollection<CardViewModel>();

        public CardViewModel SelectedCar { get; set; }

        public bool IsEditButtonEnabled => SelectedCar != null;

        public bool IsDeleteButtonEnabled => SelectedCar != null;

        public string FileName { get; private set; }

        public string WindowTitle => string.IsNullOrEmpty(FileName) ? "Автосалон" : $"Автосалон: {Path.GetFileName(FileName)}";

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

        public CardViewModel GetSelectedCar()
        {
            return SelectedCar;
        }

        public CardViewModel GetNewCar()
        {
            return new CardViewModel
            {
                ReleaseDate = new DateTime(2020, 1, 1),
                ArrivalDate = DateTime.Now,
            };
        }

        public void SaveEditedCar(CardViewModel car)
        {
            var index = Cars.ToList().FindIndex(c => c.Id == car.Id);

            if (index < 0)
            {
                throw new Exception("Автомобиль с таким Id не существует");
            }

            var id = _service.Save(ToBusiness(car));

            if (id < 0)
            {
                Cars.RemoveAt(index);
            }
            else
            {
                Cars[index].LoadViewModel(car);
            }
        }

        public void SaveNewCar(CardViewModel car)
        {
            var newCarVm = new CardViewModel();
            newCarVm.LoadViewModel(car);

            var id = _service.Save(ToBusiness(car));

            if (id < 0)
            {
                return;
            }

            newCarVm.Id = id;
            Cars.Add(newCarVm);
        }

        public void SelectionChanged()
        {
            OnPropertyChanged(nameof(IsDeleteButtonEnabled));
            OnPropertyChanged(nameof(IsEditButtonEnabled));
        }

        private void LoadAllData()
        {
            var cars = _service.GetAll();
            Cars.Clear();
            foreach (var car in cars)
            {
                Cars.Add(ToViewModel(car));
            }
        }

        public void DeleteSelectedCar()
        {
            if (SelectedCar == null)
            {
                return;
            }

            _service.Delete(SelectedCar.Id);
            var index = Cars.ToList().FindIndex(c => c.Id == SelectedCar.Id);

            if (index < 0)
            {
                throw new Exception("Автомобиль с таким Id не существует");
            }

            Cars.RemoveAt(index);
            SelectedCar = null;

            OnPropertyChanged(nameof(SelectedCar));
            OnPropertyChanged(nameof(IsDeleteButtonEnabled));
            OnPropertyChanged(nameof(IsEditButtonEnabled));
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

        private CardViewModel ToViewModel(Car car)
        {
            return Mapping.Mapper.Map<CardViewModel>(car);
        }

        private Car ToBusiness(CardViewModel carVm)
        {
            return Mapping.Mapper.Map<Car>(carVm);
        }
    }
}