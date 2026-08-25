using System.Windows;

namespace TicTacToe.Wpf
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // Здесь мы создаем экземпляр ViewModel и "привязываем" его к окну
            // Теперь все {Binding} в XAML будут смотреть сюда
            this.DataContext = new MainWindowViewModel();
        }
    }
}