using FifteenGame.Wpf.ViewModels;
using FifteenGame.Wpf.Views;
using System.Windows;
using System.Windows.Input;

namespace FifteenGame.Wpf
{
    public partial class MainWindow : Window
    {
        private TicTacToeViewModel GameVm => (TicTacToeViewModel)DataContext;

        // Это "мост" только для логина: UserLoginWindowViewModel ожидает MainViewModel.SetUser(...)
        private readonly MainWindowViewModel _loginMainVm = new MainWindowViewModel();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // 1) создаём VM логина и ОБЯЗАТЕЛЬНО задаём MainViewModel
            var loginVm = new UserLoginWindowViewModel
            {
                MainViewModel = _loginMainVm
            };

            // 2) открываем окно логина с этим VM
            var loginWindow = new UserLoginWindow
            {
                DataContext = loginVm
            };

            loginWindow.ShowDialog();

            // 3) Если пользователь выбран — он лежит в _loginMainVm.CurrentUser (ты уже добавлял)
            var user = _loginMainVm.CurrentUser;
            if (user == null)
            {
                Close();
                return;
            }

            // 4) Передаём в игру
            GameVm.SetUser(user);
        }

        private void Cell_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var tag = (int[])((FrameworkElement)sender).Tag;
            var result = GameVm.MakeMove(tag);

            if (result.IsFinished)
            {
                var text = result.WinnerSymbol == null
                    ? "Ничья! Сыграем ещё раз?"
                    : $"Победил(а): {result.WinnerSymbol}\nСыграем ещё раз?";

                if (MessageBox.Show(text, "Крестики-нолики", MessageBoxButton.YesNo, MessageBoxImage.Information)
                    == MessageBoxResult.Yes)
                {
                    GameVm.Reset();
                }
            }
        }

        private void NewGameButton_Click(object sender, RoutedEventArgs e)
        {
            GameVm.Reset();
        }
    }
}
