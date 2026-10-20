using System.ComponentModel;
using System.Runtime.CompilerServices;
using TicTacToeGame.Common;

namespace TicTacToeClient
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private GameState _gameState = new GameState();

        public GameState GameState
        {
            get => _gameState;
            set { _gameState = value; OnPropertyChanged(); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}