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

        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }

        public string Name { get; set; }

        /// <summary>
        /// год производства
        /// </summary>
        public DateTime YearOfProduction { get; set; }

        /// <summary>
        /// Должность
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Дата поставки
        /// </summary>
        public DateTime DeliveryDate { get; set; }

        /// <summary>
        /// Стоимость
        /// </summary>
        public int Price { get; set; }


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

        //public void IsWorkingTillNowChecked()
        //{
        //    DismissalDate = null;

        //    OnPropertyChanged(nameof(DismissalDate));
        //    OnPropertyChanged(nameof(IsDismissalDateEnabled));
        //}

        //public void IsWorkingTillNowUnchecked()
        //{
        //    DismissalDate = DateTime.Today;

        //    OnPropertyChanged(nameof(DismissalDate));
        //    OnPropertyChanged(nameof(IsDismissalDateEnabled));
        //}

        private void UpdateAll()
        {
            OnPropertyChanged(nameof(Id));
            OnPropertyChanged(nameof(Name));
            OnPropertyChanged(nameof(YearOfProduction));
            OnPropertyChanged(nameof(Type));
            OnPropertyChanged(nameof(DeliveryDate));
            OnPropertyChanged(nameof(Price));
        }
    }
}
