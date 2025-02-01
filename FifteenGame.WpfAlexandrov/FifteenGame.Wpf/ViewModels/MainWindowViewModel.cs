using FifteenGame.Business.Models;
using FifteenGame.Business.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace FifteenGame.Wpf.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private GameModel selectedUnit;
        
        public GameModel SelectedUnit
        {
            get => selectedUnit;
            set
            {
                selectedUnit = value;
                OnPropertyChanged(nameof(SelectedUnit));
            }
        }

        




        private GameService _service = new GameService();
       
        private GameModel _model = new GameModel(" ", 0, 0, 0,0,GameModel.UnitType.None);

        public ObservableCollection<CellViewModel> Cells { get; set; } = new ObservableCollection<CellViewModel>();

        public MainWindowViewModel()
        {
            Initialize();
        }
        
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        
        private string _someProperty;
        public string SomeProperty
        {
            get => _someProperty;
            set
            {
                if (_someProperty != value)
                {
                    _someProperty = value;
                    OnPropertyChanged();
                }
            }
        }

        

        

        public void Initialize()
        {
            _service.Shuffle(_model);
            FromModel(_model);
        }

        private void FromModel(GameModel model)
        {
            Cells.Clear();
            for (int row = 0; row < GameModel.RowCount; row++)
            {
                for (int column = 0; column < GameModel.ColumnCount; column++)
                {
                    if (model[row, column] != null)
                    {
                        var direction = MoveDirection.None;
                        if (row == model.FreeCellRow)
                        {
                            if (column == model.FreeCellColumn - 1)
                            {
                                direction = MoveDirection.XRight;
                            }
                            else if (column == model.FreeCellColumn + 1)
                            {
                                direction = MoveDirection.XLeft;
                            }
                        }
                        else if (column == model.FreeCellColumn)
                        {
                            if (row == model.FreeCellRow - 1)
                            {
                                direction = MoveDirection.YDown;
                            }
                            else if (row == model.FreeCellRow + 1)
                            {
                                direction = MoveDirection.YUp;
                            }
                        }
                        else if (Math.Abs(row - model.FreeCellRow) == 1 && Math.Abs(column - model.FreeCellColumn) == 1)
                        {
                            if (row < model.FreeCellRow && column < model.FreeCellColumn)
                            {
                                direction = MoveDirection.DiagonalDownRight;
                            }
                            else if (row < model.FreeCellRow && column > model.FreeCellColumn)
                            {
                                direction = MoveDirection.DiagonalDownLeft;
                            }
                            else if (row > model.FreeCellRow && column < model.FreeCellColumn)
                            {
                                direction = MoveDirection.DiagonalUpRight;
                            }
                            else if (row > model.FreeCellRow && column > model.FreeCellColumn)
                            {
                                direction = MoveDirection.DiagonalUpLeft;
                            }
                        }
                        Cells.Add(new CellViewModel
                        {
                            Row = row,
                            Column = column,
                            Num = model[row, column],
                            Direction = direction
                        });
                    }else if (model[row, column] == null)
                    {
                        
                        Cells.Add(new CellViewModel
                        {
                            Row = row,
                            Column = column,
                            Num = new GameModel(" ", 0, 0, row, column, GameModel.UnitType.None), // Или любое другое значение по умолчанию
                            Direction = MoveDirection.None
                        });
                    }

                }
            }
        }
        public void SelectUnit(GameModel unit)
        {
            
            if (SelectedUnit != null)
            {
                SelectedUnit.IsSelected = false;
            }

            
            SelectedUnit = unit;
            if (SelectedUnit != null)
            {
                SelectedUnit.IsSelected = true;
            }
        }
    }
}
