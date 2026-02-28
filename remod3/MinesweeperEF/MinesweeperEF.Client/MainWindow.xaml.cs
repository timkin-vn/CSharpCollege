using System.Text.Json;
using System.Text.Json.Serialization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using MinesweeperEF.Business.Cells;
using MinesweeperEF.Business.Models;
using MinesweeperEF.Client.BusinessProxy;
using MinesweeperEF.Client.BusinessProxy.Services;
using MinesweeperEF.Contracts.Games;

namespace MinesweeperEF.Client;

public partial class MainWindow {
    private ApiClient? _api;
    private UserServiceProxy? _users;
    private GameServiceProxy? _games;

    private Guid? _currentGameId;
    private string _currentGameName = "";
    private GameStateDto? _currentState;

    private static readonly Brush RevealedBrush = new SolidColorBrush(Color.FromRgb(230, 230, 230));
    private static readonly Brush[] NumberBrushes = [
        Brushes.Transparent,
        Brushes.SteelBlue,
        Brushes.ForestGreen,
        Brushes.Firebrick,
        Brushes.MidnightBlue,
        Brushes.DarkRed,
        Brushes.MediumVioletRed,
        Brushes.Black,
        Brushes.DarkSlateGray
    ];
    private static readonly JsonSerializerOptions JsonOptions = new() {
        PropertyNameCaseInsensitive = true,
        NumberHandling = JsonNumberHandling.AllowReadingFromString
    };

    public MainWindow() {
        InitializeComponent();
        UpdateStatus(null);
    }

    private bool EnsureApiInitialized() {
        if (_api is not null) return true;
    
        var url = ApiUrlBox.Text.Trim();
        if (string.IsNullOrWhiteSpace(url)) {
            MessageBox.Show("Укажите URL API");
            return false;
        }

        _api = new ApiClient(url);
        _users = new UserServiceProxy(_api);
        _games = new GameServiceProxy(_api);
        return true;
    }

    private async void Register_Click(object sender, RoutedEventArgs e) {
        if (!EnsureApiInitialized()) return;

        try {
            await _users!.RegisterAsync(UserBox.Text, PassBox.Password);
            MessageBox.Show("Пользователь зарегистрирован");
        }
        catch (Exception ex) {
            MessageBox.Show($"Ошибка регистрации: {ex.Message}");
        }
    }

    private async void Login_Click(object sender, RoutedEventArgs e) {
        if (!EnsureApiInitialized()) return;

        try {
            await _users!.LoginAsync(UserBox.Text, PassBox.Password);
            MessageBox.Show("Вход выполнен");
            await RefreshGamesAsync();
        }
        catch (Exception ex) {
            MessageBox.Show($"Ошибка входа: {ex.Message}");
        }
    }
    
    private void ClearForm_Click(object sender, RoutedEventArgs e) {
        RowsBox.Text = "16";
        ColsBox.Text = "16";
        MinesBox.Text = "40";
        NameBox.Text = "Game";
    }

    private async void NewGame_Click(object sender, RoutedEventArgs e) {
        if (_games is null && !EnsureApiInitialized()) return;
        if (_games is null) return;

        if (!TryReadSettings(out var rows, out var cols, out var mines)) return;

        try {
            var name = string.IsNullOrWhiteSpace(NameBox.Text) ? "Game" : NameBox.Text.Trim();
            var snapshot = await _games.NewGameAsync(rows, cols, mines, name);
            _currentGameName = name;
            RenderSnapshot(snapshot);
            await RefreshGamesAsync();
        }
        catch (Exception ex) {
            MessageBox.Show($"Ошибка создания игры: {ex.Message}");
        }
    }

    private async void GamesList_SelectionChanged(object sender, SelectionChangedEventArgs e) {
        if (_games is null) return;
        if (GamesList.SelectedItem is not SavedGameInfoDto info) return;

        try {
            var snapshot = await _games.GetAsync(info.GameId);
            _currentGameName = info.Name;
            RenderSnapshot(snapshot);
        }
        catch (Exception ex) {
            MessageBox.Show($"Ошибка загрузки игры: {ex.Message}");
        }
    }

    private async void RefreshGames_Click(object sender, RoutedEventArgs e) {
        await RefreshGamesAsync();
    }

    private async Task RefreshGamesAsync() {
        if (_games is null) return;
        try {
            var list = await _games.ListAsync();
            GamesList.ItemsSource = list;
        }
        catch (Exception ex) {
            MessageBox.Show($"Ошибка загрузки списка: {ex.Message}");
        }
    }

    private void RenderSnapshot(GameSnapshotDto snapshot) {
        _currentGameId = snapshot.GameId;
        _currentState = JsonSerializer.Deserialize<GameStateDto>(snapshot.StateJson, JsonOptions);
        if (_currentState is null) {
            MessageBox.Show("Не удалось прочитать состояние поля");
            return;
        }
        
        BoardInfoText.Text = $"{_currentState.Settings.Rows} x {_currentState.Settings.Columns}";
        GameNameText.Text = string.IsNullOrWhiteSpace(_currentGameName) ? "Новая игра" : _currentGameName;

        BoardGrid.Children.Clear();
        BoardGrid.Rows = _currentState.Settings.Rows;
        BoardGrid.Columns = _currentState.Settings.Columns;

        for (var r = 0; r < _currentState.Settings.Rows; r++) {
            for (var c = 0; c < _currentState.Settings.Columns; c++) {
                var idx = r * _currentState.Settings.Columns + c;
                var cell = _currentState.Cells[idx];

                var btn = new Button {
                    Tag = (r, c),
                    Width = 36,
                    Height = 36,
                    MinWidth = 36,
                    MinHeight = 36,
                    MaxWidth = 36,
                    MaxHeight = 36,
                    Margin = new Thickness(1),
                    Padding = new Thickness(0),
                    FontWeight = FontWeights.Bold,
                    FontSize = 18,
                    FocusVisualStyle = null,
                    HorizontalContentAlignment = HorizontalAlignment.Center,
                    VerticalContentAlignment = VerticalAlignment.Center
                };

                UpdateButtonVisual(btn, cell);

                btn.Click += Cell_Click;
                btn.MouseRightButtonUp += Cell_RightClick;
                btn.MouseDoubleClick += Cell_DoubleClick;
                BoardGrid.Children.Add(btn);
            }
        }

        UpdateStatus(snapshot);
    }

    private void UpdateStatus(GameSnapshotDto? snapshot) {
        if (snapshot is null || _currentState is null) {
            StatusText.Text = "Нет активной игры";
            FlagsText.Text = string.Empty;
            BoardInfoText.Text = string.Empty;
            GameNameText.Text = string.Empty;
            return;
        }

        var status = snapshot.GameOver
            ? (snapshot.HasWon ? "Победа" : IsDebugMode ? "Поражение (Debug)" : "Поражение")
            : "Игра продолжается";

        StatusText.Text = status;
        FlagsText.Text = $"Флагов осталось: {snapshot.FlagsLeft}";
        
        if (IsDebugMode && snapshot is { GameOver: true, HasWon: false }) {
            StatusText.Foreground = new SolidColorBrush(Color.FromRgb(255, 183, 77));
        } else {
            StatusText.Foreground = (Brush)FindResource("AccentBrush");
        }
        
        var showRevealMines = !IsDebugMode && snapshot is { GameOver: true, HasWon: false };
        RevealMinesButton.Visibility = showRevealMines ? Visibility.Visible : Visibility.Collapsed;
    }

    private void UpdateButtonVisual(Button btn, CellDto cell) {
        btn.Background = Brushes.LightGray;
        btn.Content = string.Empty;

        switch (cell.State) {
            case CellState.Hidden:
                btn.Content = string.Empty;
                break;
            case CellState.Flagged:
                btn.Content = "🚩";
                break;
            case CellState.Questioned:
                btn.Content = "?";
                break;
            case CellState.Mine:
                btn.Content = "💣";
                btn.Background = Brushes.IndianRed;
                break;
            case CellState.Exploded:
                btn.Content = "💥";
                btn.Background = Brushes.OrangeRed;
                break;
            case CellState.Revealed:
                btn.Background = RevealedBrush;
                if (cell.AdjacentMines > 0) {
                    btn.Content = cell.AdjacentMines.ToString();
                    btn.Foreground = NumberBrushes[Math.Min(cell.AdjacentMines, NumberBrushes.Length - 1)];
                }
                else {
                    btn.Content = string.Empty;
                }
                break;
        }
    }

    private bool _isDoubleClick;
    private bool IsDebugMode => DebugModeCheck?.IsChecked == true;

    private async void Cell_Click(object sender, RoutedEventArgs e) {
        if (_games is null || _currentState is null || _currentGameId is null) return;
        if (sender is not Button btn || btn.Tag is not ValueTuple<int, int> coords) return;

        if (_isDoubleClick) {
            _isDoubleClick = false;
            return;
        }

        await Task.Delay(200);
        
        if (_isDoubleClick) {
            _isDoubleClick = false;
            return;
        }

        var (row, col) = coords;

        const GameActionType action = GameActionType.Reveal;
        await SendActionAsync(action, row, col, IsDebugMode);
    }
    
    private async void Cell_DoubleClick(object sender, MouseButtonEventArgs e) {
        if (_games is null || _currentState is null || _currentGameId is null) return;
        if (sender is not Button btn || btn.Tag is not ValueTuple<int, int> coords) return;

        _isDoubleClick = true;
        e.Handled = true;

        var (row, col) = coords;
        await SendActionAsync(GameActionType.Chord, row, col, IsDebugMode);
    }

    private async void Cell_RightClick(object sender, MouseButtonEventArgs e) {
        if (_games is null || _currentState is null || _currentGameId is null) return;
        if (sender is not Button btn || btn.Tag is not ValueTuple<int, int> coords) return;
        e.Handled = true;

        var (row, col) = coords;
        await SendActionAsync(GameActionType.ToggleFlag, row, col, IsDebugMode);
    }

    private async Task SendActionAsync(GameActionType type, int row, int col, bool debugMode) {
        if (_games is null || _currentGameId is null) return;

        try {
            var snapshot = await _games.ActionAsync(_currentGameId.Value, type, row, col, debugMode);
            
            RenderSnapshot(snapshot);
        }
        catch (Exception ex) {
            MessageBox.Show($"Ошибка хода: {ex.Message}");
        }
    }
    
    private async void RevealMines_Click(object sender, RoutedEventArgs e) {
        if (_games is null || _currentGameId is null) return;

        try {
            var snapshot = await _games.ActionAsync(_currentGameId.Value, GameActionType.RevealMines, 0, 0, false);
            RenderSnapshot(snapshot);
        }
        catch (Exception ex) {
            MessageBox.Show($"Не удалось открыть все мины: {ex.Message}");
        }
    }
    
    private bool TryReadSettings(out int rows, out int cols, out int mines) {
        rows = cols = mines = 0;

        if (!int.TryParse(RowsBox.Text, out rows) ||
            !int.TryParse(ColsBox.Text, out cols) ||
            !int.TryParse(MinesBox.Text, out mines)) {
            MessageBox.Show("Введите корректные числа");
            return false;
        }

        if (rows is < 2 or > 22 || cols is < 2 or > 30) {
            MessageBox.Show("Допустимые размеры: от 2x2 до 22x30");
            return false;
        }

        if (mines <= 0 || mines >= rows * cols) {
            MessageBox.Show("Количество мин должно быть меньше количества клеток");
            return false;
        }

        return true;
    }
}
