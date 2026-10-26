using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace FifteenGame.Wpf.ViewModels
{
    public class CellViewModel : INotifyPropertyChanged
    {
        private string _text;

        public string Text
        {
            get => _text;
            set { _text = value; OnPropertyChanged(); }
        }

        public int Row { get; set; }
        public int Col { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}