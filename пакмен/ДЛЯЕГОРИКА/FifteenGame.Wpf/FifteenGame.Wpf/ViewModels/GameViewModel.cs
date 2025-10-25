using System.ComponentModel;
using System.Timers;
using StepByStepPacman.Business;
using StepByStepPacman.Business.Models;
using StepByStepPacman.Business.Services;

namespace StepByStepPacman.WPF.ViewModels
{
    public class GameViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private readonly GameService _gameService = new GameService();
        private readonly Timer _timer;

        public GameState State => _gameService.State;

        public GameViewModel()
        {
            _timer = new Timer(50);
            _timer.Elapsed += Timer_Elapsed;
            _timer.Start();
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Update();
        }

        private void Update()
        {
            _gameService.Update();
            OnPropertyChanged("State");
        }

        public void SetDirection(Direction direction)
        {
            _gameService.State.Player.Direction = direction;
        }

        public void Restart()
        {
            _gameService.Restart();
            OnPropertyChanged("State");
        }

        private void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}