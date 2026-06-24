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
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly GameService _service = new GameService();
        private readonly GameModel _model = new GameModel();

        public ObservableCollection<CellViewModel> Cells { get; } = new ObservableCollection<CellViewModel>();

        public string MoneyText => $"Баланс: {_model.Money} руб.";
        public string TurnsText => $"Ход: {_model.TurnsPlayed} / {GameModel.TargetTurns}";

        public MainWindowViewModel()
        {
            Initialize();
        }

        public void Initialize()
        {
            _service.Initialize(_model);
            LoadViewModel();
            UpdateStats();
        }

        public void ProcessCellClick(CellViewModel cellVM, Action<GameResult> gameFinishedAction)
        {
            // Передаем координаты кликнутой клетки
            if (_service.MakeMove(_model, cellVM.Row, cellVM.Column))
            {
                LoadViewModel();
                UpdateStats();

                var status = _service.CheckGameStatus(_model);
                if (status != GameResult.None)
                {
                    gameFinishedAction?.Invoke(status);
                }
            }
        }

        private void UpdateStats()
        {
            OnPropertyChanged(nameof(MoneyText));
            OnPropertyChanged(nameof(TurnsText));
        }

        private void LoadViewModel()
        {
            Cells.Clear();
            for (int row = 0; row < GameModel.RowCount; row++)
            {
                for (int column = 0; column < GameModel.ColumnCount; column++)
                {
                    bool covered = _service.IsCellCovered(_model, row, column);
                    int veggieNeighbors = _service.GetVeggieNeighborsCount(_model, row, column);

                    Cells.Add(new CellViewModel
                    {
                        Row = row,
                        Column = column,
                        PeopleCount = _model.GetPeopleCount(row, column),
                        HasShop = _model.GetHasShop(row, column),
                        IsCovered = covered,
                        IsVeggie = _model.GetIsVeggie(row, column),
                        IsRevealed = _model.GetIsRevealed(row, column),
                        VeggieNeighborsCount = veggieNeighbors
                    });
                }
            }
        }
    }
}