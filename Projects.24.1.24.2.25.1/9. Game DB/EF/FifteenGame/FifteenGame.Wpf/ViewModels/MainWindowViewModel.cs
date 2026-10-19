
using FifteenGame.Common.BusinessModels;
using FifteenGame.Common.Contracts.Services;
using FifteenGame.Common.Definitions;
using FifteenGame.Common.Infrastructure;
using Ninject;
using System;
using System.Collections.ObjectModel;

namespace FifteenGame.Wpf.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly IGameService _service =
            NinjectKernel.Instance.Get<IGameService>();

        private readonly IUserService _userService =
            NinjectKernel.Instance.Get<IUserService>();

        private GameModel _model = new GameModel();
        private UserModel _user;

        public ObservableCollection<CellViewModel> Cells { get; }
            = new ObservableCollection<CellViewModel>();

        public string UserName => _user?.Name ?? "<нет>";

        public int MoveCount => _model?.MoveCount ?? 0;

        public bool HasUser => _user != null;

        public bool HasGame => _user != null && _model != null && _model.Id > 0;

        public MainWindowViewModel()
        {
        }

        public void CommitUser(UserModel user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            _user = user;
            _model = _service.GetByUserId(user.Id);

            LoadViewModel(_model);
            NotifyUserStateChanged();
        }

        public void Initialize()
        {
            if (_user == null)
            {
                return;
            }

            _model = _service.GetByUserId(_user.Id);
            LoadViewModel(_model);
            NotifyUserStateChanged();
        }

        public void Logout()
        {
            _user = null;
            _model = new GameModel();

            Cells.Clear();

            OnPropertyChanged(nameof(UserName));
            OnPropertyChanged(nameof(MoveCount));
            NotifyUserStateChanged();
        }

        public void DeleteCurrentGame()
        {
            if (!HasGame)
            {
                return;
            }

            _service.RemoveGame(_model.Id);

            _model = _service.GetByUserId(_user.Id);

            LoadViewModel(_model);
            NotifyUserStateChanged();
        }

        public void DeleteCurrentUser()
        {
            if (_user == null)
            {
                return;
            }

            int userId = _user.Id;

            _userService.DeleteUser(userId);

            Logout();
        }

        public void MakeMove(
            MoveDirection direction,
            Action gameFinishedAction)
        {
            if (!HasGame)
            {
                return;
            }

            var updatedModel = _service.MakeMove(_model.Id, direction);

            if (updatedModel == null)
            {
                return;
            }

            _model = updatedModel;
            LoadViewModel(_model);

            if (_service.IsGameOver(_model.Id) == true)
            {
                int completedGameId = _model.Id;

                _service.RemoveGame(completedGameId);

                _model = _service.GetByUserId(_user.Id);
                LoadViewModel(_model);
                NotifyUserStateChanged();

                gameFinishedAction?.Invoke();
            }
        }

        private void LoadViewModel(GameModel model)
        {
            Cells.Clear();

            if (model == null)
            {
                OnPropertyChanged(nameof(MoveCount));
                OnPropertyChanged(nameof(HasGame));
                return;
            }

            for (int row = 0; row < Constants.RowCount; row++)
            {
                for (int column = 0;
                     column < Constants.ColumnCount;
                     column++)
                {
                    if (model[row, column] == Constants.FreeCellValue)
                    {
                        continue;
                    }

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
                        Value = model[row, column],
                        Direction = direction
                    });
                }
            }

            OnPropertyChanged(nameof(MoveCount));
            OnPropertyChanged(nameof(HasGame));
        }

        private void NotifyUserStateChanged()
        {
            OnPropertyChanged(nameof(UserName));
            OnPropertyChanged(nameof(MoveCount));
            OnPropertyChanged(nameof(HasUser));
            OnPropertyChanged(nameof(HasGame));
        }
    }
}
