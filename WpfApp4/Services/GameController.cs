using WpfApp4.Core;
using WpfApp4.ViewModels;
using System.Collections.Generic;
using System.Windows.Media;

namespace WpfApp4.Services
{
    public class StatusInfo
    {
        public string Text { get; set; }
        public Brush Color { get; set; }

        public StatusInfo(string text, Brush color)
        {
            Text = text;
            Color = color;
        }
    }

    public class GameController
    {
        private readonly GameEngine _gameEngine;
        private readonly ViewModelMapper _viewModelMapper;

        public GameEngine GameEngine => _gameEngine;
        public ViewModelMapper ViewModelMapper => _viewModelMapper;

        public GameController()
        {
            _gameEngine = new GameEngine();
            _viewModelMapper = new ViewModelMapper();
        }

        public void InitializeGame()
        {
            _gameEngine.InitializeGame();
        }

        public MoveResult MovePlayer(int deltaRow, int deltaCol)
        {
            return _gameEngine.MovePlayer(deltaRow, deltaCol);
        }

        public void RestartGame()
        {
            _gameEngine.Restart();
        }

        public List<MazeCellViewModel> GetCurrentViewModels()
        {
            return _viewModelMapper.MapToViewModels(_gameEngine);
        }

        public string GetMovesText()
        {
            return $"Ходы: {_gameEngine.MovesCount}";
        }

        public StatusInfo GetStatusInfo()
        {
            if (_gameEngine.IsGameFinished)
            {
                return new StatusInfo(
                    $"🎉 Поздравляем! Вы прошли лабиринт за {_gameEngine.MovesCount} ходов! 🎉",
                    Brushes.DarkGreen
                );
            }
            else
            {
                return new StatusInfo(
                    "Используйте стрелки для движения. Дойдите до выхода (★)!",
                    Brushes.DarkBlue
                );
            }
        }
    }
}