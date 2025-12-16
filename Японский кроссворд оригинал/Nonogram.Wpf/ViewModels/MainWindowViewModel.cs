using Nonogram.Business.Models;
using Nonogram.Business.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nonogram.Wpf.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private NonogramService _service = new NonogramService();
        private NonogramModel _model = new NonogramModel();

        public ObservableCollection<CellViewModel> Cells { get; set; } = new ObservableCollection<CellViewModel>();
        public ObservableCollection<RowClueViewModel> RowClues { get; set; } = new ObservableCollection<RowClueViewModel>();
        public ObservableCollection<ColumnClueViewModel> ColumnClues { get; set; } = new ObservableCollection<ColumnClueViewModel>();

        private int _mistakesCount;
        public int MistakesCount
        {
            get => _mistakesCount;
            private set
            {
                _mistakesCount = value;
                OnPropertyChanged(nameof(MistakesCount));
            }
        }

        public event EventHandler GameOver;
        public event EventHandler GameWon;

        public MainWindowViewModel()
        {
            Initialize();
        }

        public void Initialize()
        {
            _service.Initialize(_model);
            FromModel();
        }

        public void MakeMove(int row, int column)
        {
            var result = _service.MakeMove(_model, row, column, true);

            var cell = Cells.FirstOrDefault(c => c.Row == row && c.Column == column);
            if (cell != null)
            {
                switch (result)
                {
                    case MoveResult.Correct:
                        cell.State = CellState.Filled;
                        bool rowComplete = false;
                        bool columnComplete = false;

                        if (_service.CheckRowComplete(_model, row))
                        {
                            rowComplete = true;
                            MarkRowAsComplete(row);
                        }

                        if (_service.CheckColumnComplete(_model, column))
                        {
                            columnComplete = true;
                            MarkColumnAsComplete(column);
                        }

                        if (rowComplete || columnComplete)
                        {
                            UpdateClueColors();
                        }
                        break;

                    case MoveResult.Wrong:
                        cell.State = CellState.Crossed;
                        break;

                    case MoveResult.AlreadyFilled:
                        break;
                }
            }

            MistakesCount = _model.MistakesCount;

            if (_service.IsGameOver(_model))
            {
                GameOver?.Invoke(this, EventArgs.Empty);
            }
            else if (_service.IsGameWon(_model))
            {
                GameWon?.Invoke(this, EventArgs.Empty);
            }
        }

        private void MarkRowAsComplete(int row)
        {
            foreach (var cell in Cells.Where(c => c.Row == row && c.State == CellState.Filled))
            {
                cell.IsCompleted = true;
            }

            var rowClue = RowClues.FirstOrDefault(r => r.RowIndex == row);
            if (rowClue != null)
            {
                rowClue.IsCompleted = true;
            }
        }

        private void MarkColumnAsComplete(int column)
        {
            foreach (var cell in Cells.Where(c => c.Column == column && c.State == CellState.Filled))
            {
                cell.IsCompleted = true;
            }

            var columnClue = ColumnClues.FirstOrDefault(c => c.ColumnIndex == column);
            if (columnClue != null)
            {
                columnClue.IsCompleted = true;
            }
        }

        private void UpdateClueColors()
        {
            foreach (var rowClue in RowClues)
            {
                rowClue.IsCompleted = _service.CheckRowComplete(_model, rowClue.RowIndex);
            }

            foreach (var columnClue in ColumnClues)
            {
                columnClue.IsCompleted = _service.CheckColumnComplete(_model, columnClue.ColumnIndex);
            }
        }

        private void FromModel()
        {
            Cells.Clear();
            RowClues.Clear();
            ColumnClues.Clear();

            for (int row = 0; row < NonogramModel.Size; row++)
            {
                for (int col = 0; col < NonogramModel.Size; col++)
                {
                    Cells.Add(new CellViewModel
                    {
                        Row = row,
                        Column = col,
                        State = (CellState)_model[row, col]
                    });
                }
            }

            for (int row = 0; row < _model.RowClues.Count; row++)
            {
                RowClues.Add(new RowClueViewModel
                {
                    RowIndex = row,
                    Clues = _model.RowClues[row],
                    IsCompleted = false
                });
            }

            for (int col = 0; col < _model.ColumnClues.Count; col++)
            {
                ColumnClues.Add(new ColumnClueViewModel
                {
                    ColumnIndex = col,
                    Clues = _model.ColumnClues[col],
                    IsCompleted = false
                });
            }

            MistakesCount = _model.MistakesCount;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}