using CardFile.Common.Infrastructure;
using System;

namespace CardFile.Wpf.ViewModels
{
    public class LetterViewModel : ViewModelBase
    {
        public int Id { get; set; }
        public string Sender { get; set; }
        public string Receiver { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public DateTime Date { get; set; }
        public bool IsRead { get; set; }

        public string DateText => Date.ToShortDateString();

        public void LoadViewModel(LetterViewModel model)
        {
            Mapping.Mapper.Map(model, this);
            UpdateAll();
        }

        private void UpdateAll()
        {
            OnPropertyChanged(nameof(Id));
            OnPropertyChanged(nameof(Sender));
            OnPropertyChanged(nameof(Receiver));
            OnPropertyChanged(nameof(Subject));
            OnPropertyChanged(nameof(Body));
            OnPropertyChanged(nameof(Date));
            OnPropertyChanged(nameof(DateText));
            OnPropertyChanged(nameof(IsRead));
        }
    }
}