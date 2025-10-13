using Match3Game.Business.Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Match3Game.Wpf.ViewModels
{
	public class CellViewModel : INotifyPropertyChanged
	{
		private int _value;
		public int Value { get => _value; set { _value = value; OnPropertyChanged(); } }
		public int Row { get; set; }
		public int Column { get; set; }

		private bool _isSelected;
		public bool IsSelected { get => _isSelected; set { _isSelected = value; OnPropertyChanged(); } }

		public event PropertyChangedEventHandler PropertyChanged;

		protected void OnPropertyChanged([CallerMemberName] string name = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
		}
	}
}
