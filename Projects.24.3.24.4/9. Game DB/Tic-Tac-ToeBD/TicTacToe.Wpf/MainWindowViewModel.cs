using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using TicTacToe.Wpf.Commands;
using TicTacToe.Business.Services;

namespace TicTacToe.Wpf
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private readonly GameService _service = new GameService();
        public ObservableCollection<string> Cells { get; set; } = new ObservableCollection<string>();
        public ICommand CellClickCommand { get; set; }

        // ДОБАВЛЯЕМ ЭТУ ПЕРЕМЕННУЮ
        private bool _isXTurn = true;

        public event PropertyChangedEventHandler PropertyChanged;

        public MainWindowViewModel()
        {
            for (int i = 0; i < 9; i++) Cells.Add(" ");
            CellClickCommand = new RelayCommand(ExecuteCellClick);
        }

        private void ExecuteCellClick(object parameter)
        {
            int index = Convert.ToInt32(parameter);
            if (Cells[index] != " ") return;

            // Логика хода
            Cells[index] = _isXTurn ? "X" : "O";

            char[] board = new char[9];
            for (int i = 0; i < 9; i++) board[i] = Cells[i][0];

            string winner = _service.CheckWinner(board);
            if (winner != null) System.Windows.MessageBox.Show($"Победитель: {winner}!");

            _service.SaveGame(1, board, _isXTurn);

            _isXTurn = !_isXTurn; // Переключаем ход
        }

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}