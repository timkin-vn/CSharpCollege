using FifteenGame.Business.Models;
using FifteenGame.Business.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace FifteenGame.Wpf.ViewModels
{
    public class MainWindowViewModel
    {
        private readonly GameService _service = new GameService();
        private readonly GameModel _model = new GameModel();

        public ObservableCollection<CellViewModel> Cells { get; } = new ObservableCollection<CellViewModel>();

        public int Score { get; private set; }

        public MainWindowViewModel()
        {
            Initialize();
        }

        public void Initialize()
        {
            _service.Initialize(_model);
            FromModel(_model);
        }

        public void MakeMove(MoveDirection direction, Action gameFinishAction)
        {
            _service.MakeMove(_model, direction);
            FromModel(_model);

            if (_service.IsGameOver(_model))
            {
                gameFinishAction?.Invoke();
            }
        }

        private void FromModel(GameModel model)
        {
            Cells.Clear();

            for (int row = 0; row < GameModel.RowCount; row++)
            {
                for (int col = 0; col < GameModel.ColumnCount; col++)
                {
                    Cells.Add(new CellViewModel
                    {
                        Row = row,
                        Column = col,
                        Value = model[row, col]
                    });
                }
            }

            Score = model.Score;
        }

        public bool IsWin => _model.IsWin;
        public bool IsLose => _model.IsLose;
    }
}