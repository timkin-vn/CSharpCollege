using Game2048.Business.Models;
using Game2048.Business.Services;
using Game2048.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game2048.Wpf.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private GameService _service = new GameService();
        private GameModel _model = new GameModel();

        public ObservableCollection<CellViewModel> Cells { get; set; } = new ObservableCollection<CellViewModel>();

        public MainWindowViewModel()
        {
            Initialize();
        }

        public void Initialize()
        {
            _service.Initialize(_model);
            FromModel(_model);
        }

        public void MakeMove(MoveDirection direction, Action gameFinishedAction)
        {
            if (_service.MakeMove(_model, direction))
            {
                FromModel(_model);
                if (_service.IsGameOver(_model))
                {
                    gameFinishedAction();
                }
            }
        }

        private void FromModel(GameModel model)
        {
            Cells.Clear();
            for (int row = 0; row < GameModel.RowCount; row++)
            {
                for (int column = 0; column < GameModel.ColumnCount; column++)
                {
                    Cells.Add(new CellViewModel
                    {
                        Row = row,
                        Column = column,
                        Num = model[row, column]
                    });
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}