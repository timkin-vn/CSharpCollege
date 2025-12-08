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

        /// <summary>
        /// Название книги
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Автор
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        /// Год выпуска
        /// </summary>
        public int Year { get; set; }

        /// <summary>
        /// Жанр
        /// </summary>
        public string Genre { get; set; }

        /// <summary>
        /// Описание
        /// </summary>
        public string Description { get; set; }

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
            OnPropertyChanged(nameof(Title));
            OnPropertyChanged(nameof(Author));
            OnPropertyChanged(nameof(Year));
            OnPropertyChanged(nameof(Genre));
            OnPropertyChanged(nameof(Description));
        }
    }
}
