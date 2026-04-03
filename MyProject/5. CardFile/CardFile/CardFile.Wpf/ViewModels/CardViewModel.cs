using CardFile.Common.Infrastructure;
using System;

namespace CardFile.Wpf.ViewModels
{
    public class CardViewModel : ViewModelBase
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string Fio => $"{LastName} {FirstName} {MiddleName}";

        public DateTime BirthDate { get; set; }

        public string BirthDateText => BirthDate.ToShortDateString();

        public DateTime DeathDate { get; set; }

        public string DeathDateText => DeathDate.ToShortDateString();

        public string CauseOfDeath { get; set; }

        public string PlaceOfDeath { get; set; }

        public DateTime AdmissionDate { get; set; }

        public string AdmissionDateText => AdmissionDate.ToShortDateString();

        public string SectionNumber { get; set; }

        public bool IsReleased { get; set; }

        public string StatusText => IsReleased ? "Выдан" : "В морге";

        public void LoadViewModel(CardViewModel model)
        {
            if (model == null)
                return;

            Id = model.Id;
            FirstName = model.FirstName;
            MiddleName = model.MiddleName;
            LastName = model.LastName;
            BirthDate = model.BirthDate;
            DeathDate = model.DeathDate;
            CauseOfDeath = model.CauseOfDeath;
            PlaceOfDeath = model.PlaceOfDeath;
            AdmissionDate = model.AdmissionDate;
            SectionNumber = model.SectionNumber;
            IsReleased = model.IsReleased;

            UpdateAll();
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
            OnPropertyChanged(nameof(DeathDate));
            OnPropertyChanged(nameof(DeathDateText));
            OnPropertyChanged(nameof(CauseOfDeath));
            OnPropertyChanged(nameof(PlaceOfDeath));
            OnPropertyChanged(nameof(AdmissionDate));
            OnPropertyChanged(nameof(AdmissionDateText));
            OnPropertyChanged(nameof(SectionNumber));
            OnPropertyChanged(nameof(IsReleased));
            OnPropertyChanged(nameof(StatusText));
        }
    }
}