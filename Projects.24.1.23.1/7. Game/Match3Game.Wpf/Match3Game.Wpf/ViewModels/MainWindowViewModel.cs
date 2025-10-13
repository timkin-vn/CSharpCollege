using Match3Game.Business.Models;
using Match3Game.Business.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Match3Game.Wpf.ViewModels
{
	public class MainWindowViewModel
	{
		private GameService _service = new GameService();
		private GameModel _model = new GameModel();

		public ObservableCollection<CellViewModel> Cells { get; set; } = new ObservableCollection<CellViewModel>();

		private CellViewModel _firstSelected;

		public MainWindowViewModel()
		{
			Initialize();
		}

		public void Initialize()
		{
			_service.Initialize(_model);
			UpdateCells();
		}

		public void SelectCell(CellViewModel cell)
		{
			if (_firstSelected == null)
			{
				_firstSelected = cell;
				cell.IsSelected = true;
			}
			else
			{
				var success = _service.Swap(_model, _firstSelected.Row, _firstSelected.Column, cell.Row, cell.Column);
				_firstSelected.IsSelected = false;
				_firstSelected = null;

                if (success)
                {
                    var matches = _service.CheckMatches(_model);
                    if (matches.Any())
                    {
                        _service.RemoveMatches(_model, matches);
                        _service.ProcessMatches(_model);
                    }

                    UpdateCells();
                }
            }
		}

		private void UpdateCells()
		{
			Cells.Clear();
			for (int row = 0; row < GameModel.RowCount; row++)
			{
				for (int col = 0; col < GameModel.ColumnCount; col++)
				{
					Cells.Add(new CellViewModel
					{
						Row = row,
						Column = col,
						Value = _model[row, col]
					});
				}
			}
		}
	}
}
