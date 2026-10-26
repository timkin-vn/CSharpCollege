using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace FifteenGame.Wpf.ViewModels
{
    public class CellViewModel : INotifyPropertyChanged
    {
        private string _text;
        private int _row;
        private int _col;

        public string Text
        {
            get => _text;
            set { _text = value; OnPropertyChanged(); }
        }

        public int Row
        {
            get => _row;
            set { _row = value; OnPropertyChanged(); }
        }

        public int Col
        {
            get => _col;
            set { _col = value; OnPropertyChanged(); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}