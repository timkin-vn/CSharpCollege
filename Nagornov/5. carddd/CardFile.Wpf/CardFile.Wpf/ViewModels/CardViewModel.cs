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


        public string Name { get; set; }

        public string Type { get; set; }

        public string Manufacturer { get; set; }

        public string strana {  get; set; }

        public decimal Price { get; set; }

        public int StockQuantity { get; set; }

        public int lavka { get; set; }

        public void LoadViewModel(CardViewModel model)
        {
            Mapping.Mapper.Map(model, this);
            //Id = model.Id;
            //FirstName = model.FirstName;
            //MiddleName = model.MiddleName;
            //LastName = model.LastName;
            //BirthDate = model.BirthDate;
            //Department = model.Department;
            //Position = model.Position;
            //EmploymentDate = model.EmploymentDate;
            //DismissalDate = model.DismissalDate;
            //Salary = model.Salary;

           

            UpdateAll();
        }

       

        private void UpdateAll()
        {
            OnPropertyChanged(nameof(Id));
            OnPropertyChanged(nameof(Name));
            OnPropertyChanged(nameof(Type));
            OnPropertyChanged(nameof(Manufacturer));
            OnPropertyChanged(nameof(strana));
            OnPropertyChanged(nameof(Price));
            OnPropertyChanged(nameof(StockQuantity));
            OnPropertyChanged(nameof(lavka));
        }
    }
}
