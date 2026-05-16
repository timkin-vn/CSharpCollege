using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LimerList.Business.Models;
using LimerList.Business.Services;
using LimerList.Common.Infrastructure;
using LimerList.WPF.Infrastructure;

namespace LimerList.ViewModels
{
    public sealed class MainViewModel : ViewModelBase
    {
        private readonly LimerItemService _service = new LimerItemService();
        public bool Changed { get; private set; } = false;
        public ObservableCollection<LimerViewModel> Limers { get; set; } = new ObservableCollection<LimerViewModel>();
        public LimerViewModel SelectedLimer { get; set; }
        public bool IsEditButtonEnabled => SelectedLimer != null;
        public bool IsDeleteButtonEnabled => SelectedLimer != null;
        public string FileName { get; private set; }
        public string WindowTitle => string.IsNullOrEmpty(FileName) ? "Список учеников" : $"Список учеников: {Path.GetFileName(FileName)}{(Changed ? "*" : "")}";
        public MainViewModel()
        {
            MapperInitialization.PreRegister();
        }
        public LimerViewModel GetNewLimer()
        {
            return new LimerViewModel
            {
                BirthDate = DateTime.Now,
            };
        }
        public void Selecting()
        {
            OnPropertyChanged(nameof(IsEditButtonEnabled));
            OnPropertyChanged(nameof(IsDeleteButtonEnabled));
        }
        public LimerViewModel GetSelectedLimer()
        {
            return SelectedLimer;
        }
        public void WindowLoaded()
        {
            LoadAllData();
        }
        public void LoadAllData()
        {
            var items = _service.GetAll();
            Limers.Clear();
            foreach (var item in items)
            {
                Limers.Add(ToViewModel(item));
            }
        }
        public void SaveEditedLimer(LimerViewModel limer)
        {
            var index = Limers.ToList().FindIndex(l =>  l.Id == limer.Id);
            if (index < 0)
            {
                throw new IndexOutOfRangeException("Карточка с таким Id не существует");
            }
            var id = _service.Save(ToBusiness(limer));
            if (id < 0)
            {
                Limers.RemoveAt(index);
            }
            else
            {
                Limers[index].LoadViewModel(limer);
            }
            Changed = true;
            OnPropertyChanged(nameof(WindowTitle));
        }
        public void SaveNewLimer(LimerViewModel limer)
        {
            var newLimer = new LimerViewModel();
            newLimer.LoadViewModel(limer);
            var id = _service.Save(ToBusiness(limer));
            if (id < 0)
            {
                return;
            }
            newLimer.Id = id;
            Limers.Add(newLimer);
            Changed = true;
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
            try
            {
                _service.SaveToFile(FileName);
            }catch (Exception)
            {
                FileName = null;
                throw;
            }
            Changed = false;
            OnPropertyChanged(nameof(WindowTitle));
        }
        public void DeleteSelectedLimer()
        {
            if (SelectedLimer == null)
            {
                return;
            }
            _service.Delete(SelectedLimer.Id);
            var index = Limers.ToList().FindIndex(l => l.Id == SelectedLimer.Id);
            System.Diagnostics.Debug.WriteLine(index);
            if (index < 0)
            {
                throw new IndexOutOfRangeException("Карточка с таким Id не существует");
            }
            
            Limers.RemoveAt(index);
            SelectedLimer = null;
            OnPropertyChanged(nameof(SelectedLimer));
            Changed = true;
            OnPropertyChanged(nameof(WindowTitle));
        }
        public void Initialized()
        {
            Mapping.Initialize();
        }
        private LimerViewModel ToViewModel(LimerItem item)
        {
            return Mapping.Mapper.Map<LimerViewModel>(item);
        }
        private LimerItem ToBusiness(LimerViewModel item)
        {
            return Mapping.Mapper.Map<LimerItem>(item);
        }
        public void OpenFormFile(string filename)
        {
            _service.OpenFromFile(filename);
            LoadAllData();
            FileName = filename;
            OnPropertyChanged(nameof(WindowTitle));
            Changed = false;
            OnPropertyChanged(nameof(WindowTitle));
        }
        
    }
}
