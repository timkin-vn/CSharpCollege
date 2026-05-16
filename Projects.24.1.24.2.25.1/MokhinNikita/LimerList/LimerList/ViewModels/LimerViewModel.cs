using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LimerList.Common.Infrastructure;

namespace LimerList.ViewModels
{
    public sealed class LimerViewModel : ViewModelBase
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string FIO
        {
            get {
                string[] strings = {LastName, FirstName, MiddleName};
                var result = from s in strings where !string.IsNullOrWhiteSpace(s) select s;
                return string.Join(" ", result);
            }
        }
        public DateTime BirthDate { get; set; }
        public string BirthDateString  => BirthDate.ToShortDateString();

        public string Group { get; set; }
        public void LoadViewModel(LimerViewModel model)
        {
            Mapping.Mapper.Map(model, this);
            UpdateAll();
        }

        private void UpdateAll()
        {
            string[] properties =
            {
                
                nameof(FirstName),
                nameof(LastName),
                nameof(FIO),
                nameof(MiddleName),
                nameof(Id),
                nameof(BirthDate),
                nameof(BirthDateString),
                nameof(Group),
            };
            foreach (var property in properties)
            {
                OnPropertyChanged(property);
            }
        }
    }
}
