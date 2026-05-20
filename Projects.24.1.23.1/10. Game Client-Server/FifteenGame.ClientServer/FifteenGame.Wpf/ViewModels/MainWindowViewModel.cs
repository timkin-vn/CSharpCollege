using FifteenGame.Business.Models;
using FifteenGame.Common.BusinessModels;
using FifteenGame.Common.Infrastructure;
using FifteenGame.Common.Services;
using Ninject;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;

namespace FifteenGame.Wpf.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private IGameService _service = NinjectKernel.Instance.Get<IGameService>();
        private GameModel _model = new GameModel();
        private CellViewModel _firstSelected;
        private UserModel _user;

        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<CellViewModel> Cells { get; } = new ObservableCollection<CellViewModel>();

        public string UserName => _user?.Name ?? "<нет>";
        public int MatchesCount => _model?.MatchesCount ?? 0;

        public MainWindowViewModel()
        {
            _model = new GameModel();
            _service.Initialize(_model);
            UpdateCells();
        }

        public void SetUser(UserModel user)
        {
            _user = user;
            OnPropertyChanged(nameof(UserName));
            _model = _service.GetByUserId(_user.Id);
            UpdateCells();
        }

        public void SelectCell(CellViewModel cell)
        {
            if (_firstSelected == null)
            {
                _firstSelected = cell;
                cell.IsSelected = true;
                OnPropertyChanged(nameof(Cells));
            }
            else
            {
                var success = _service.Swap(_model, _firstSelected.Row, _firstSelected.Column, cell.Row, cell.Column);
                _firstSelected.IsSelected = false;
                _firstSelected = null;

                if (success)
                {
                    var matches = _service.CheckMatches(_model);
                    if (matches.Any())
                    {
                        _service.AddMatches(_model, matches.Count);
                        _service.RemoveMatches(_model, matches);
                        _service.ProcessMatches(_model);
                        _service.Save(_model);
                        UpdateCells();
                        CheckGameFinish();
                    }
                    else
                    {
                        UpdateCells();
                    }
                }
            }
        }

        private void UpdateCells()
        {
            Cells.Clear();
            for (int row = 0; row < GameModel.RowCount; row++)
                for (int col = 0; col < GameModel.ColumnCount; col++)
                    Cells.Add(new CellViewModel { Row = row, Column = col, Num = _model[row, col] });
            OnPropertyChanged(nameof(MatchesCount));
        }

        private void CheckGameFinish()
        {
            if (_model.IsFinished)
            {
                var result = MessageBox.Show(
                    $"Поздравляем!\n\nВы собрали {MatchesCount} фишек.\n\nПовторить?",
                    "Игра окончена", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    _model.MatchesCount = 0;
                    _service.Initialize(_model);
                    UpdateCells();
                }
                else
                {
                    Application.Current.Shutdown();
                }
            }
        }

        private void OnPropertyChanged(string propertyName)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}