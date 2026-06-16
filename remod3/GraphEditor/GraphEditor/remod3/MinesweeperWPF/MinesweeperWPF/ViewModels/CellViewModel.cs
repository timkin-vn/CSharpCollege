using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Media;
using Minesweeper.Business;
using MinesweeperWPF.Business.Cells;

namespace MinesweeperWPF.ViewModels;

public sealed class CellViewModel : INotifyPropertyChanged {
    public int Row { get; }
    public int Column { get; }

    private CellState _state;
    private int _adjacent;
    private string _text = string.Empty;
    private Brush _background = Brushes.WhiteSmoke;
    private Brush _foreground = Brushes.Black;
    private bool _isEnabled = true;

    public CellState State { get => _state; private set { _state = value; OnChanged(); } }
    public int AdjacentMines { get => _adjacent; private set { _adjacent = value; OnChanged(); } }

    public string Text { get => _text; private set { _text = value; OnChanged(); } }
    public Brush Background { get => _background; private set { _background = value; OnChanged(); } }
    public Brush Foreground { get => _foreground; private set { _foreground = value; OnChanged(); } }
    public bool IsEnabled { get => _isEnabled; private set { _isEnabled = value; OnChanged(); } }

    public CellViewModel(int row, int column) {
        Row = row;
        Column = column;
        Apply(new CellUpdate(row, column, CellState.Hidden, 0));
    }

    public void Apply(CellUpdate update) {
        State = update.State;
        AdjacentMines = update.AdjacentMines;

        switch (update.State) {
            case CellState.Hidden:
                IsEnabled = true;
                Text = string.Empty;
                Background = Brushes.WhiteSmoke;
                Foreground = Brushes.Black;
                break;

            case CellState.Flagged:
                IsEnabled = true;
                Text = "⚑";
                Background = Brushes.WhiteSmoke;
                Foreground = Brushes.IndianRed;
                break;

            case CellState.Questioned:
                IsEnabled = true;
                Text = "?";
                Background = Brushes.WhiteSmoke;
                Foreground = Brushes.DarkOrange;
                break;

            case CellState.Revealed:
                IsEnabled = false;
                Background = Brushes.White;
                Text = update.AdjacentMines > 0 ? update.AdjacentMines.ToString() : string.Empty;
                Foreground = NumberBrush(update.AdjacentMines);
                break;

            case CellState.Mine:
                IsEnabled = false;
                Background = Brushes.MistyRose;
                Text = "💣";
                Foreground = Brushes.Black;
                break;

            case CellState.Exploded:
                IsEnabled = false;
                Background = Brushes.IndianRed;
                Text = "💣";
                Foreground = Brushes.White;
                break;

            default:
                throw new ArgumentOutOfRangeException(nameof(update.State), update.State, "Unknown cell state");
        }
    }

    private static Brush NumberBrush(int n) => n switch {
        1 => Brushes.Blue,
        2 => Brushes.Green,
        3 => Brushes.Red,
        4 => Brushes.Navy,
        5 => Brushes.Maroon,
        6 => Brushes.Teal,
        7 => Brushes.Black,
        8 => Brushes.Gray,
        _ => Brushes.Black
    };

    public event PropertyChangedEventHandler? PropertyChanged;
    private void OnChanged([CallerMemberName] string? name = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}
