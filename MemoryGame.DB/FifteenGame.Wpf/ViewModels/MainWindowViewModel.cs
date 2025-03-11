using FifteenGame.Business.Services;
using FifteenGame.Common.BusinessModels;
using FifteenGame.Common.Definitions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace FifteenGame.Wpf.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private GameService _gameService = new GameService();

        private GameModel _gameModel = new GameModel();

        private UserModel _user;

        public ObservableCollection<CellViewModel> Cells { get; set; } = new ObservableCollection<CellViewModel>();

        public string UserName => _user?.Name ?? "<нет>";

        public string MoveCountText => (_gameModel?.MoveCount ?? 0).ToString();
        int countbuttonclick = 0;
        int[] FisrsbuutonRowCol = { 0, 0 }, SecondbuutonRowCol = {0,0};
        string FistbuutonText, SecondbuutonText;
        public MainWindowViewModel()
        {
            Initialize();
        }

        public void SetUser(UserModel user)
        {
            _user = user;
            OnPropertyChanged(nameof(UserName));

            _gameModel = _gameService.GetByUserId(_user.Id);
            FromModel(_gameModel);
        }
        public void MakeMove(int[] colrow, Action gameFinishedAction)
        {
            
            if (countbuttonclick == 0)
            {
               
                FisrsbuutonRowCol = colrow;
                FistbuutonText = _gameModel[colrow[0], colrow[1]];
                countbuttonclick++;
                _gameModel = _gameService.CheckMatch(_gameModel.Id,FisrsbuutonRowCol,SecondbuutonRowCol);
                FromModel(_gameModel);

            }
            else if (countbuttonclick == 1)
            {
                
                SecondbuutonText = _gameModel[colrow[0], colrow[1]];
                SecondbuutonRowCol = colrow;
                _gameModel = _gameService.CheckMatch(_gameModel.Id, FisrsbuutonRowCol, SecondbuutonRowCol);
                FromModel(_gameModel);


                countbuttonclick = 0;

            }


            FromModel(_gameModel);
            if (_gameService.IsGameOver(_gameModel))
            {
                _gameService.RemoveGame(_gameModel.Id);
                gameFinishedAction?.Invoke();
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
            
            FromModel(_gameModel);
        }

        public void ReInitialize()
        {
            _gameModel = _gameService.GetByUserId(_user.Id);
            FromModel(_gameModel);
        }

        private void FromModel(GameModel model)
        {
            Cells.Clear();
            
            for (int row = 0; row < Constants.RowCount; row++)
            {
                for (int column = 0; column < Constants.ColumnCount; column++)
                {

                    if ((row == FisrsbuutonRowCol[0] && column == FisrsbuutonRowCol[1])
                        || (row == SecondbuutonRowCol[0] && column == SecondbuutonRowCol[1]))
                    {
                        Cells.Add(item: new CellViewModel
                        {
                            Row = row,
                            ColumnRow = new int[] { row, column },
                            Column = column,
                            ColorText = model[row, column],

                            IsFaceUp = true,

                        });
                    }
                    else if (FistbuutonText == String.Empty || SecondbuutonText == String.Empty || model[row, column] == "")
                    {
                        continue;
                    }

                    else
                    {
                        Cells.Add(item: new CellViewModel
                        {
                            Row = row,
                            ColumnRow = new int[] { row, column },
                            Column = column,
                            ColorText = model[row, column],

                            IsFaceUp = false,

                        });
                    }
                }

                OnPropertyChanged(nameof(MoveCountText));
            }
        }
    }
}
