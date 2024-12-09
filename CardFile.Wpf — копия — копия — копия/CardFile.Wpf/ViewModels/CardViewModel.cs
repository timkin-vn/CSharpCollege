using System;
using System.ComponentModel;

namespace CardFile.Wpf.ViewModels
{
    internal class CardViewModel : INotifyPropertyChanged
    {
        private string _firstName;
        private string _lastName;
        private string _middleName;
        private DateTime _birthDate = new DateTime(2000, 1, 1);
        private decimal _debtAmount;
        private string _position;
        private int _subordinatesCount;
        private decimal _paymentAmount;

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

        public decimal DebtAmount
        {
            get => _debtAmount;
            set
            {
                _debtAmount = value;
                OnPropertyChanged(nameof(DebtAmount));
            }
        }

        public string Position
        {
            get => _position;
            set
            {
                _position = value;
                OnPropertyChanged(nameof(Position));
            }
        }

        public int SubordinatesCount
        {
            get => _subordinatesCount;
            set
            {
                _subordinatesCount = value;
                OnPropertyChanged(nameof(SubordinatesCount));
            }
        }

        public decimal PaymentAmount
        {
            get => _paymentAmount;
            set
            {
                _paymentAmount = value;
                OnPropertyChanged(nameof(PaymentAmount));
            }
        }

        public string PaymentAmountText => $"{PaymentAmount:#,##0.00} р.";

        public event PropertyChangedEventHandler PropertyChanged;

        public void CopyFrom(CardViewModel model)
        {
            Id = model.Id;
            FirstName = model.FirstName;
            LastName = model.LastName;
            MiddleName = model.MiddleName;
            BirthDate = model.BirthDate;
            DebtAmount = model.DebtAmount;
            Position = model.Position;
            SubordinatesCount = model.SubordinatesCount;
            PaymentAmount = model.PaymentAmount;
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private string _department;

        public string Department
        {
            get => _department;
            set
            {
                _department = value;
                OnPropertyChanged(nameof(Department));
            }
        }

    }
}
