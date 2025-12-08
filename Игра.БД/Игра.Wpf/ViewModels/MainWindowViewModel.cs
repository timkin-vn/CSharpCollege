using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using Игра.Common.BusinessModels;
using Игра.Common.Definitions;
using Игра.Common.Infrastructure;
using Игра.Common.Repositories;
using Игра.Common.Services;
using Игра.Wpf.Infrastructure;

namespace Игра.Wpf.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly IGameService _service = NinjectKernel.Instance.Get<IGameService>();

        private GameModel _model;

        public List<CellViewModel> Cells { get; set; }

        private bool _isGameWon;

        public bool IsGameWon
        {
            get => _isGameWon;
            set => Set(ref _isGameWon, value);
        }

        public MainWindowViewModel()
        {
            NinjectSetup.Init();

            var userRepository = NinjectKernel.Instance.Get<IUserRepository>();

            var testUser = userRepository.GetAll().FirstOrDefault();
            if (testUser == null)
            {
                testUser = userRepository.Create("TestUser");
            }

            _service = NinjectKernel.Instance.Get<IGameService>();

            _model = _service.GetByUserId(testUser.Id);

            Cells = new List<CellViewModel>();
            InitializeCells();
            UpdateAllCellColors();
        }

        private void InitializeCells()
        {
            Cells.Clear();
            for (int r = 0; r < Constants.Size; r++)
            {
                for (int c = 0; c < Constants.Size; c++)
                {
                    Cells.Add(new CellViewModel(r, c));
                }
            }
        }

        public void SetUser(UserModel user)
        {
            if (user == null)
            {
                return;
            }

            _model = _service.GetByUserId(user.Id);
            UpdateAllCellColors();
        }

        public void OnClick(int row, int col)
        {
            if (_model == null)
            {
                return;
            }

            if (IsGameWon)
            {
                return;
            }

            _model = _service.MakeMove(_model.Id, row, col);
            IsGameWon = _service.IsGameOver(_model.Id);
            UpdateAllCellColors();
        }

        public void ReInitialize(UserModel user)
        {
            if (user == null)
            {
                return;
            }

            _model = _service.GetByUserId(user.Id);
            IsGameWon = _service.IsGameOver(_model.Id);
            UpdateAllCellColors();
        }

        private void UpdateAllCellColors()
        {
            bool isWin = IsGameWon;

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
                bool isActive = _model.Cells[cellVm.Row, cellVm.Column];
                cellVm.UpdateColor(isActive, isWin);
            }
        }
    }
}