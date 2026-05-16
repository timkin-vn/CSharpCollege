using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Text.Json;
using System.Linq;
using System.ComponentModel;
using System.Windows.Data;
using CardFile.Business.Models;

namespace CardFile.Wpf.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        private const string DefaultFileName = "data.compjson";
        private readonly ICollectionView _companiesView;
        private string _searchText;
        private Company _selectedCompany;

        public string WindowTitle => "Картотека компаний";
        public ObservableCollection<Company> Companies { get; set; } = new ObservableCollection<Company>();

        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                OnPropertyChanged();
                _companiesView.Refresh();
            }
        }

        public Company SelectedCompany
        {
            get => _selectedCompany;
            set
            {
                _selectedCompany = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(IsEditButtonEnabled));
                OnPropertyChanged(nameof(IsDeleteButtonEnabled));
            }
        }

        public bool IsEditButtonEnabled => SelectedCompany != null;
        public bool IsDeleteButtonEnabled => SelectedCompany != null;
        public string FileName { get; private set; }

        public MainWindowViewModel()
        {
            _companiesView = CollectionViewSource.GetDefaultView(Companies);
            _companiesView.Filter = FilterCompanies;
        }

        private bool FilterCompanies(object obj)
        {
            if (string.IsNullOrWhiteSpace(SearchText)) return true;
            if (!(obj is Company company)) return false;

            return company.Name.IndexOf(SearchText, StringComparison.OrdinalIgnoreCase) >= 0 ||
                   (company.TaxId != null && company.TaxId.Contains(SearchText));
        }

        public void Initialized()
        {
            if (File.Exists(DefaultFileName))
            {
                OpenFromFile(DefaultFileName);
            }
        }

        public Company GetNewCompany() => new Company { Name = "Новая компания" };

        public void SaveNewCompany(CardEditViewModel editVm)
        {
            if (editVm?.Company == null) return;
            Companies.Add(editVm.Company);
            SaveToFile(DefaultFileName);
        }

        public void SaveEditedCompany(CardEditViewModel editVm)
        {
            if (editVm?.Company == null || SelectedCompany == null) return;

            var index = Companies.IndexOf(SelectedCompany);
            if (index != -1)
            {
                Companies[index] = editVm.Company;
                SaveToFile(DefaultFileName);
            }
        }

        public void DeleteSelectedCompany()
        {
            if (SelectedCompany != null)
            {
                Companies.Remove(SelectedCompany);
                SaveToFile(DefaultFileName);
            }
        }

        public void OpenFromFile(string path)
        {
            try
            {
                if (!File.Exists(path)) return;
                var json = File.ReadAllText(path);
                var data = JsonSerializer.Deserialize<ObservableCollection<Company>>(json);

                Companies.Clear();
                if (data != null)
                {
                    foreach (var item in data) Companies.Add(item);
                }
                FileName = path;
            }
            catch { }
        }

        public void SaveToFile(string path = null)
        {
            path = path ?? FileName ?? DefaultFileName;
            if (string.IsNullOrEmpty(path)) return;

            var options = new JsonSerializerOptions { WriteIndented = true };
            var json = JsonSerializer.Serialize(Companies, options);
            File.WriteAllText(path, json);
            FileName = path;
        }

        public void SelectionChanged() { }
        public void WindowLoaded() { }
    }
}