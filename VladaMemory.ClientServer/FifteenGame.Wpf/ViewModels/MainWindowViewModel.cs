using FifteenGame.Common.BusinessModels;
using FifteenGame.Common.Definitions;
using FifteenGame.Common.Infrastructure;
using FifteenGame.Common.Services;
using Ninject;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace FifteenGame.Wpf.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private IGameService _service;

        private GameModel _model = new GameModel();

        private UserModel _userModel;

        public ObservableCollection<CellViewModel> Cells { get; set; } = new ObservableCollection<CellViewModel>();

        public string UserName => _userModel?.Name ?? "<нет>";

        public string MoveCountText => (_model?.MoveCount ?? 0).ToString();
        int countbuttonclick = 0;
        int[] ParaOneebuutonRowCol = { 0, 0 }, ParaTwoebuutonRowCol = { 0, 0 };
        public MainWindowViewModel()
        {
            _service = NinjectKernel.Instance.Get<IGameService>();
        }

        public void MakeMove(int[] colrow, Action gameFinishedAction)
        {
            if (countbuttonclick == 0)
            {

                ParaOneebuutonRowCol = colrow;
                
                countbuttonclick++;
                _model = _service.Connect_components(_model.GameId, ParaOneebuutonRowCol, ParaTwoebuutonRowCol);
                FromModel(_model);

            }
            else if (countbuttonclick == 1)
            {


                ParaOneebuutonRowCol = colrow;
                _model = _service.Connect_components(_model.GameId, ParaOneebuutonRowCol, ParaTwoebuutonRowCol);
                FromModel(_model);


                countbuttonclick = 0;

            }


            FromModel(_model);
            if (_service.IsGameOver(_model.GameId))
            {
                _service.RemoveGame(_model.GameId);
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
            _model = _service.GetByUserId(_userModel.Id);
            FromModel(_model);
        }

        public void SetUser(UserModel user)
        {
            _userModel = user;
            Initialize();
            OnPropertyChanged(nameof(UserName));
        }

        private void FromModel(GameModel model)
        {
            Cells.Clear();

            for (int row = 0; row < Constants.RowCount; row++)
            {
                for (int column = 0; column < Constants.ColumnCount; column++)
                {

                    if ((row == ParaOneebuutonRowCol[0] && column == ParaOneebuutonRowCol[1])
                        || (row == ParaTwoebuutonRowCol[0] && column == ParaTwoebuutonRowCol[1]))
                    {
                        Cells.Add(item: new CellViewModel
                        {
                            Row = row,
                            ColumnRow = new int[] { row, column },
                            Column = column,
                            NameСomponents = model[row, column],

                            IsConenkt = true,

                        });
                    }
                    else if (string.IsNullOrEmpty(model[row, column]))
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
                            NameСomponents = model[row, column],

                            IsConenkt = false,

                        });
                    }
                }

                OnPropertyChanged(nameof(MoveCountText));
            }
        }
    }
}
