using FifteenGame.Business.Services;
using FifteenGame.Common.BusinessModels;
using FifteenGame.Common.Definitions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace FifteenGame.Wpf.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        

        private GameService _gameService = new GameService();

        private GameModel _gameModel = new GameModel();
        private Units units = new Units(" ", 0,0,0,0, Units.UnitType.None);
        private UserModel _user;
        

        public ObservableCollection<CellViewModel> Cells { get; set; } = new ObservableCollection<CellViewModel>();

        public string UserName => _user?.Name ?? "<нет>";

        public string MoveCountText => (_gameModel?.MoveCount ?? 0).ToString();

        public MainWindowViewModel()
        {
            Initialize();
        }
            public List<Units> units_ = new List<Units>
                {

                    new Units(" ", 0, 0, 0, 0, Units.UnitType.None),
                    new Units("Д", 100, 20, 0, 1, Units.UnitType.Dragon), // Дракон
                    new Units("М", 80, 10, 0, 3, Units.UnitType.Medic), // Медик
                    new Units("Р", 120, 25, 0, 5, Units.UnitType.Knight), // Рыцарь
                    new Units("К", 150, 15, 0, 7, Units.UnitType.King), // Король
                    new Units("Б", 500, 40, 4, 2, Units.UnitType.Boss) // Босс
                };
         
        public void SetUser(UserModel user)
        {
            _user = user;
            OnPropertyChanged(nameof(UserName));

            _gameModel = _gameService.GetByUserId(_user.Id);
            FromModel(units);
        }

        public void MakeMove(MoveDirection direction,int X,int Y, Action gameFinishedAction)
        {
            _gameModel = _gameService.MakeMove(_gameModel.Id,X,Y, units, direction);
            
            FromModel(units);

            if (_gameService.IsGameOver(_gameModel.Id))
            {
                _gameService.RemoveGame(_gameModel.Id);
                gameFinishedAction();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void Initialize()
        {
            _gameModel = new GameModel { MoveCount = 0, };
            
            FromModel(units);
        }

        public void ReInitialize()
        {
            _gameModel = _gameService.GetByUserId(_user.Id);
            
            FromModel(units);
        }

        public void FromModel(Units units)
        {


            for (int row = 0; row < Constants.RowCount; row++)
            {
                for (int column = 0; column < Constants.ColumnCount; column++)
                {
                    if (units[row, column] != null)
                    {
                        var direction = MoveDirection.None;
                        if (row == units.FreeCellRow)
                        {
                            if (column == units.FreeCellColumn - 1)
                            {
                                direction = MoveDirection.XRight;
                            }
                            else if (column == units.FreeCellColumn + 1)
                            {
                                direction = MoveDirection.XLeft;
                            }
                        }
                        else if (column == units.FreeCellColumn)
                        {
                            if (row == units.FreeCellRow - 1)
                            {
                                direction = MoveDirection.YDown;
                            }
                            else if (row == units.FreeCellRow + 1)
                            {
                                direction = MoveDirection.YUp;
                            }
                        }
                        else if (Math.Abs(row - units.FreeCellRow) == 1 && Math.Abs(column - units.FreeCellColumn) == 1)
                        {
                            if (row < units.FreeCellRow && column < units.FreeCellColumn)
                            {
                                direction = MoveDirection.DiagonalDownRight;
                            }
                            else if (row < units.FreeCellRow && column > units.FreeCellColumn)
                            {
                                direction = MoveDirection.DiagonalDownLeft;
                            }
                            else if (row > units.FreeCellRow && column < units.FreeCellColumn)
                            {
                                direction = MoveDirection.DiagonalUpRight;
                            }
                            else if (row > units.FreeCellRow && column > units.FreeCellColumn)
                            {
                                direction = MoveDirection.DiagonalUpLeft;
                            }
                        }
                        Cells.Add(new CellViewModel
                        {
                            Row = row,
                            Column = column,
                            Num = units[row, column],
                            Direction = direction
                        });
                    }
                    else
                    {


                        Cells.Add(new CellViewModel
                        {
                            Row = row,
                            Column = column,
                            Num = new Units(" ", 0, 0, 0, 0, Units.UnitType.None),
                            Direction = MoveDirection.None,
                        });
                    }
                }
            }



        }
    }
}
