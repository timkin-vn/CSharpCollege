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

            /// <summary>Имя</summary>
            public string FirstName { get; set; }

            /// <summary>Отчество</summary>
            public string MiddleName { get; set; }

            /// <summary>Фамилия</summary>
            public string LastName { get; set; }

            public string Fio => $"{LastName} {FirstName} {MiddleName}";

            /// <summary>Дата рождения</summary>
            public DateTime BirthDate { get; set; }

            public string BirthDateText => BirthDate.ToShortDateString();

            /// <summary>Пол</summary>
            public string Gender { get; set; }

            /// <summary>Адрес</summary>
            public string Address { get; set; }

            /// <summary>Диагноз</summary>
            public string Diagnosis { get; set; }

            public string DiagnosisText => Diagnosis;

            /// <summary>Дата последнего приёма</summary>
            public DateTime? LastVisitDate { get; set; }

            public string LastVisitDateText => LastVisitDate?.ToShortDateString();

            /// <summary>Телефон</summary>
            public string PhoneNumber { get; set; }
            
            public string InsuranceNumber { get; set; }
            public List<string> GenderOptions { get; } = new List<string> { "Мужской", "Женский" };
        public bool VisitedRecently { get; set; }

            public bool IsLastVisitDateEnabled => !VisitedRecently;

            public void LoadFromViewModel(CardViewModel viewModel)
            {

            Gender = viewModel.Gender;
            Address = viewModel.Address;
            Diagnosis = viewModel.Diagnosis;
            PhoneNumber = viewModel.PhoneNumber;
            LastVisitDate = viewModel.LastVisitDate;
            VisitedRecently = !viewModel.LastVisitDate.HasValue;
            Mapping.Mapper.Map(viewModel, this);
                VisitedRecently = !viewModel.LastVisitDate.HasValue;
            InsuranceNumber = viewModel.InsuranceNumber;
            UpdateAll();
            }

            public void VisitedRecentlyChecked()
            {
                LastVisitDate = null;
                OnPropertyChanged(nameof(LastVisitDate));
                OnPropertyChanged(nameof(IsLastVisitDateEnabled));
            }

            public void VisitedRecentlyUnchecked()
            {
                LastVisitDate = DateTime.Today;
                OnPropertyChanged(nameof(LastVisitDate));
                OnPropertyChanged(nameof(IsLastVisitDateEnabled));
            }

            private void UpdateAll()
            {
                OnPropertyChanged(nameof(Id));
                OnPropertyChanged(nameof(FirstName));
                OnPropertyChanged(nameof(MiddleName));
                OnPropertyChanged(nameof(LastName));
                OnPropertyChanged(nameof(Fio));
                OnPropertyChanged(nameof(BirthDate));
                OnPropertyChanged(nameof(BirthDateText));
                OnPropertyChanged(nameof(Gender));
                OnPropertyChanged(nameof(Address));
                OnPropertyChanged(nameof(Diagnosis));
                OnPropertyChanged(nameof(DiagnosisText));
                OnPropertyChanged(nameof(LastVisitDate));
                OnPropertyChanged(nameof(LastVisitDateText));
                OnPropertyChanged(nameof(PhoneNumber));
                OnPropertyChanged(nameof(VisitedRecently));
                OnPropertyChanged(nameof(IsLastVisitDateEnabled));
                  OnPropertyChanged(nameof(InsuranceNumber));
        }
        }
    }
