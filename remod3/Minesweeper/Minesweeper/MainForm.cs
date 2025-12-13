using Minesweeper.Business;
using System.Diagnostics.CodeAnalysis;

namespace Minesweeper;

public sealed partial class MainForm : Form {
    private readonly Panel _gridHost = new() { Dock = DockStyle.Fill, BackColor = Color.FromArgb(245, 246, 248) };
    private readonly Panel _topBar = new() { Dock = DockStyle.Top, Height = 48, BackColor = Color.White };
    private readonly Label _lblMines = new() { AutoSize = true, Font = new Font("Segoe UI", 11, FontStyle.Bold) };
    private readonly Label _lblTime = new() { AutoSize = true, Font = new Font("Segoe UI", 11, FontStyle.Bold) };
    private readonly Button _btnNew = new() { Text = "Новая игра", AutoSize = true };
    private readonly ComboBox _cbDifficulty = new() { DropDownStyle = ComboBoxStyle.DropDownList, Width = 160 };

    private readonly System.Windows.Forms.Timer _timer = new() { Interval = 1000 };

    private Button[,] _buttons = null!;
    private GameBoard _game = null!;
    private GameSettings _settings = GameSettings.Intermediate();
    private int _seconds;

    private const int _Cell = 28;
    private readonly Color[] _numberColors = [
        Color.Transparent, Color.Blue, Color.Green, Color.Red, Color.Navy, Color.Maroon, Color.Teal, Color.Black, Color.Gray
    ];

    public MainForm() {
        Text = "Сапёр (C# WinForms)";
        StartPosition = FormStartPosition.CenterScreen;
        MinimumSize = new Size(420, 360);
        Font = new Font("Segoe UI", 10);

        _topBar.Padding = new Padding(12, 8, 12, 8);
        var bar = new FlowLayoutPanel { Dock = DockStyle.Fill, AutoSize = false, FlowDirection = FlowDirection.LeftToRight, WrapContents = false };
        _cbDifficulty.Items.AddRange("Новичок (9×9, 10 мин)", "Любитель (16×16, 40 мин)", "Профессионал (30×16, 99 мин)", "Пользовательский…");
        _cbDifficulty.SelectedIndex = 1;

        bar.Controls.Add(new Label{ Text="Сложность:", AutoSize = true, Margin = new Padding(0,8,8,0) });
        bar.Controls.Add(_cbDifficulty);
        bar.Controls.Add(new Label{ Text="  ", AutoSize = true, Width=18 });
        bar.Controls.Add(_btnNew);
        bar.Controls.Add(new Label{ Text="  |  ", AutoSize = true, Margin = new Padding(8,8,8,0) });

        _lblMines.Text = "Мины: 0";
        _lblTime.Text  = "Время: 0";

        bar.Controls.Add(_lblMines);
        bar.Controls.Add(new Label{ Text="  ", AutoSize = true, Width=18 });
        bar.Controls.Add(_lblTime);

        _topBar.Controls.Add(bar);

        Controls.Add(_gridHost);
        Controls.Add(_topBar);

        _btnNew.Click += (_, _) => NewGameFromUi();
        _cbDifficulty.SelectedIndexChanged += (_, _) => NewGameFromUi();
        _timer.Tick += (_, _) => { _seconds++; UpdateStatus(); };

        ApplyDifficulty(_cbDifficulty.SelectedIndex);
        InitializeGame();
    }

    public sealed override Size MinimumSize {
        get => base.MinimumSize;
        set => base.MinimumSize = value;
    }

    [AllowNull] public override string Text {
        get => base.Text;
        set => base.Text = value;
    }

    private void NewGameFromUi() {
        if (_cbDifficulty.SelectedIndex == 3) {
            using var dlg = new CustomDialog(_settings.Rows, _settings.Columns, _settings.Mines);
            if (dlg.ShowDialog(this) == DialogResult.OK) {
                _settings = new GameSettings(dlg.Rows, dlg.Cols, Math.Min(dlg.Mines, dlg.Rows * dlg.Cols - 1));
                BuildGrid();
                StartNewGame();
                return;
            }
            _cbDifficulty.SelectedIndex = DifficultyIndexFromCurrent();
        }
        else {
            ApplyDifficulty(_cbDifficulty.SelectedIndex);
            BuildGrid();
            StartNewGame();
        }
    }

    private int DifficultyIndexFromCurrent() {
        return _settings switch {
            { Rows: 9, Columns: 9, Mines: 10 } => 0,
            { Rows: 16, Columns: 16, Mines: 40 } => 1,
            { Rows: 16, Columns: 30, Mines: 99 } => 2,
            _ => 3
        };
    }

    private void ApplyDifficulty(int index) {
        _settings = index switch {
            0 => GameSettings.Beginner(),
            1 => GameSettings.Intermediate(),
            2 => GameSettings.Expert(),
            _ => _settings
        };
    }

    private void InitializeGame() {
        _game = new GameBoard();
        _game.ApplySettings(_settings);
        BuildGrid();
        StartNewGame();
    }

    private void BuildGrid() {
        _gridHost.Controls.Clear();
        _gridHost.AutoScroll = true;

        var gridW = _settings.Columns * _Cell + 2;
        var gridH = _settings.Rows * _Cell + 2;
        _gridHost.MinimumSize = new Size(gridW + 12, gridH + 12);

        var grid = new Panel {
            Size = new Size(gridW, gridH),
            BackColor = Color.Gainsboro,
            Location = new Point(12, 12),
            BorderStyle = BorderStyle.FixedSingle
        };
        _gridHost.Controls.Add(grid);

        _buttons = new Button[_settings.Rows, _settings.Columns];
        for (var r = 0; r < _settings.Rows; r++) {
            for (var c = 0; c < _settings.Columns; c++) {
                var b = new Button {
                    Size = new Size(_Cell, _Cell),
                    Location = new Point(c * _Cell + 1, r * _Cell + 1),
                    BackColor = Color.WhiteSmoke,
                    Font = new Font("Segoe UI", 9, FontStyle.Bold),
                    Tag = (r, c),
                    TabStop = false
                };
                b.MouseUp += Cell_MouseUp;
                _buttons[r, c] = b;
                grid.Controls.Add(b);
            }
        }
    }

    private void StartNewGame()
    {
        _timer.Stop();
        _seconds = 0;
        _game.ApplySettings(_settings);
        _game.NewGame();
        ResetButtons();
        UpdateStatus();
    }

    private void ResetButtons() {
        for (var r = 0; r < _settings.Rows; r++)
        for (var c = 0; c < _settings.Columns; c++) {
            var b = _buttons[r, c];
            b.Enabled = true;
            b.Text = string.Empty;
            b.BackColor = Color.WhiteSmoke;
            b.ForeColor = Color.Black;
        }
    }

    private void UpdateStatus() {
        _lblMines.Text = $"Мины: {_game.FlagsLeft}";
        _lblTime.Text = $"Время: {_seconds}";
    }

    private void Cell_MouseUp(object? sender, MouseEventArgs e) {
        if (_game.GameOver || sender is not Button b) return;
        var (r, c) = ((int r, int c))b.Tag!;

        switch (e.Button) {
            case MouseButtons.Right:
                ApplyResult(_game.ToggleFlag(r, c));
                return;
            case MouseButtons.Left: {
                var cell = _game[r, c];
                var result = cell.State == CellState.Revealed
                    ? _game.Chord(r, c)
                    : _game.Reveal(r, c);
                ApplyResult(result);
                break;
            }
        }
    }
    
    private void ApplyResult(GameActionResult result) {
        if (result.Updates.Count == 0)
            return;

        if (_game.HasStarted && !_timer.Enabled)
            _timer.Start();

        foreach (var update in result.Updates)
            ApplyUpdate(update);

        if (result.GameOver) {
            _timer.Stop();
            if (result.HitMine)
                foreach (var mine in _game.RevealAllMines())
                    ApplyUpdate(mine);

            var msg = result.HasWon ? $"Победа! Время: {_seconds} c" : "Вы подорвались на мине :(";
            var res = MessageBox.Show(this, msg + "\nСыграть ещё?", "Итог", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (res == DialogResult.Yes) {
                StartNewGame();
                return;
            }
        }
        UpdateStatus();
    }

    private void ApplyUpdate(CellUpdate update) {
        var button = _buttons[update.Row, update.Column];
        switch (update.State) {
            case CellState.Hidden:
                button.Enabled = true;
                button.Text = string.Empty;
                button.BackColor = Color.WhiteSmoke;
                button.ForeColor = Color.Black;
                break;
            case CellState.Flagged:
                button.Text = "⚑";
                button.ForeColor = Color.IndianRed;
                button.Enabled = true;
                break;
            case CellState.Questioned:
                button.Text = "?";
                button.ForeColor = Color.DarkOrange;
                button.Enabled = true;
                break;
            case CellState.Revealed:
                button.Enabled = false;
                button.BackColor = Color.White;
                button.Text = update.AdjacentMines > 0 ? update.AdjacentMines.ToString() : string.Empty;
                button.ForeColor = update.AdjacentMines > 0 ? _numberColors[Math.Min(update.AdjacentMines, _numberColors.Length - 1)] : Color.Black;
                break;
            case CellState.Mine:
                button.Text = "💣";
                button.BackColor = Color.MistyRose;
                button.Enabled = false;
                break;
            case CellState.Exploded:
                button.BackColor = Color.IndianRed;
                button.Enabled = false;
                break;
            default:
                var exception = new ArgumentOutOfRangeException {
                    HelpLink = null,
                    HResult = 0,
                    Source = null
                };
                throw exception;
        }
    }

    private sealed class CustomDialog : Form {
        private readonly NumericUpDown _numR = new() { Minimum=5, Maximum=60, Value=16, Width=80 };
        private readonly NumericUpDown _numC = new() { Minimum=5, Maximum=60, Value=16, Width=80 };
        private readonly NumericUpDown _numM = new() { Minimum=1, Maximum=999, Value=40, Width=80 };
        public int Rows => (int)_numR.Value;
        public int Cols => (int)_numC.Value;
        public int Mines => (int)_numM.Value;

        public CustomDialog(int r, int c, int m) {
            Text = "Пользовательская сложность";
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = MinimizeBox = false;
            ClientSize = new Size(320, 160);

            _numR.Value = r; _numC.Value = c; _numM.Value = m;

            var layout = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 2, RowCount = 4, Padding = new Padding(12) };
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60));
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40));

            layout.Controls.Add(new Label{ Text="Строки:", AutoSize = true, Anchor = AnchorStyles.Left }, 0, 0);
            layout.Controls.Add(_numR, 1, 0);
            layout.Controls.Add(new Label{ Text="Столбцы:", AutoSize = true, Anchor = AnchorStyles.Left }, 0, 1);
            layout.Controls.Add(_numC, 1, 1);
            layout.Controls.Add(new Label{ Text="Мины:", AutoSize = true, Anchor = AnchorStyles.Left }, 0, 2);
            layout.Controls.Add(_numM, 1, 2);

            var flow = new FlowLayoutPanel{ FlowDirection = FlowDirection.RightToLeft, Dock = DockStyle.Fill };
            var ok = new Button{ Text="OK", DialogResult = DialogResult.OK };
            var cancel = new Button{ Text="Отмена", DialogResult = DialogResult.Cancel };
            flow.Controls.Add(ok); flow.Controls.Add(cancel);
            layout.Controls.Add(flow, 0, 3);
            layout.SetColumnSpan(flow, 2);

            Controls.Add(layout);
        }
    }
}