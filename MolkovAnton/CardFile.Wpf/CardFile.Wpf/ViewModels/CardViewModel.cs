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

        private decimal _paymentAmount;

        private int _childrenCount;

        private string _licenseNumber;

        private string _issuedLicense;

        private string _licenseName;

        public int Id { get; set; }

        public string Fio => $"{LastName} {FirstName} {MiddleName}";

        public string License => $"№{LicenseNumber} ; \"{LicenseName}\" ; {IssuedLicense}";

        public string LicenseNumber
        {
            get => _licenseNumber;
            set
            {
                _licenseNumber = value;
                OnPropertyChanged(nameof(License));
                OnPropertyChanged(nameof(LicenseNumber));
            }
        }
        public string LicenseName
        {
            get => _licenseName;
            set
            {
                _licenseName = value;
                OnPropertyChanged(nameof(License));
                OnPropertyChanged(nameof(LicenseName));
            }
        }

        public string IssuedLicense
        {
            get => _issuedLicense;
            set
            {
                _issuedLicense = value;
                OnPropertyChanged(nameof(License));
                OnPropertyChanged(nameof(IssuedLicense));
            }
        }


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

        public decimal PaymentAmount
        {
            get => _paymentAmount;
            set
            {
                _paymentAmount = value;
                OnPropertyChanged(nameof(PaymentAmount));
            }
        }

        public string PaymentAmountText => PaymentAmount.ToString("#,##0.00 р\\.");

        public int ChildrenCount
        {
            get => _childrenCount;
            set
            {
                _childrenCount = value;
                OnPropertyChanged(nameof(ChildrenCount));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void CopyFrom(CardViewModel model)
        {
            Id = model.Id;
            FirstName = model.FirstName;
            MiddleName = model.MiddleName;
            LastName = model.LastName;
            BirthDate = model.BirthDate;
            PaymentAmount = model.PaymentAmount;
            ChildrenCount = model.ChildrenCount;
            LicenseNumber = model.LicenseNumber;
            IssuedLicense = model.IssuedLicense;
            LicenseName = model.LicenseName;
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
