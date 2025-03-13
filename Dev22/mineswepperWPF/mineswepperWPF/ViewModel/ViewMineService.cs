using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MIneSweepper.Bisiness.Services;
using MIneSweepper.Bisiness.Model;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace mineswepperWPF.ViewModel
{
    public class ViewMineService : INotifyPropertyChanged
    {
        private MineSevicer _service = new MineSevicer();
        private GameModel _model = new GameModel();

        public ObservableCollection<CellWiewModel> Cells { get; set; } = new ObservableCollection<CellWiewModel>();
        //флаги
        private int _countFlags;
        public int CountFlags
        {
            get => _countFlags;
            set
            {
                _countFlags = value;
                OnPropertyChanged(nameof(CountFlags));
            }
        }
        //j
        public int Column => GameModel.ColumnCount; 
        public int Row => GameModel.RowCount; 

        public ViewMineService()
        {
            Initialize();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void Initialize()
        {
            _service.Iniziallize(_model, 10);
            FromModel(_model);
        }


        public void setFlagCell(int row, int column)
        {
            _service.issFlaged(_model, row, column);
            FromModel(_model);
        }
        public void MakeMove(int row, int column, Action gameFinishedAction)
        {

            _service.RevealCell(row, column, _model);
            FromModel(_model);
            if (_service.IsGameOver(_model))
            {

                gameFinishedAction();
            }

        }

        private void FromModel(GameModel model)
        {
            Cells.Clear();
             CountFlags = model.RedFlags;
            for (int row = 0; row < GameModel.RowCount; row++)
            {
                for (int column = 0; column < GameModel.ColumnCount; column++)
                {
                    var cellViewModel = new CellWiewModel
                    {
                        Row = row,
                        Column = column,
                        Num = model[row, column].NeightborMines,
                        IsRevealed = model[row,column].IsRevealed,
                        IsFlagged = model[row, column].Isflag,
                        IsmIne = model[row, column].IsMine,

                    };

                    Cells.Add(cellViewModel);
                }
            }
        }
    }
}
