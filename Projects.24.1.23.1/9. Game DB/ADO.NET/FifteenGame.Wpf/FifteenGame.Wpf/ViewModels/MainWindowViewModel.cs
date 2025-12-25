using FifteenGame.Business.Models;
using FifteenGame.Business.Services;
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

        public int MoveCount => _model?.MatchesCount ?? 0;

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
            _model = new GameModel();
            _service.Initialize(_model);
            FromModel(_model);
        }

        public void ReInitialize()
        {
            _model = _service.GetByUserId(_user.Id);
            FromModel(_model);
        }

        public void MakeMove(int r1, int c1, int r2, int c2, Action gameFinishAction)
        {
            if (_service.Swap(_model, r1, c1, r2, c2))
            {
                _service.ProcessMatches(_model);
                FromModel(_model);

                if (_model.IsFinished)
                {
                    _service.RemoveGame(_model.Id);
                    gameFinishAction?.Invoke();
                }
            }
        }


        private void FromModel(GameModel model)
        {
            Cells.Clear();
            for (int row = 0; row < GameModel.RowCount; row++)
            {
                for (int column = 0; column < GameModel.ColumnCount; column++)
                {
                    Cells.Add(new CellViewModel
                    {
                        Row = row,
                        Column = column,
                        Num = model[row, column]
                    });
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
