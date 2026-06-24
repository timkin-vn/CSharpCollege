using FifteenGame.Common.BusinessModels;
using FifteenGame.Common.Contracts.Services;
using FifteenGame.Common.Infrastructure;
using Ninject;
using System.Collections.ObjectModel;

namespace FifteenGame.Wpf.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly IGameService _service =
            NinjectKernel.Instance.Get<IGameService>();

        private GameModel _model;

        private UserModel _user;

        public ObservableCollection<CellViewModel> Cells { get; } =
            new ObservableCollection<CellViewModel>();

        public string UserName =>
            _user?.Name ?? "<нет>";

        public int Score =>
            _model?.Score ?? 0;

        public int BestTile =>
            _model?.BestTile ?? 0;

        public MainWindowViewModel()
        {
            CreateEmptyBoard();
        }

        public void CommitUser(UserModel user)
        {
            _user = user;

            OnPropertyChanged(nameof(UserName));

            _model = _service.GetByUserId(user.Id);

            LoadBoard();
        }

        public void NewGame()
        {
            _model = _service.GetByUserId(_user.Id);

            LoadBoard();
        }

        public void MoveLeft()
        {
            if (_model == null)
                return;

            _model = _service.MoveLeft(_model.Id);

            LoadBoard();
        }

        public void MoveRight()
        {
            if (_model == null)
                return;

            _model = _service.MoveRight(_model.Id);

            LoadBoard();
        }

        public void MoveUp()
        {
            if (_model == null)
                return;

            _model = _service.MoveUp(_model.Id);

            LoadBoard();
        }

        public void MoveDown()
        {
            if (_model == null)
                return;

            _model = _service.MoveDown(_model.Id);

            LoadBoard();
        }

        private void CreateEmptyBoard()
        {
            Cells.Clear();

            for (int row = 0; row < 4; row++)
            {
                for (int col = 0; col < 4; col++)
                {
                    Cells.Add(new CellViewModel
                    {
                        Row = row,
                        Column = col,
                        Value = 0
                    });
                }
            }
        }

        private void LoadBoard()
        {
            Cells.Clear();

            for (int row = 0; row < 4; row++)
            {
                for (int col = 0; col < 4; col++)
                {
                    Cells.Add(new CellViewModel
                    {
                        Row = row,
                        Column = col,
                        Value = _model[row, col]
                    });
                }
            }

            OnPropertyChanged(nameof(Score));
            OnPropertyChanged(nameof(BestTile));
        }
    }
}