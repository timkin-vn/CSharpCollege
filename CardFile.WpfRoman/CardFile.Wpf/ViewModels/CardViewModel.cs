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
        private string _nameClass;
        private int _numberClass;

        private string _lastName;

        private string _middleName;

        private DateTime _birthDate = new DateTime(2000, 6, 15);

        private decimal _paymentAmount;

        private int _childrenCountClass;
        
        public int Id { get; set; }

        public string Fio => $"{LastName} {FirstName} {MiddleName}";
        public string Class => $"{NameClass }-{NumberClass}";
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
        public string NameClass
        {
            get => _nameClass;
            set
            {
                _nameClass = value;
                OnPropertyChanged(nameof(Class));
                OnPropertyChanged(nameof(NameClass));
            }
        }
        public int NumberClass
        {
            get => _numberClass;
            set
            {
                _numberClass = value;
                OnPropertyChanged(nameof(Class));
                OnPropertyChanged(nameof(NumberClass));
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

        public int ChildrenCountClass
        {
            get => _childrenCountClass;
            set
            {
                _childrenCountClass = value;
                OnPropertyChanged(nameof(ChildrenCountClass));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void CopyFrom(CardViewModel model)
        {
            Id = model.Id;
            FirstName = model.FirstName;
            MiddleName = model.MiddleName;
            LastName = model.LastName;
            NameClass = model.NameClass;
            NumberClass = model.NumberClass;
            BirthDate = model.BirthDate;
            PaymentAmount = model.PaymentAmount;
            ChildrenCountClass = model.ChildrenCountClass;
            
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
