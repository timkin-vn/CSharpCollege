using FifteenGame.Common.BusinessModels;
using FifteenGame.Common.Definitions;
using FifteenGame.Common.Infrastructure;
using FifteenGame.Common.Services;
using Ninject;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FifteenGame.Wpf.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private IGameService _service = NinjectKernel.Instance.Get<IGameService>();

        private GameModel _model = new GameModel();

        private UserModel _user;

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<CellViewModel> Cells { get; set; } = new ObservableCollection<CellViewModel>();

        public string UserName => _user?.Name ?? "<нет>";

        public int MoveCount => _model?.MoveCount ?? 0;

        public MainWindowViewModel()
        {
            Initialize();
        }

        public void SetUser(UserModel user)
        {
            _user = user;
            OnPropertyChanged(nameof(UserName));

            _model = _service.GetByUserId(_user.Id);
            FromModel(_model);
        }

        public void Initialize()
        {
            _model = new GameModel { MoveCount = 0, };
            FromModel(_model);
        }

        public void ReInitialize()
        {
            _model = _service.GetByUserId(_user.Id);
            FromModel(_model);
        }

        public void MakeMove(MoveDirection direction, Action gameFinishAction)
        {
            _model = _service.MakeMove(_model.Id, direction);
            FromModel(_model);

            if (_service.IsGameOver(_model.Id))
            {
                _service.RemoveGame(_model.Id);
                gameFinishAction?.Invoke();
            }
        }

        private void FromModel(GameModel model)
        {
            Cells.Clear();
            for (int row = 0; row < Constants.RowCount; row++)
            {
                for (int column = 0; column < Constants.ColumnCount; column++)
                {
                    if (model[row, column] != Constants.FreeCellValue)
                    {
                        var direction = MoveDirection.None;
                        if (row == model.FreeCellRow)
                        {
                            if (column == model.FreeCellColumn - 1)
                            {
                                direction = MoveDirection.Right;
                            }
                            else if (column == model.FreeCellColumn + 1)
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
                            Row = row,
                            Column = column,
                            Num = model[row, column],
                            Direction = direction
                        });
                    }
                }
            }

            OnPropertyChanged(nameof(MoveCount));
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
