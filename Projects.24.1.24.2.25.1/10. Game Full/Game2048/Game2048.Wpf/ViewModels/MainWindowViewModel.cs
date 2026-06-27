using Game2048.Common.BusinessModels;
using Game2048.Common.Contracts.Services;
using Game2048.Common.Definitions;
using Ninject;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using TwentyFortyEight.Wpf.ViewModels;

namespace Game2048.Wpf
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly IGameService _matchEngine;
        private GameModel _sessionData;
        private UserModel _userProfile;

        public ObservableCollection<CellViewModel> GridCells { get; } = new ObservableCollection<CellViewModel>();

        public string UserName => _userProfile?.Name ?? "Гость";
        public int Score => _sessionData?.Score ?? 0;
        public bool IsGameActive => _userProfile != null;

        public int BestTile
        {
            get
            {
                if (_sessionData == null) return 0;

                int highestValue = 0;
                int dimensions = Constants.GridSize;

                for (int r = 0; r < dimensions; r++)
                {
                    for (int c = 0; c < dimensions; c++)
                    {
                        if (_sessionData[r, c] > highestValue)
                        {
                            highestValue = _sessionData[r, c];
                        }
                    }
                }

                return highestValue;
            }
        }

        public MainWindowViewModel()
        {
            var container = Application.Current.Resources["ServiceProvider"] as IKernel;
            _matchEngine = container?.Get<IGameService>();
        }

        public void CommitUser(UserModel user)
        {
            _userProfile = user;
            _sessionData = _matchEngine.GetByUserId(user.Id);

            SyncInterfaceGrid();

            OnPropertyChanged(nameof(IsGameActive));
            OnPropertyChanged(nameof(UserName));
        }

        public void MakeMove(MoveDirection side, Action finishCallback, Action successCallback)
        {
            if (_sessionData == null) return;

            _sessionData = _matchEngine.MakeMove(_sessionData.Id, side);
            SyncInterfaceGrid();

            if ((_matchEngine.IsGameWon(_sessionData.Id) ?? false) && _sessionData.IsWon)
            {
                _sessionData.IsWon = false;
                successCallback?.Invoke();
            }
            else if (_matchEngine.IsGameOver(_sessionData.Id) ?? false)
            {
                finishCallback?.Invoke();
            }
        }

        public void NewGame()
        {
            if (_userProfile == null || _sessionData == null) return;

            _matchEngine.RemoveGame(_sessionData.Id);
            _sessionData = _matchEngine.GetByUserId(_userProfile.Id);
            SyncInterfaceGrid();
        }

        private void SyncInterfaceGrid()
        {
            GridCells.Clear();

            int dimensions = Constants.GridSize;

            for (int r = 0; r < dimensions; r++)
            {
                for (int c = 0; c < dimensions; c++)
                {
                    var item = new CellViewModel
                    {
                        Row = r,
                        Column = c,
                        Value = _sessionData[r, c]
                    };
                    GridCells.Add(item);
                }
            }

            OnPropertyChanged(nameof(BestTile));
            OnPropertyChanged(nameof(Score));
        }
    }
}