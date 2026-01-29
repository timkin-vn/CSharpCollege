using Игра.Common.BusinessModels;
using Игра.Common.Infrastructure;
using Игра.Common.Services;
using System.Collections.ObjectModel;
using Ninject;

namespace Игра.Wpf.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly IGameService _service = NinjectKernel.Instance.Get<IGameService>();
        private GameModel _model;
        private UserModel _user;

        public ObservableCollection<CellViewModel> Cells { get; } = new ObservableCollection<CellViewModel>();

        private bool _isGameWon;
        public bool IsGameWon
        {
            get => _isGameWon;
            set => Set(ref _isGameWon, value);
        }

        public string UserName => _user?.Name ?? "<нет>";

        public MainWindowViewModel()
        {
            InitializeCells();
        }

        private void InitializeCells()
        {
            Cells.Clear();
            for (int r = 0; r < GameModel.RowCount; r++)
            {
                for (int c = 0; c < GameModel.ColumnCount; c++)
                {
                    Cells.Add(new CellViewModel(r, c));
                }
            }
        }

        public void SetUser(UserModel user)
        {
            _user = user;
            if (user == null)
            {
                return;
            }
            _model = _service.GetByUserId(user.Id);
            IsGameWon = _service.IsGameOver(_model.Id);
            if (IsGameWon)
            {
                _service.RemoveGame(_model.Id);
            }
            UpdateAllCellColors();
            OnPropertyChanged(nameof(UserName));
        }

        public void OnClick(int row, int col)
        {
            if (_model == null || IsGameWon)
            {
                return;
            }
            _model = _service.MakeMove(_model.Id, row, col);
            IsGameWon = _service.IsGameOver(_model.Id);
            if (IsGameWon)
            {
                _service.RemoveGame(_model.Id);
            }
            UpdateAllCellColors();
        }

        public void ReInitialize()
        {
            if (_user == null)
            {
                return;
            }
            _model = _service.GetByUserId(_user.Id);
            IsGameWon = _service.IsGameOver(_model.Id);
            if (IsGameWon)
            {
                _service.RemoveGame(_model.Id);
            }
            UpdateAllCellColors();
        }

        private void UpdateAllCellColors()
        {
            bool win = IsGameWon;
            if (_model == null)
            {
                foreach (var cellVm in Cells)
                {
                    cellVm.UpdateColor(false, false);
                }
                return;
            }
            foreach (var cellVm in Cells)
            {
                bool isActive = _model[cellVm.Row, cellVm.Column];
                cellVm.UpdateColor(isActive, win);
            }
        }
    }
}