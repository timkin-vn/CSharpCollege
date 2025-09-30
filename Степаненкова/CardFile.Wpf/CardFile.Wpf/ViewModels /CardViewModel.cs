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

        /// <summary>
        /// Имя
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Отчество
        /// </summary>
        public string MiddleName { get; set; }

        /// <summary>
        /// Фамилия
        /// </summary>
        public string LastName { get; set; }

        public string Fio => $"{LastName} {FirstName} {MiddleName}";

        /// <summary>
        /// Дата рождения
        /// </summary>
        public DateTime BirthDate { get; set; }

        public string BirthDateText => BirthDate.ToShortDateString();

        /// <summary>
        /// Дата регистрации в библиотеке
        /// </summary>
        public DateTime RegistrationDate { get; set; }

        public string RegDateText => RegistrationDate.ToShortDateString();

        /// <summary>
        /// Автор произведения
        /// </summary>

        public string Autor { get; set; }

        /// <summary>
        /// Жанр произведения
        /// </summary>
        public string Genre { get; set; }

        /// <summary>
        /// Произведение
        /// </summary>
        public string Book { get; set; }

        /// <summary>
        /// Дата взятия
        /// </summary>
        public DateTime GetDate { get; set; }

        public string GetDateText => GetDate.ToShortDateString();

        /// <summary>
        /// Дата возврата
        /// </summary>
        public DateTime? RefundDate { get; set; }

        public string RefundDateText => RefundDate?.ToShortDateString();

        public bool ReadTillNow { get; set; }

        public bool IsRefundDateEnabled => !ReadTillNow;

        /// <summary>
        /// Всего взято книг (не за раз, а за все время)
        /// </summary>
        public decimal Collection { get; set; }

        public void LoadFromViewModel(CardViewModel viewModel)
        {
            Mapping.Mapper.Map(viewModel, this);
            ReadTillNow = !viewModel.RefundDate.HasValue;
            UpdateAll();
        }

        public void WorksTillNowChecked()
        {
            RefundDate = null;

            OnPropertyChanged(nameof(RefundDate));
            OnPropertyChanged(nameof(IsRefundDateEnabled));
        }

        public void WorksTillNowUnchecked()
        {
            RefundDate = DateTime.Today;

            OnPropertyChanged(nameof(RefundDate));
            OnPropertyChanged(nameof(IsRefundDateEnabled));
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
            OnPropertyChanged(nameof(RegistrationDate));
            OnPropertyChanged(nameof(RegDateText));
            OnPropertyChanged(nameof(Autor));
            OnPropertyChanged(nameof(Genre));
            OnPropertyChanged(nameof(Book));
            OnPropertyChanged(nameof(GetDate));
            OnPropertyChanged(nameof(GetDateText));
            OnPropertyChanged(nameof(RefundDate));
            OnPropertyChanged(nameof(RefundDateText));
            OnPropertyChanged(nameof(IsRefundDateEnabled));
            OnPropertyChanged(nameof(ReadTillNow));
            OnPropertyChanged(nameof(Collection));
        }
    }
}
