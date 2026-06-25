using FifteenGame.Business.Models;
using FifteenGame.Business.Services;
using FifteenGame.Common.BusinessModels;
using FifteenGame.Common.Contracts.Services;
using FifteenGame.Common.Definitions;
using FifteenGame.Common.Infrastructure;
using Ninject;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FifteenGame.Wpf.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private IMazeGameService _mazeGameService = NinjectKernel.Instance.Get<IMazeGameService>();

        private UserModel _user;

        public ObservableCollection<CellViewModel> Cells { get; } = new ObservableCollection<CellViewModel>();

        public string UserName => _user?.Name ?? "<нет>";

        public int MoveCount => _user != null ? _mazeGameService.GetMazeGameByUserId(_user.Id)?.Player?.Moves ?? 0 : 0;

        public MainWindowViewModel()
        {
            // No initial game load here, will be handled by CommitUser or Initialize
        }

        public void CommitUser(UserModel user)
        {
            _user = user;
            OnPropertyChanged(nameof(UserName));

            _mazeGameService.StartNewMazeGame(_user.Id);
            LoadViewModel(_mazeGameService.GetMazeGameByUserId(_user.Id));
        }

        public void Initialize()
        {
            LoadViewModel(_mazeGameService.GetMazeGameByUserId(_user.Id));
        }

        public void MakeMove(int deltaRow, int deltaCol, Action gameFinishedAction)
        {
            var gameManager = _mazeGameService.MakeMazeMove(_user.Id, deltaRow, deltaCol);
            LoadViewModel(gameManager);
            if (_mazeGameService.IsMazeGameOver(_user.Id))
            {
                gameFinishedAction?.Invoke();
            }
        }

        private void LoadViewModel(GameManager gameManager)
        {
            Cells.Clear();
            if (gameManager == null) return;

            var level = gameManager.CurrentLevel;
            var player = gameManager.Player;

            for (int r = 0; r < Level.Rows; r++)
            {
                for (int c = 0; c < Level.Columns; c++)
                {
                    var cell = level.Grid[r, c];
                    if (cell != null)
                    {
                        var vm = new CellViewModel(r, c, cell.Type);

                        // Игрок
                        if (r == player.Row && c == player.Column)
                        {
                            vm.BackgroundColor = "#FF4444";
                            vm.DisplayText = "😊";
                        }

                        // Переключатель
                        if (cell.Type == CellType.Switch && level.SwitchStates.ContainsKey((r, c)))
                        {
                            bool active = level.SwitchStates[(r, c)];
                            vm.DisplayText = active ? "🔹" : "⚡";
                            vm.BackgroundColor = active ? "#66FF66" : "#FFAA66";
                        }

                        // Дверь
                        if (cell.Type == CellType.Door)
                        {
                            bool isOpen = level.DoorStates.ContainsKey((r, c)) && level.DoorStates[(r, c)];
                            vm.DisplayText = isOpen ? "🚪(открыта)" : "🚪(закрыта)";
                            vm.BackgroundColor = isOpen ? "#66FF66" : "#CC9966";
                        }

                        // Выход
                        if (cell.Type == CellType.Exit)
                        {
                            bool canExit = level.AreAllSwitchesActive();
                            vm.DisplayText = canExit ? "🚪(выход)" : "🚪(закрыт)";
                            vm.BackgroundColor = canExit ? "#66FF66" : "#FF6666";
                        }

                        Cells.Add(vm);
                    }
                }
            }
            OnPropertyChanged(nameof(MoveCount));
        }
    }
}
