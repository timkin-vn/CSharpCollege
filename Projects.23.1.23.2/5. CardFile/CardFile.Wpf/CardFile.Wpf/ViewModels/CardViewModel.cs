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

        /// <summary>
        /// Имя
        /// </summary>
        public string Name { get; set; }

        public DateTime DeliverDate { get; set; }

        public string DeliverDateText => DeliverDate.ToShortDateString();

        public DateTime ExpirationDate { get; set; }

        public string ExpirationDateText => ExpirationDate.ToShortDateString();

        public int Count { get; set; }

        public double Rating { get; set; }

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
            //Salary = viewModel.Salary;

            UpdateAll();
        }

        private void UpdateAll()
        {
            OnPropertyChanged(nameof(Id));
            OnPropertyChanged(nameof(Name));
            OnPropertyChanged(nameof(DeliverDate));
            OnPropertyChanged(nameof(ExpirationDate));
            OnPropertyChanged(nameof(Count));
            OnPropertyChanged(nameof(Rating));
        }
    }
}
