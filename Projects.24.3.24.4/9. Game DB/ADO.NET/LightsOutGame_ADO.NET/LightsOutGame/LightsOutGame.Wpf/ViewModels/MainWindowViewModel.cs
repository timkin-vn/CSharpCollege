using LightsOutGame.Business.Services;
using LightsOutGame.Common.BusinessModels;
using LightsOutGame.Common.Contracts.Services;
using LightsOutGame.Common.Definitions;
using LightsOutGame.Common.Infrastucture;
using Ninject;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightsOutGame.Wpf.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private IGameService _service = NinjectKernel.Instance.Get<IGameService>();

        private GameModel _model = new GameModel();

        private UserModel _user;

        public ObservableCollection<CellViewModel> Cells { get; } = new ObservableCollection<CellViewModel>();

        public string UserName => _user?.Name ?? "<нет>";

        public int MoveCount => _model?.MoveCount ?? 0;

        public MainWindowViewModel()
        {
            _model = new GameModel();
            LoadViewModel(_model);
        }

        public void CommitUser(UserModel userModel)
        {
            _user = userModel;
            OnPropertyChanged(nameof(UserName));

            _model = _service.GetByUserId(_user.Id);
            LoadViewModel(_model);
        }

        public void Initialize()
        {
            _model = _service.GetByUserId(_user.Id);
            LoadViewModel(_model);
        }

        public void PressCell(CellViewModel cell, Action gameFinishedAction)
        {
            _model = _service.PressCell(_model.Id, cell.Row, cell.Column);
            LoadViewModel(_model);

            if (_service.IsGameOver(_model.Id))
            {
                _service.RemoveGame(_model.Id);
                gameFinishedAction?.Invoke();
            }
        }

        private void LoadViewModel(GameModel model)
        {
            Cells.Clear();
            for (int row = 0; row < Constants.RowCount; row++)
            {
                for (int column = 0; column < Constants.ColumnCount; column++)
                {
                    Cells.Add(new CellViewModel
                    {
                        Row = row,
                        Column = column,
                        IsOn = model[row, column],
                    });
                }
            }

            OnPropertyChanged(nameof(MoveCount));
        }
    }
}
