using FifteenGame.Common.BusinessDtos;
using FifteenGame.Common.Enums;
using FifteenGame.Wpf.Infrastructure;
using System;
using System.Windows.Input;

namespace FifteenGame.Wpf.ViewModels
{
    public class ModeSelectionViewModel
    {
        private readonly UserDto _currentUser;
        public event Action<GameMode> OnGameModeSelected;

        public string WelcomeMessage => $"Добро пожаловать, {_currentUser.Username}!";
        public string BestScoreInfo => $"Ваш лучший рекорд: {_currentUser.BestScore}";

        public ICommand StartClassicCommand { get; }
        public ICommand StartRankedCommand { get; }

        public ModeSelectionViewModel(UserDto user)
        {
            _currentUser = user;
            StartClassicCommand = new RelayCommand(o => SelectMode(GameMode.Classic));
            StartRankedCommand = new RelayCommand(o => SelectMode(GameMode.Ranked));
        }

        private void SelectMode(GameMode mode)
        {
            OnGameModeSelected?.Invoke(mode);
        }
    }
}