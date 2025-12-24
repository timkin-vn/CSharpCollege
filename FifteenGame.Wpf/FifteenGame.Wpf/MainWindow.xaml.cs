using System.Windows;

namespace StepByStepPacman.WPF
{
    public partial class MainWindow : Window
    {
        // оставляем, чтобы не сломать места, где окно создают с userId
        private readonly int _currentUserId;

        // Конструктор без аргументов (как у тебя было)
        public MainWindow() : this(0)
        {
        }

        // Критически важный конструктор (как у тебя было)
        public MainWindow(int userId)
        {
            InitializeComponent();

            _currentUserId = userId;

            // ВАЖНО:
            // DataContext уже задан в MainWindow.xaml:
            // <Window.DataContext><vm:TicTacToeViewModel/></Window.DataContext>
            // Поэтому тут ничего НЕ назначаем, иначе XAML-подключение сломается.
        }
    }
}
