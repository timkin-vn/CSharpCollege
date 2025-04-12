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
        private Units[,] units = new Units[Constants.RowCount, Constants.ColumnCount];
        private UserModel _user;
        private Units selectedUnit;
        private bool isUnitSelected = false;

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

        public void MakeMove(MoveDirection direction, int X, int Y, Action gameFinishedAction)
        {
            if (_gameModel == null) return;

            var currentUnit = units[X, Y];
            
            if (!isUnitSelected)
            {
                // Если юнит не выбран, выбираем его
                if (currentUnit != null && currentUnit.Type != Units.UnitType.None)
                {
                    selectedUnit = currentUnit;
                    isUnitSelected = true;
                    return;
                }
            }
            else
            {
                // Если юнит уже выбран, пытаемся его переместить
                if (IsAdjacent(selectedUnit.X, selectedUnit.Y, X, Y))
                {
                    // Меняем местами юниты
                    var temp = units[X, Y];
                    units[X, Y] = selectedUnit;
                    units[selectedUnit.X, selectedUnit.Y] = temp;
                    
                    // Обновляем позиции юнитов
                    selectedUnit.X = X;
                    selectedUnit.Y = Y;
                    if (temp != null)
                    {
                        temp.X = selectedUnit.X;
                        temp.Y = selectedUnit.Y;
                    }
                    
                    isUnitSelected = false;
                    selectedUnit = null;
                    
                    // Обновляем модель и интерфейс
                    FromModel(units);
                    OnPropertyChanged(nameof(MoveCountText));
                    
                    if (_gameService.IsGameOver(_gameModel.Id))
                    {
                        _gameService.RemoveGame(_gameModel.Id);
                        gameFinishedAction();
                    }
                }
                else
                {
                    // Если клетка не соседняя, отменяем выбор
                    isUnitSelected = false;
                    selectedUnit = null;
                }
            }
        }

        private bool IsAdjacent(int unitRow, int unitColumn, int targetRow, int targetColumn)
        {
            return (Math.Abs(unitRow - targetRow) == 1 && unitColumn == targetColumn) ||
                   (Math.Abs(unitColumn - targetColumn) == 1 && unitRow == targetRow);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void Initialize()
        {
            _gameModel = new GameModel { MoveCount = 0 };
            
            // Инициализируем юниты
            for (int row = 0; row < Constants.RowCount; row++)
            {
                for (int column = 0; column < Constants.ColumnCount; column++)
                {
                    if (row == 0)
                    {
                        // Размещаем юниты в первой строке
                        if (column == 0) units[row, column] = units_[0]; // Пустая клетка
                        else if (column == 1) units[row, column] = units_[1]; // Дракон
                        else if (column == 3) units[row, column] = units_[2]; // Медик
                        else if (column == 5) units[row, column] = units_[3]; // Рыцарь
                        else if (column == 7) units[row, column] = units_[4]; // Король
                        else units[row, column] = units_[0]; // Пустая клетка
                    }
                    else if (row == 4 && column == 2)
                    {
                        units[row, column] = units_[5]; // Босс
                    }
                    else
                    {
                        units[row, column] = units_[0]; // Пустая клетка
                    }
                }
            }
            
            FromModel(units);
        }

        private void FromModel(Units[,] model)
        {
            Cells.Clear();
            
            for (int row = 0; row < Constants.RowCount; row++)
            {
                for (int column = 0; column < Constants.ColumnCount; column++)
                {
                    Cells.Add(new CellViewModel
                    {
                        Num = model[row, column],
                        Row = row,
                        Column = column
                    });
                }
            }
        }

        public void ReInitialize()
        {
            Initialize();
            OnPropertyChanged(nameof(MoveCountText));
        }
    }
}
