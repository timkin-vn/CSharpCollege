using FifteenGame.Business.Models;
using FifteenGame.Business.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace FifteenGame.Wpf.ViewModels
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

        public void MakeMove(MoveDirection direction, Action gameFinishedAction)
        {
            _service.MakeMove(_model, direction);
            FromModel(_model);
            if (_service.IsGameOver(_model))
            {
                gameFinishedAction();
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
                    if (row != model.PlayerRow || column != model.PlayerColumn)
                    {
                        var direction = MoveDirection.None;
                        if (row == model.PlayerRow)
                        {
                            if (column == model.PlayerColumn - 1)
                            {
                                direction = MoveDirection.Right;
                            }
                            else if (column == model.PlayerColumn + 1)
                            {
                                direction = MoveDirection.Left;
                            }
                        }
                        else if (column == model.PlayerColumn)
                        {
                            if (row == model.PlayerRow - 1)
                            {
                                direction = MoveDirection.Down;
                            }
                            else if (row == model.PlayerRow + 1)
                            {
                                direction = MoveDirection.Up;
                            }
                        }
                        var color = new SolidColorBrush(Colors.White);
                        if (row == 1 && column == GameModel.ColumnCount - 1)
                        {
                            color = new SolidColorBrush(Colors.Yellow);
                        }
                        else if (model[row, column] > 0)
                        {
                            color = new SolidColorBrush(Color.FromRgb(12, 171, 171));
                        }
                        else if (model[row, column] < 0)
                        {
                            color = new SolidColorBrush(Color.FromRgb(171, 57, 57));
                        }
                        else
                        {
                            color = new SolidColorBrush(Colors.Gray);
                        }
                        Cells.Add(new CellViewModel
                        {
                            Row = row,
                            Column = column,
                            Num = model[row, column],
                            Direction = direction,
                            FillColor = color
                        }) ;
                    }
                    else
                    {
                        Cells.Add(new CellViewModel
                        {
                            Row = row,
                            Column = column,
                            Num = model[row, column],
                            FillColor = new SolidColorBrush(Color.FromRgb(10, 148, 36))
                    });
                    }
                }
            }
        }
    }
}
