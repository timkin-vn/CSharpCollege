using CardFile.Common.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardFile.Wpf.ViewModels
{
    public class CardViewModel : ViewModelBase
    {
        public int Id { get; set; }
        public string BookName { get; set; }
        public string AuthorFirstName { get; set; }
        public string AuthorLastName { get; set; }
        public string Fio => $" {AuthorFirstName} {AuthorLastName}";
        public string Genre { get; set; }
        public DateTime DateOfPublishing { get; set; }
        public string DateOfPublishingText => DateOfPublishing.ToShortDateString();
        public string Edition { get; set; }
        public int Price { get; set; }
        public int AmountOfPages { get; set; }
        public DateTime? DateOfDelisting { get; set; }
        public string DateOfDelistingText => DateOfDelisting?.ToShortDateString();
        public decimal Rating { get; set; }

        public bool InCatalogTillNow { get; set; }

        public bool IsDateOfDelistingEnabled => !InCatalogTillNow;

        public void LoadFromViewModel(CardViewModel viewModel)
        {
            Mapping.Mapper.Map(viewModel, this);
            //Id = viewModel.Id;
            //FirstName = viewModel.FirstName;
            //MiddleName = viewModel.MiddleName;
            //LastName = viewModel.LastName;
            //BirthDate = viewModel.BirthDate;
            //Department = viewModel.Department;
            //Position = viewModel.Position;
            //EmploymentDate = viewModel.EmploymentDate;
            //DismissalDate = viewModel.DismissalDate;
            InCatalogTillNow = !viewModel.DateOfDelisting.HasValue;
            //Salary = viewModel.Salary;

            UpdateAll();
        }

        public void InCatalogTillNowChecked()
        {
            DateOfDelisting = null;

            OnPropertyChanged(nameof(DateOfDelisting));
            OnPropertyChanged(nameof(IsDateOfDelistingEnabled));
        }

        public void InCatalogTillNowUnchecked()
        {
            DateOfDelisting = DateTime.Today;

            OnPropertyChanged(nameof(DateOfDelisting));
            OnPropertyChanged(nameof(IsDateOfDelistingEnabled));
        }

        private void UpdateAll()
        {
            OnPropertyChanged(nameof(Id));
            OnPropertyChanged(nameof(BookName));
            OnPropertyChanged(nameof(AuthorFirstName));
            OnPropertyChanged(nameof(AuthorLastName));
            OnPropertyChanged(nameof(Fio));
            OnPropertyChanged(nameof(Genre));
            OnPropertyChanged(nameof(DateOfPublishing));
            OnPropertyChanged(nameof(DateOfPublishingText));
            OnPropertyChanged(nameof(Edition));
            OnPropertyChanged(nameof(Price));
            OnPropertyChanged(nameof(AmountOfPages));
            OnPropertyChanged(nameof(DateOfDelisting));
            OnPropertyChanged(nameof(DateOfDelistingText));
            OnPropertyChanged(nameof(Rating));
            OnPropertyChanged(nameof(InCatalogTillNow));
            OnPropertyChanged(nameof(IsDateOfDelistingEnabled));
        }
    }
}
