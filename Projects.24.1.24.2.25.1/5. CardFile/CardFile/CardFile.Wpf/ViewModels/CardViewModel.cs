using CardFile.Common.Infrastructure;
using System;

namespace CardFile.Wpf.ViewModels
{
    public class CardViewModel : ViewModelBase
    {
        /// <summary>
        /// Id заметки
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Заголовок
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Текст заметки
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Категория
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// Дата создания
        /// </summary>
        public DateTime CreatedAt { get; set; }

        public string CreatedAtText => CreatedAt.ToShortDateString();

        /// <summary>
        /// Выполнено
        /// </summary>
        public bool IsDone { get; set; }

        /// <summary>
        /// Закреплено
        /// </summary>
        public bool IsPinned { get; set; }

        public string DoneText => IsDone ? "Да" : "Нет";

        public string PinnedText => IsPinned ? "Да" : "Нет";

        public string ShortText
        {
            get
            {
                if (string.IsNullOrWhiteSpace(Text))
                {
                    return string.Empty;
                }

                return Text.Length > 50 ? Text.Substring(0, 50) + "..." : Text;
            }
        }

        public void LoadViewModel(CardViewModel model)
        {
            Mapping.Mapper.Map(model, this);
            UpdateAll();
        }

        public void ToggleDone()
        {
            IsDone = !IsDone;
            OnPropertyChanged(nameof(IsDone));
            OnPropertyChanged(nameof(DoneText));
        }

        public void TogglePinned()
        {
            IsPinned = !IsPinned;
            OnPropertyChanged(nameof(IsPinned));
            OnPropertyChanged(nameof(PinnedText));
        }

        private void UpdateAll()
        {
            OnPropertyChanged(nameof(Id));
            OnPropertyChanged(nameof(Title));
            OnPropertyChanged(nameof(Text));
            OnPropertyChanged(nameof(Category));
            OnPropertyChanged(nameof(CreatedAt));
            OnPropertyChanged(nameof(CreatedAtText));
            OnPropertyChanged(nameof(IsDone));
            OnPropertyChanged(nameof(IsPinned));
            OnPropertyChanged(nameof(DoneText));
            OnPropertyChanged(nameof(PinnedText));
            OnPropertyChanged(nameof(ShortText));
        }
    }
}