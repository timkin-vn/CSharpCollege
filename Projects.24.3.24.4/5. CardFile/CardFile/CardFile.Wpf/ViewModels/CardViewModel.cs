using CardFile.Common.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardFile.DataStore.Dtos;

namespace CardFile.Wpf.ViewModels
{
    public class CardViewModel : ViewModelBase
    {
        public CardViewModel() { }

        public int Id { get; set; }

        public string LastName { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        /// \<summary>
        /// ФИО
        /// \</summary>
        public string Fio => $"{LastName} {FirstName} {MiddleName}".Trim();

        /// \<summary>
        /// Дата рождения
        /// \</summary>
        public DateTime BirthDate { get; set; } = new DateTime(2004, 1, 1);

        public string BirthDateText => BirthDate.ToShortDateString();

        /// \<summary>
        /// Факультет/специальность
        /// \</summary>
        public string Faculty { get; set; }

        /// \<summary>
        /// Номер группы
        /// \</summary>
        public string GroupNumber { get; set; }

        /// \<summary>
        /// Номер зачетной книжки
        /// \</summary>
        public int RecordBookNumber { get; set; }

        /// \<summary>
        /// Средний балл
        /// \</summary>
        public double AverageScore { get; set; }

        /// \<summary>
        /// Форма обучения
        /// \</summary>
        public bool IsBudget { get; set; } = true;

        private DateTime? _expulsionDate;
        /// \<summary>
        /// Дата отчисления/окончания
        /// \</summary>
        public DateTime? ExpulsionDate
        {
            get => _expulsionDate;
            set
            {
                _expulsionDate = value;
                OnPropertyChanged("ExpulsionDate");
                OnPropertyChanged("IsStudying");
            }
        }

        /// \<summary>
        /// Учится до сих пор
        /// \</summary>
        public bool IsStudying
        {
            get => ExpulsionDate == null;
            set
            {
                if (value)
                {
                    ExpulsionDate = null;
                }
                else if (ExpulsionDate == null)
                {
                    ExpulsionDate = DateTime.Today;
                }

                OnPropertyChanged("IsStudying");
                OnPropertyChanged("ExpulsionDate");
            }
        }

        private string _photoPath;
        /// \<summary>
        /// Путь к файлу фотографии студента
        /// \</summary>
        public string PhotoPath
        {
            get => _photoPath;
            set
            {
                _photoPath = value;
                OnPropertyChanged("PhotoPath");
            }
        }

        /// \<summary>
        /// Составное поле для сводки в таблице
        /// \</summary>
        public string StudentSummary => $"{Fio} | Группа: {GroupNumber} | Средний балл: {AverageScore:F2}";

        public void LoadViewModel(CardViewModel card)
        {
            PhotoPath = card.PhotoPath;

            Mapping.Mapper.Map(card, this);
            UpdateAll();
        }

        private void UpdateAll()
        {
            OnPropertyChanged("Id");
            OnPropertyChanged("LastName");
            OnPropertyChanged("FirstName");
            OnPropertyChanged("MiddleName");
            OnPropertyChanged("Fio");
            OnPropertyChanged("BirthDate");
            OnPropertyChanged("BirthDateText");
            OnPropertyChanged("Faculty");
            OnPropertyChanged("GroupNumber");
            OnPropertyChanged("RecordBookNumber");
            OnPropertyChanged("AverageScore");
            OnPropertyChanged("IsBudget");
            OnPropertyChanged("ExpulsionDate");
            OnPropertyChanged("IsStudying");
            OnPropertyChanged("PhotoPath");
            OnPropertyChanged("StudentSummary");
        }

        public CardDto ToDto()
        {
            return Mapping.Mapper.Map< CardDto > (this);
        }
    }
}
