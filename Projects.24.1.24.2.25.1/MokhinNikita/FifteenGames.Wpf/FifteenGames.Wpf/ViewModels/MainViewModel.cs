using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using FifteenGames.Business.Services;
using FifteenGames.Common.BusinessModels;
using static FifteenGames.Common.Definitions.Constants;

namespace FifteenGames.Wpf.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private GameService _service = new GameService();

        private GameModel _model = new GameModel();

        public ObservableCollection<CellViewModel> Cells { get; } = new ObservableCollection<CellViewModel>();

        public MainViewModel()
        {
            Initialize();
        }
        public void Initialize()
        {
            _service.Shuffle(_model);
            LoadViewModel(_model);
        }

        public void MakeMove(MoveDirection direction, Action dialogAction)
        {
            _service.MakeMove(_model, direction);
            LoadViewModel(_model);
            if (_service.IsGameOver(_model))
            {
                dialogAction?.Invoke();
            }
        }

        private void LoadViewModel(GameModel model)
        {
            Cells.Clear();
            for (int row = 0; row < RowCount; row++)
            {
                for(int column = 0; column < ColumnCount; column++)
                {
                    if (model[row, column] == FreeCellValue)
                    {
                        continue;
                    }
                    var direction = MoveDirection.None;
                    if(row == model.FreeCellRow)
                    {
                        if(column == model.FreeCellColumn - 1)
                        {
                            direction = MoveDirection.Right;
                        }else if(column == model.FreeCellColumn + 1)
                        {
                            direction = MoveDirection.Left;
                        }
                    }
                    else if (column == model.FreeCellColumn)
                    {
                        if (row == model.FreeCellRow - 1)
                        {
                            direction = MoveDirection.Down;
                        }
                        else if (row == model.FreeCellRow + 1)
                        {
                            direction = MoveDirection.Up;
                        }
                    }
                    Cells.Add(new CellViewModel
                    {
                        Value = model[row, column],
                        Row = row,
                        Column = column,
                        Direction = direction
                    });
                }
            }
        }
    }
}
