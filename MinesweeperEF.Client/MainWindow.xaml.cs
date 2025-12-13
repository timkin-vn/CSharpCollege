using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using MinesweeperEF.Business;
using MinesweeperEF.Client.BusinessProxy;
using MinesweeperEF.Client.BusinessProxy.Services;
using MinesweeperEF.Contracts.Games;

namespace MinesweeperEF.Client;

public partial class MainWindow {
    private ApiClient? _api;
    private UserServiceProxy? _users;
    private GameServiceProxy? _games;

    private Guid? _currentGameId;
    private GameStateDto? _currentState;

    private static readonly Brush RevealedBrush = new SolidColorBrush(Color.FromRgb(230, 230, 230));
    private static readonly JsonSerializerOptions JsonOptions = new(JsonSerializerDefaults.Web);

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

    private async void NewGame_Click(object sender, RoutedEventArgs e) {
        if (_games is null && !EnsureApiInitialized()) return;
        if (_games is null) return;

        if (!int.TryParse(RowsBox.Text, out var rows) ||
            !int.TryParse(ColsBox.Text, out var cols) ||
            !int.TryParse(MinesBox.Text, out var mines)) {
            MessageBox.Show("Введите корректные числа");
            return;
        }

        try {
            var snapshot = await _games.NewGameAsync(rows, cols, mines, NameBox.Text);
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

        BoardGrid.Children.Clear();
        BoardGrid.Rows = _currentState.Settings.Rows;
        BoardGrid.Columns = _currentState.Settings.Columns;

        for (var r = 0; r < _currentState.Settings.Rows; r++) {
            for (var c = 0; c < _currentState.Settings.Columns; c++) {
                var idx = r * _currentState.Settings.Columns + c;
                var cell = _currentState.Cells[idx];

                var btn = new Button {
                    Tag = (r, c),
                    MinWidth = 32,
                    MinHeight = 32,
                    Margin = new Thickness(2),
                    FontWeight = FontWeights.Bold
                };

                UpdateButtonVisual(btn, cell);

                btn.Click += Cell_Click;
                btn.MouseRightButtonUp += Cell_RightClick;
                BoardGrid.Children.Add(btn);
            }
        }

        UpdateStatus(snapshot);
    }

    private void UpdateStatus(GameSnapshotDto? snapshot) {
        if (snapshot is null || _currentState is null) {
            StatusText.Text = "Нет активной игры";
            FlagsText.Text = string.Empty;
            return;
        }

        var status = snapshot.GameOver
            ? (snapshot.HasWon ? "Победа" : "Поражение")
            : "Игра продолжается";

        StatusText.Text = $"Статус: {status}";
        FlagsText.Text = $"Флагов осталось: {snapshot.FlagsLeft}";
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
                btn.Content = cell.AdjacentMines > 0 ? cell.AdjacentMines.ToString() : string.Empty;
                break;
        }
    }

    private async void Cell_Click(object sender, RoutedEventArgs e) {
        if (_games is null || _currentState is null || _currentGameId is null) return;
        if (sender is not Button btn || btn.Tag is not ValueTuple<int, int> coords) return;

        var (row, col) = coords;
        var idx = row * _currentState.Settings.Columns + col;
        var cell = _currentState.Cells[idx];

        var action = cell.State == CellState.Revealed && cell.AdjacentMines > 0
            ? GameActionType.Chord
            : GameActionType.Reveal;

        await SendActionAsync(action, row, col);
    }

    private async void Cell_RightClick(object sender, MouseButtonEventArgs e) {
        if (_games is null || _currentState is null || _currentGameId is null) return;
        if (sender is not Button btn || btn.Tag is not ValueTuple<int, int> coords) return;
        e.Handled = true;

        var (row, col) = coords;
        await SendActionAsync(GameActionType.ToggleFlag, row, col);
    }

    private async Task SendActionAsync(GameActionType type, int row, int col) {
        if (_games is null || _currentGameId is null) return;

        try {
            var snapshot = await _games.ActionAsync(_currentGameId.Value, type, row, col);
            RenderSnapshot(snapshot);
        }
        catch (Exception ex) {
            MessageBox.Show($"Ошибка хода: {ex.Message}");
        }
    }
}
