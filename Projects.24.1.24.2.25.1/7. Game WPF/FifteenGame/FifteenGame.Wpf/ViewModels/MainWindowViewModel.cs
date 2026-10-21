
using FifteenGame.Business.Models;
using FifteenGame.Business.Services;
using System;
using System.Collections.ObjectModel;
using System.IO;

namespace FifteenGame.Wpf.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly GameService _service = new GameService();
        private readonly GameModel _model = new GameModel();

        private readonly string _bestScorePath = Path.Combine(
            Environment.GetFolderPath(
                Environment.SpecialFolder.LocalApplicationData),
            "CSharpCollege",
            "2048BestScore.txt");

        private int[,] _undoBoard =
            new int[GameModel.RowCount, GameModel.ColumnCount];

        private int _undoScore;
        private bool _undoHasWon;
        private string _undoStatusMessage = string.Empty;

        private int _score;
        private int _bestScore;
        private bool _isGameOver;
        private bool _hasWon;
        private bool _canUndo;
        private string _statusMessage = string.Empty;

        public ObservableCollection<CellViewModel> Cells { get; } =
            new ObservableCollection<CellViewModel>();

        public int Score
        {
            get => _score;
            private set
            {
                _score = value;
                OnPropertyChanged(nameof(Score));
            }
        }

        public int BestScore
        {
            get => _bestScore;
            private set
            {
                _bestScore = value;
                OnPropertyChanged(nameof(BestScore));
            }
        }

        public bool IsGameOver
        {
            get => _isGameOver;
            private set
            {
                _isGameOver = value;
                OnPropertyChanged(nameof(IsGameOver));
            }
        }

        public bool HasWon
        {
            get => _hasWon;
            private set
            {
                _hasWon = value;
                OnPropertyChanged(nameof(HasWon));
            }
        }

        public bool CanUndo
        {
            get => _canUndo;
            private set
            {
                _canUndo = value;
                OnPropertyChanged(nameof(CanUndo));
            }
        }

        public string StatusMessage
        {
            get => _statusMessage;
            private set
            {
                _statusMessage = value;
                OnPropertyChanged(nameof(StatusMessage));
            }
        }

        public MainWindowViewModel()
        {
            LoadBestScore();
            Initialize();
        }

        public void Initialize()
        {
            _service.Initialize(_model);

            Score = 0;
            IsGameOver = false;
            HasWon = false;
            CanUndo = false;

            StatusMessage = "Объединяй плитки и набери 2048!";

            LoadViewModel();
        }

        public void MakeMove(MoveDirection direction)
        {
            if (IsGameOver || direction == MoveDirection.None)
                return;

            int[,] previousBoard =
                new int[GameModel.RowCount, GameModel.ColumnCount];

            for (int row = 0; row < GameModel.RowCount; row++)
            {
                for (int column = 0;
                     column < GameModel.ColumnCount;
                     column++)
                {
                    previousBoard[row, column] =
                        _model[row, column];
                }
            }

            int previousScore = _model.Score;
            bool previousHasWon = HasWon;
            string previousStatusMessage = StatusMessage;

            bool changed = _service.MakeMove(_model, direction);

            if (!changed)
                return;

            _undoBoard = previousBoard;
            _undoScore = previousScore;
            _undoHasWon = previousHasWon;
            _undoStatusMessage = previousStatusMessage;
            CanUndo = true;

            Score = _model.Score;

            if (Score > BestScore)
            {
                BestScore = Score;
                SaveBestScore();
            }

            if (!HasWon && _model.HasWon)
            {
                HasWon = true;
                StatusMessage =
                    "Ты собрал 2048! Можно продолжить игру.";
            }

            IsGameOver = _service.IsGameOver(_model);

            if (IsGameOver)
            {
                StatusMessage =
                    "Ходов больше нет. Начни новую игру!";
            }
            else if (_model.Score > previousScore && !HasWon)
            {
                StatusMessage =
                    "Отлично! Продолжай объединять плитки.";
            }

            LoadViewModel();
        }

        public void Undo()
        {
            if (!CanUndo)
                return;

            for (int row = 0; row < GameModel.RowCount; row++)
            {
                for (int column = 0;
                     column < GameModel.ColumnCount;
                     column++)
                {
                    _model.RestoreState(_undoBoard, _undoScore);
                }
            }

            _model.RestoreState(_undoBoard, _undoScore);

            Score = _undoScore;
            HasWon = _undoHasWon;
            IsGameOver = _service.IsGameOver(_model);
            StatusMessage = _undoStatusMessage;

            CanUndo = false;

            LoadViewModel();
        }

        private void LoadViewModel()
        {
            Cells.Clear();

            for (int row = 0; row < GameModel.RowCount; row++)
            {
                for (int column = 0;
                     column < GameModel.ColumnCount;
                     column++)
                {
                    Cells.Add(new CellViewModel
                    {
                        Row = row,
                        Column = column,
                        Value = _model[row, column],
                        Direction = MoveDirection.None
                    });
                }
            }
        }

        private void LoadBestScore()
        {
            try
            {
                if (File.Exists(_bestScorePath) &&
                    int.TryParse(
                        File.ReadAllText(_bestScorePath),
                        out int value))
                {
                    BestScore = Math.Max(0, value);
                }
            }
            catch (IOException)
            {
                BestScore = 0;
            }
            catch (UnauthorizedAccessException)
            {
                BestScore = 0;
            }
        }

        private void SaveBestScore()
        {
            try
            {
                string directory =
                    Path.GetDirectoryName(_bestScorePath);

                if (!Directory.Exists(directory))
                    Directory.CreateDirectory(directory);

                File.WriteAllText(
                    _bestScorePath,
                    BestScore.ToString());
            }
            catch (IOException)
            {
            }
            catch (UnauthorizedAccessException)
            {
            }
        }
    }
}
