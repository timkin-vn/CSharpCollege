using FifteenGame.Business.Services;
using FifteenGame.Common.BusinessModels;
using FifteenGame.Common.Definitions;
using FifteenGame.Common.Services;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Media;

namespace FifteenGame.Wpf.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private IGameService _service;

        private GameModel _model = new GameModel();

        private UserModel _userModel;

        public ObservableCollection<CellViewModel> Cells { get; set; } = new ObservableCollection<CellViewModel>();

        public string UserName => _userModel?.Name ?? "<нет>";

        public string PlayerCountText => (_model?.PlayerCount ?? 0).ToString();

        public MainWindowViewModel()
        {
            _service = new GameService();
        }

        public void MakeMove(MoveDirection direction, Action gameFinishedAction)
        {
            _model = _service.MakeMove(_model.GameId, direction);
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
                    if (row != model.PlayerRow || column != model.PlayerColumn)
                    {
                        var direction = MoveDirection.None;
                        if (row == model.PlayerRow)
                        {
                            if (column == model.PlayerColumn - 1)
                            {
                                direction = MoveDirection.Right;
                            }
                            else if (column == model.PlayerColumn + 1)
                            {
                                direction = MoveDirection.Left;
                            }
                        }
                        else if (column == model.PlayerColumn)
                        {
                            if (row == model.PlayerRow - 1)
                            {
                                direction = MoveDirection.Down;
                            }
                            else if (row == model.PlayerRow + 1)
                            {
                                direction = MoveDirection.Up;
                            }
                        }
                        var color = new SolidColorBrush(Colors.White);
                        if (row == 1 && column == Constants.ColumnCount - 1)
                        {
                            color = new SolidColorBrush(Colors.Yellow);
                        }
                        else if (model[row, column] > 0)
                        {
                            color = new SolidColorBrush(Color.FromRgb(12, 171, 171));
                        }
                        else if (model[row, column] < 0)
                        {
                            color = new SolidColorBrush(Color.FromRgb(171, 57, 57));
                        }
                        else
                        {
                            color = new SolidColorBrush(Colors.Gray);
                        }
                        Cells.Add(new CellViewModel
                        {
                            Row = row,
                            Column = column,
                            Num = model[row, column],
                            Direction = direction,
                            FillColor = color
                        });
                    }
                    else
                    {
                        Cells.Add(new CellViewModel
                        {
                            Row = row,
                            Column = column,
                            Num = model[row, column],
                            FillColor = new SolidColorBrush(Color.FromRgb(10, 148, 36))
                        });
                    }
                }
            }
            OnPropertyChanged(nameof(PlayerCountText));
        }
    }
}
