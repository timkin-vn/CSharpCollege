using Game2048.Business.Models;
using Game2048.Business.Services;
using Game2048.Wpf.ViewModels;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Game2048.Wpf.ViewModels
{
    public class MainWindowViewModel
    {
        private readonly GameService _service = new GameService();
        private readonly GameModel _model = new GameModel();

        public ObservableCollection<CellViewModel> Cells { get; set; }

        public MainWindowViewModel()
        {
            Cells = new ObservableCollection<CellViewModel>();
            InitializeGame();
        }

        private void InitializeGame()
        {
            _service.Initialize(_model);
            UpdateCells();
        }

        private void UpdateCells()
        {
            Cells.Clear();
            for (int row = 0; row < GameModel.RowCount; row++)
            {
                for (int column = 0; column < GameModel.ColumnCount; column++)
                {
                    Cells.Add(new CellViewModel
                    {
                        Value = _model[row, column]
                    });
                }
            }
        }

        public void MakeMove(MoveDirection direction)
        {
            bool moved = _service.MakeMove(_model, direction);
            if (moved)
            {
                UpdateCells();
            }
        }
    }
}
