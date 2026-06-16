using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Threading;
using Minesweeper.Business;
using MinesweeperWPF.Business.Cells;
using MinesweeperWPF.Business.Results;
using GameBoard = MinesweeperWPF.Business.Core.GameBoard;

namespace MinesweeperWPF.ViewModels;

public sealed class MainViewModel : INotifyPropertyChanged {
    private readonly GameBoard _game = new();
    private readonly DispatcherTimer _timer = new() { Interval = TimeSpan.FromSeconds(1) };

    private GameSettings _settings = GameSettings.Intermediate();
    private int _seconds;

    private int _selectedDifficultyIndex = 1;

    public ObservableCollection<CellViewModel> Cells { get; private set; } = new();

    public int Rows => _settings.Rows;
    public int Columns => _settings.Columns;
    public int MinesSetting => _settings.Mines;

    public int FlagsLeft => _game.FlagsLeft;
    public int Seconds => _seconds;

    public int CellSize { get; } = 28;

    public int SelectedDifficultyIndex {
        get => _selectedDifficultyIndex;
        set {
            if (_selectedDifficultyIndex == value) return;

            _selectedDifficultyIndex = value;
            OnChanged();

            if (value == 3) {
                CustomDifficultyRequested?.Invoke(this, EventArgs.Empty);
                return;
            }

            ApplyDifficulty(value);
            BuildGridAndStart();
        }
    }

    public RelayCommand NewGameCommand { get; }
    public RelayCommand CellLeftClickCommand { get; }
    public RelayCommand CellRightClickCommand { get; }

    public event EventHandler? CustomDifficultyRequested;
    public event EventHandler<GameEndedEventArgs>? GameEnded;

    public MainViewModel() {
        _timer.Tick += (_, _) => {
            _seconds++;
            OnChanged(nameof(Seconds));
        };

        NewGameCommand = new RelayCommand(_ => BuildGridAndStart());

        CellLeftClickCommand = new RelayCommand(p => {
            if (_game.GameOver) return;
            if (p is not CellViewModel cell) return;

            var result = cell.State == CellState.Revealed
                ? _game.Chord(cell.Row, cell.Column)
                : _game.Reveal(cell.Row, cell.Column);

            ApplyResult(result);
        });

        CellRightClickCommand = new RelayCommand(p => {
            if (_game.GameOver) return;
            if (p is not CellViewModel cell) return;

            ApplyResult(_game.ToggleFlag(cell.Row, cell.Column));
        });

        BuildGridAndStart();
    }

    public void SetCustomDifficulty(int rows, int cols, int mines) {
        mines = Math.Min(mines, rows * cols - 1);
        _settings = new GameSettings(rows, cols, mines);

        OnChanged(nameof(Rows));
        OnChanged(nameof(Columns));
        OnChanged(nameof(MinesSetting));

        BuildGridAndStart();
    }

    public void SetDifficultyIndex(int index) {
        _selectedDifficultyIndex = index;
        OnChanged(nameof(SelectedDifficultyIndex));
    }

    public int DifficultyIndexFromCurrent() =>
        _settings switch {
            var s when s == GameSettings.Beginner() => 0,
            var s when s == GameSettings.Intermediate() => 1,
            var s when s == GameSettings.Expert() => 2,
            _ => 3
        };

    private void ApplyDifficulty(int index) {
        _settings = index switch {
            0 => GameSettings.Beginner(),
            1 => GameSettings.Intermediate(),
            2 => GameSettings.Expert(),
            _ => _settings
        };

        OnChanged(nameof(Rows));
        OnChanged(nameof(Columns));
        OnChanged(nameof(MinesSetting));
    }

    private void BuildGridAndStart() {
        _timer.Stop();
        _seconds = 0;
        OnChanged(nameof(Seconds));

        _game.ApplySettings(_settings);
        _game.NewGame();

        var list = new ObservableCollection<CellViewModel>();
        for (var r = 0; r < _settings.Rows; r++)
        for (var c = 0; c < _settings.Columns; c++)
            list.Add(new CellViewModel(r, c));

        Cells = list;
        OnChanged(nameof(Cells));
        OnChanged(nameof(FlagsLeft));
    }

    private void ApplyResult(GameActionResult result) {
        if (result.Updates.Count == 0)
            return;

        if (_game.HasStarted && !_timer.IsEnabled)
            _timer.Start();

        foreach (var u in result.Updates)
            Cell(u.Row, u.Column).Apply(u);

        OnChanged(nameof(FlagsLeft));

        if (!result.GameOver)
            return;

        _timer.Stop();

        if (result.HitMine) {
            foreach (var mine in _game.RevealAllMines())
                Cell(mine.Row, mine.Column).Apply(mine);
        }

        GameEnded?.Invoke(this, new GameEndedEventArgs(result.HasWon, _seconds));
    }

    private CellViewModel Cell(int row, int col) => Cells[row * _settings.Columns + col];

    public event PropertyChangedEventHandler? PropertyChanged;
    private void OnChanged([CallerMemberName] string? name = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}

public sealed class GameEndedEventArgs(bool hasWon, int seconds) : EventArgs {
    public bool HasWon { get; } = hasWon;
    public int Seconds { get; } = seconds;
}
