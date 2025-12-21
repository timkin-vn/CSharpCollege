using _2048Game.WPF.ViewModels;
using System.Windows;
using System.Windows.Input;
using _2048Game.Business.Services;
using _2048Game.Business.Models;

namespace _2048Game.WPF
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly string _username;
        private readonly SokobanService _service;

        public MainWindow(string username, SokobanBoard board)
        {
            InitializeComponent();
            _username = username;
            _service = new SokobanService(board);
            this.DataContext = new MainWindowViewModel(_service);
        }

        public MainWindow() : this("", null)
        {
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (DataContext is MainWindowViewModel vm)
            {
                vm.HandKey(e.Key);
            }
        }

        private void RestartButton_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is MainWindowViewModel vm)
            {
                vm.RestartLevel();
            }

            // ensure window has keyboard focus after clicking restart
            this.Focus();
            Keyboard.Focus(this);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // save board for user
            if (!string.IsNullOrEmpty(_username))
            {
                UserService.SaveBoard(_username, _service.Board);
            }
        }
    }
}
