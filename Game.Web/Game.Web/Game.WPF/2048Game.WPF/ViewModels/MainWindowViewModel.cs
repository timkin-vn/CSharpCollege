using _2048Game.Business.Services;
using _2048Game.Business.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows;
using System.Runtime.CompilerServices;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace _2048Game.WPF.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private readonly SokobanService _sokobanService;

        public event PropertyChangedEventHandler PropertyChanged;

        // keep persistent tile viewmodels to be able to animate specific cells
        public ObservableCollection<SokobanTileViewModel> Tiles { get; set; }

        public int RemainingBoxes { get; private set; }
        public int RemainingTargets { get; private set; }

        public MainWindowViewModel()
        {
            _sokobanService = new SokobanService();
            Tiles = new ObservableCollection<SokobanTileViewModel>();

            _sokobanService.BoardUpdated += UpdateTiles;
            _sokobanService.LevelCompleted += OnLevelCompleted;
            _sokobanService.BoxPlaced += OnBoxPlaced;

            // initialize view
            UpdateTiles();
        }

        private void UpdateTiles()
        {
            var board = _sokobanService.Board;
            if (board == null) return;

            // ensure Tiles collection size matches board
            int total = board.Rows * board.Cols;
            if (Tiles.Count != total)
            {
                Tiles.Clear();
                for (int i = 0; i < total; i++) Tiles.Add(new SokobanTileViewModel());
            }

            int idx = 0;
            int boxes = 0;
            int targets = 0;
            for (int r = 0; r < board.Rows; r++)
            {
                for (int c = 0; c < board.Cols; c++)
                {
                    int value = board.Cells[r, c];
                    var vm = Tiles[idx++];
                    vm.Value = value;
                    vm.IsRecentlyPlaced = false;

                    if (value == 2 || value == 5) boxes++;
                    if (value == 3 || value == 5) targets++;

                    vm.OnPropertyChanged(nameof(vm.Value));
                    vm.OnPropertyChanged(nameof(vm.Background));
                    vm.OnPropertyChanged(nameof(vm.Text));
                    vm.OnPropertyChanged(nameof(vm.Foreground));
                }
            }

            RemainingBoxes = boxes;
            RemainingTargets = targets;
            OnPropertyChanged(nameof(RemainingBoxes));
            OnPropertyChanged(nameof(RemainingTargets));

            OnPropertyChanged(nameof(Tiles));
        }

        private async void OnBoxPlaced(int r, int c)
        {
            var board = _sokobanService.Board;
            if (board == null) return;
            int idx = r * board.Cols + c;
            if (idx >= 0 && idx < Tiles.Count)
            {
                var vm = Tiles[idx];
                vm.IsRecentlyPlaced = true;
                vm.OnPropertyChanged(nameof(vm.IsRecentlyPlaced));

                // short animation flag duration
                await Task.Delay(400);

                vm.IsRecentlyPlaced = false;
                vm.OnPropertyChanged(nameof(vm.IsRecentlyPlaced));
            }

            // update counters after placement
            UpdateTiles();
        }

        public void HandKey(Key key)
        {
            switch (key)
            {
                case Key.Up:
                    _sokobanService.Move(SokobanDirection.Up);
                    break;
                case Key.Down:
                    _sokobanService.Move(SokobanDirection.Down);
                    break;
                case Key.Left:
                    _sokobanService.Move(SokobanDirection.Left);
                    break;
                case Key.Right:
                    _sokobanService.Move(SokobanDirection.Right);
                    break;
            }
        }

        public void RestartLevel()
        {
            _sokobanService.StartDefaultLevel();
            UpdateTiles();
        }

        private void OnLevelCompleted()
        {
            // show completion and disable further moves by not restarting automatically
            MessageBox.Show("Поздравляем! Все коробки размещены на целях. Игра завершена.", "Игра завершена", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
