using FifteenGame.Common.BusinessModels;
using FifteenGame.Common.Definitions;
using FifteenGame.Common.Infrastructure;
using FifteenGame.Common.Services;
using Ninject;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace FifteenGame.Wpf.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private IGameService _service;
        private GameModel _model = new GameModel();
        private UserModel _userModel;
        private int countbuttonclick = 0;
        private int[] firstSelectedCell = { -1, -1 }; // Координаты первой выбранной ячейки
        private CellViewModel firstSelectedCellVM = null; // Первая выбранная ячейка (ViewModel)

        public ObservableCollection<CellViewModel> Cells { get; set; } = new ObservableCollection<CellViewModel>();

        public string UserName => _userModel?.Name ?? "<нет>";
        public UserModel CurrentUser => _userModel;
        public string MoveCountText => (_model?.MoveCount ?? 0).ToString();
        
        public MainWindowViewModel()
        {
            _service = NinjectKernel.Instance.Get<IGameService>();
        }

        public void MakeMove(int[] colrow, Action gameFinishedAction)
        {
            // Находим выбранную ячейку
            var selectedCell = Cells.FirstOrDefault(c => c.Row == colrow[0] && c.Column == colrow[1]);
            
            // Если ячейка уже сопоставлена или открыта, игнорируем клик
            if (selectedCell == null || selectedCell.IsMatched)
                return;
                
            if (countbuttonclick == 0)
            {
                // Первое нажатие - открываем первую ячейку
                firstSelectedCell = colrow;
                firstSelectedCellVM = selectedCell;
                selectedCell.IsOpen = true;
                
                countbuttonclick++;
            }
            else if (countbuttonclick == 1)
            {
                // Если нажали на ту же ячейку - игнорируем
                if (firstSelectedCell[0] == colrow[0] && firstSelectedCell[1] == colrow[1])
                    return;
                    
                // На всякий случай проверяем, не null ли firstSelectedCellVM
                if (firstSelectedCellVM == null)
                {
                    // Если по какой-то причине первая ячейка не сохранилась, сбрасываем состояние
                    countbuttonclick = 0;
                    return;
                }
                
                // Второе нажатие - открываем вторую ячейку
                selectedCell.IsOpen = true;
                
                // Проверяем, совпадают ли компоненты
                if (selectedCell.NameСomponents == firstSelectedCellVM.NameСomponents)
                {
                    // Совпадение найдено - помечаем ячейки как сопоставленные (они останутся видимыми)
                    selectedCell.IsMatched = true;
                    firstSelectedCellVM.IsMatched = true;
                    
                    // Убираем их из модели (серверная логика)
                    _model = _service.Connect_components(_model.GameId, firstSelectedCell, colrow);
                }
                else
                {
                    // Сохраняем локальные копии ячеек, чтобы избежать проблем с null reference
                    var cell1 = selectedCell;
                    var cell2 = firstSelectedCellVM;
                    
                    // Если не совпали, закрываем обе ячейки через небольшую задержку
                    Task.Delay(800).ContinueWith(_ => {
                        // Дополнительные проверки на null внутри Task
                        if (cell1 != null) 
                            cell1.IsOpen = false;
                        if (cell2 != null) 
                            cell2.IsOpen = false;
                    }, TaskScheduler.FromCurrentSynchronizationContext()); // Запускаем в UI потоке
                }
                
                countbuttonclick = 0;
                firstSelectedCell = new int[] { -1, -1 };
                firstSelectedCellVM = null;
            }

            // Проверяем, завершена ли игра
            if (_service.IsGameOver(_model.GameId))
            {
                _service.RemoveGame(_model.GameId);
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
            _model = _service.GetByUserId(_userModel.Id);
            FromModel(_model);
        }

        public void SetUser(UserModel user)
        {
            _userModel = user;
            Initialize();
            OnPropertyChanged(nameof(UserName));
        }

        private void FromModel(GameModel model)
        {
            Cells.Clear();

            for (int row = 0; row < Constants.RowCount; row++)
            {
                for (int column = 0; column < Constants.ColumnCount; column++)
                {
                    // Пропускаем пустые ячейки (уже найденные пары)
                    if (string.IsNullOrEmpty(model[row, column]))
                        continue;

                    // Добавляем ячейку в коллекцию
                    Cells.Add(new CellViewModel
                    {
                        Row = row,
                        Column = column,
                        ColumnRow = new int[] { row, column },
                        NameСomponents = model[row, column],
                        IsOpen = false,
                        IsMatched = false
                    });
                }
            }

            OnPropertyChanged(nameof(MoveCountText));
        }

        // Метод для начала новой игры
        public void StartNewGame()
        {
            // Удаляем старую игру, если она существует
            if (_model.GameId > 0)
            {
                _service.RemoveGame(_model.GameId);
            }
            
            // Получаем новую игру с обновленными названиями
            _model = _service.GetByUserId(_userModel.Id);
            FromModel(_model);
        }
        
        // Метод для быстрого завершения текущей игры
        public void FinishCurrentGame()
        {
            if (_model.GameId > 0)
            {
                _service.RemoveGame(_model.GameId);
                _model = _service.GetByUserId(_userModel.Id);
                FromModel(_model);
            }
        }
    }
}
