using Match3Game.Business.Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Match3Game.Wpf.ViewModels
{
	public class CellViewModel : INotifyPropertyChanged
	{
		private Cell cell;
		public Cell Cell
		{
			get => cell;
			set
			{
				cell = value;
				OnPropertyChanged();
				OnPropertyChanged(nameof(Type));
			}
		}

		public string Type => Cell.Type.ToString();

		public int Row => Cell.Row;
		public int Column => Cell.Column;

		public event PropertyChangedEventHandler PropertyChanged;

		protected void OnPropertyChanged([CallerMemberName] string name = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
		}
	}
}
