using System.Windows.Input;
using LightsOutGame.Models;

namespace LightsOutGame.ViewModels
{
    public class CellViewModel : ViewModelBase
    {
        private readonly CellModel _cell;

        public int Row { get; }
        public int Column { get; }

        public ICommand ClickCommand { get; }

        public bool IsOn => _cell.IsOn;

        public CellViewModel(CellModel cell, int row, int column, ICommand clickCommand)
        {
            _cell = cell;
            Row = row;
            Column = column;
            ClickCommand = clickCommand;
        }

        public void Refresh()
        {
            OnPropertyChanged(nameof(IsOn));
        }
    }
}
