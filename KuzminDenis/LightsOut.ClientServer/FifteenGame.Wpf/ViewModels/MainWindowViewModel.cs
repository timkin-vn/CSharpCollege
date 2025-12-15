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
        public int MovesLeft => _model?.MoveCount ?? 0;

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
            ReInitialize();
        }

        public void ReInitialize()
        {
            if (_user != null)
            {
                _model = _service.GetByUserId(_user.Id);
                FromModel(_model);
            }
        }

        public void MakeMoveAt(int row, int column, Action gameWinAction, Action gameLoseAction)
        {
            _model = _service.MakeMove(_model.Id, row, column);

            FromModel(_model);

            if (_service.IsGameOver(_model.Id))
            {
                _service.RemoveGame(_model.Id);

                bool isWin = true;
                for (int r = 0; r < GameModel.RowCount; r++)
                {
                    for (int c = 0; c < GameModel.ColumnCount; c++)
                    {
                        if (_model[r, c])
                        {
                            isWin = false;
                            break;
                        }
                    }
                    if (!isWin) break;
                }

                if (isWin)
                {
                    gameWinAction?.Invoke();
                }
                else
                {
                    gameLoseAction?.Invoke();
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
                        IsOn = model[row, column]
                    });
                }
            }
            OnPropertyChanged(nameof(MovesLeft));
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}