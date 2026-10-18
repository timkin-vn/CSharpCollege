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

        public string LoreName { get; set; }

        public string PrimaryAttribute { get; set; }

        public string AttackType { get; set; }

        public string Role { get; set; }

        public DateTime ReleaseDate { get; set; }

        public string ReleaseDateText => ReleaseDate.ToShortDateString();

        public DateTime? ReworkDate { get; set; }

        public string ReworkDateText => ReworkDate?.ToShortDateString() ?? "-";

        public decimal WinRate { get; set; }

        public string WinRateText => $"{WinRate:0.0}%";

        public bool IsNotReworked { get; set; }

        public bool IsReworkDateEnabled => !IsNotReworked;

        public void IsNotReworkedChecked()
        {
            ReworkDate = null;

            OnPropertyChanged(nameof(ReworkDate));
            OnPropertyChanged(nameof(IsReworkDateEnabled));
        }

        public void IsNotReworkedUnchecked()
        {
            ReworkDate = DateTime.Today;

            OnPropertyChanged(nameof(ReworkDate));
            OnPropertyChanged(nameof(IsReworkDateEnabled));
        }

        public void LoadViewModel(CardViewModel model)
        {
            Mapping.Mapper.Map(model, this);

            IsNotReworked = !model.ReworkDate.HasValue;

            UpdateAll();
        }

        private void UpdateAll()
        {
            OnPropertyChanged(nameof(Id));
            OnPropertyChanged(nameof(Name));
            OnPropertyChanged(nameof(LoreName));
            OnPropertyChanged(nameof(PrimaryAttribute));
            OnPropertyChanged(nameof(AttackType));
            OnPropertyChanged(nameof(Role));
            OnPropertyChanged(nameof(ReleaseDate));
            OnPropertyChanged(nameof(ReworkDate));
            OnPropertyChanged(nameof(WinRate));
            OnPropertyChanged(nameof(IsNotReworked));
            OnPropertyChanged(nameof(IsReworkDateEnabled));
        }
    }
}
