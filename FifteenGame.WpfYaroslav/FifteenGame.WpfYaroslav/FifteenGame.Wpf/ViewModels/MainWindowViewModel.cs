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
    // создание тега где будет находитс две знааени col и row
    // сделать скрытие сли они совпали 
    //
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
            if (countbuttonclick == 0)
            {
                _model.FisrsbuutonRowCol = colrow;
                _model.Fistbuuton = _model[colrow[0], colrow[1]];
                countbuttonclick++;
                
            }
            else if (countbuttonclick == 1)
            {
                _model.Secondbuuton = _model[colrow[0], colrow[1]];
                _model.SecondbuutonRowCol = colrow;
                if (_service.CheckMatch(_model))
                {
                    FromModel(_model);
                }
                

                countbuttonclick = 0;
               
            }


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
                    
                   

                    Cells.Add(item: new CellViewModel
                    {
                        Row = row,
                        ColumnRow = new int[] { row, column },
                        Column = column,
                        ColorText = model[row, column],
                        ColorButton = model[row, column],
                        IsFaceUp  = true,
                        
                    });
                }
            }
        }
    }
   
}
