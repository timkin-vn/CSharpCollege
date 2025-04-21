using FifteenGame.Business.Models;
using FifteenGame.Business.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace FifteenGame.Wpf.ViewModels
{
    using FifteenGame.Business;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Windows;
    public class MainWindowViewModel : INotifyPropertyChanged
    {

        private GameService _service = new GameService();
        private GameModel _model = new GameModel();

        public int countbuttonclick = 0;
        
        

        
        public ObservableCollection<CellViewModel> Cells { get; set; } = new ObservableCollection<CellViewModel>();

        public MainWindowViewModel()
        {
            Initialize();


        }

        public void MakeMove(int[] colrow, Action gameFinishedAction)
        {

            _service.CheckMatch(_model,colrow);


            FromModel(_model);
            if (_service.IsGameOver(_model))
            {
                gameFinishedAction?.Invoke();
            }

        }
                

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void Initialize()
        {
            _service.Initialize(_model);
            FromModel(_model);
        }

        private void FromModel(GameModel model)
        {
            Cells.Clear();
            for (int row = 0; row < GameModel.RowCount; row++)
            {
                for (int column = 0; column < GameModel.ColumnCount; column++)
                {

                    if ((row == model.OneColumnRow[0]&& column == model.OneColumnRow[1])
                        || (row == model.TwoColumnRow[0] && column == model.TwoColumnRow[1]))
                    {
                        Cells.Add(item: new CellViewModel
                        {
                            Row = row,
                            ColumnRow = new int[] { row, column },
                            Column = column,
                            NameOre = model[row, column],

                            IsMine = true,

                        });
                    }
                    else if (string.IsNullOrEmpty(model[row, column]))
                    {
                        continue;
                    }

                    else {
                        Cells.Add(item: new CellViewModel
                        {
                            Row = row,
                            ColumnRow = new int[] { row, column },
                            Column = column,
                            NameOre = model[row, column],

                            IsMine = false,

                        });
                    }
                }
            }
        }
    }
   
}
