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

        private int _sud;

        private string _articleNumber;

        private string _issuedArticle;

        private string _articleName;

        public int Id { get; set; }

        public string Fio => $"{LastName} {FirstName} {MiddleName}";

        public string ArticleNumber
        {
            get => _articleNumber;
            set
            {
                _articleNumber = value;
                OnPropertyChanged(nameof(Article));
                OnPropertyChanged(nameof(ArticleNumber));
            }
        }
        public string ArticleName
        {
            get => _articleName;
            set
            {
                _articleName = value;
                OnPropertyChanged(nameof(Article));
                OnPropertyChanged(nameof(ArticleName));
            }
        }

        public string IssuedArticle
        {
            get => _issuedArticle;
            set
            {
                _issuedArticle = value;
                OnPropertyChanged(nameof(Article));
                OnPropertyChanged(nameof(IssuedArticle));
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

        public int Sud
        {
            get => _sud;
            set
            {
                _sud = value;
                OnPropertyChanged(nameof(Sud));
            }
        }

        public string Article => $"{ArticleNumber} - {ArticleName} ({IssuedArticle})";

        public event PropertyChangedEventHandler PropertyChanged;

        public void CopyFrom(CardViewModel model)
        {
            Id = model.Id;
            FirstName = model.FirstName;
            MiddleName = model.MiddleName;
            LastName = model.LastName;
            BirthDate = model.BirthDate;
            PaymentAmount = model.PaymentAmount;
            Sud = model.Sud;
            ArticleNumber = model.ArticleNumber;
            IssuedArticle = model.IssuedArticle;
            ArticleName = model.ArticleName;
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
