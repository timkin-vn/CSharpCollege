using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardFile.Wpf.ViewModels
{
    public class CardViewModel : INotifyPropertyChanged
    {
        private string _firstName;

        private string _lastName;

        private string _middleName;

        private DateTime _birthDate = new DateTime(2000, 6, 15);

        private decimal _Credit;

        private DateTime _dateOfEndCredit = new DateTime(2000, 6, 15);

        public int Id { get; set; }

        public string Fio => $"{LastName} {FirstName} {MiddleName}";

        public string FirstName
        {
            get => _firstName;
            set
            {
                _firstName = value;
                OnPropertyChanged(nameof(Fio));
                OnPropertyChanged(nameof(FirstName));
            }
        }

        public string MiddleName
        {
            get => _middleName;
            set
            {
                _middleName = value;
                OnPropertyChanged(nameof(Fio));
                OnPropertyChanged(nameof(MiddleName));
            }
        }

        public string LastName
        {
            get => _lastName;
            set
            {
                _lastName = value;
                OnPropertyChanged(nameof(Fio));
                OnPropertyChanged(nameof(LastName));
            }
        }

        public DateTime BirthDate
        {
            get => _birthDate;
            set
            {
                _birthDate = value;
                OnPropertyChanged(nameof(BirthDate));
            }
        }


        public string BirthDateText => BirthDate.ToShortDateString();

        public decimal Credit
        {
            get => _Credit;
            set
            {
                _Credit = value;
                OnPropertyChanged(nameof(Credit));
            }
        }

        public string PaymentAmountText => Credit.ToString("#,##0.00 р\\.");
        public DateTime DateOfEndCredit
        {
            get => _dateOfEndCredit;
            set
            {
                _dateOfEndCredit = value;
                OnPropertyChanged(nameof(DateOfEndCredit));
            }
        }
        public string DateOfEndCreditText => DateOfEndCredit.ToShortDateString();


        public event PropertyChangedEventHandler PropertyChanged;

        public void CopyFrom(CardViewModel model)
        {
            Id = model.Id;
            FirstName = model.FirstName;
            MiddleName = model.MiddleName;
            LastName = model.LastName;
            BirthDate = model.BirthDate;
            Credit = model.Credit;
            DateOfEndCredit = model.DateOfEndCredit;
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
