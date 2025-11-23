using FifteenGame.Business.Models;
using FifteenGame.Data.Entities;
using FifteenGame.Wpf.Commands;
using System;
using System.Windows.Input;

namespace FifteenGame.Wpf.ViewModels
{
    public class ModeSelectionViewModel
    {
        private readonly User _currentUser;
        public event Action<GameMode> OnGameModeSelected;

        public string WelcomeMessage => $"Добро пожаловать, {_currentUser.Username}!";
        public string BestScoreInfo => $"Ваш лучший рекорд: {_currentUser.BestScore}";

        public ICommand StartClassicCommand { get; }
        public ICommand StartRankedCommand { get; }

        public ModeSelectionViewModel(User user)
        {
            _currentUser = user;
            StartClassicCommand = new RelayCommand(() => SelectMode(GameMode.Classic));
            StartRankedCommand = new RelayCommand(() => SelectMode(GameMode.Ranked));
        }

        private void SelectMode(GameMode mode)
        {
            OnGameModeSelected?.Invoke(mode);
        }
    }
}