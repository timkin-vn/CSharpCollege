using System.Windows;
using System.Windows.Input;
using Game2048.Common.BusinessModels;
using Game2048.Common.Definitions;

namespace Game2048.Wpf
{
    public partial class MainWindow : Window
    {
        public MainWindowViewModel GameContext => (MainWindowViewModel)DataContext;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void OnScreenLoaded(object sender, RoutedEventArgs e)
        {
            var authScreen = new UserLoginWindow();
            if (authScreen.DataContext is UserLoginWindowViewModel authContext)
            {
                authContext.MainViewModel = GameContext;
            }
            authScreen.ShowDialog();
        }

        private void OnKeyPressed(object sender, KeyEventArgs e)
        {
            MoveDirection? targetSide = null;

            switch (e.Key)
            {
                case Key.Left:
                case Key.A:
                    targetSide = MoveDirection.Left;
                    break;

                case Key.Right:
                case Key.D:
                    targetSide = MoveDirection.Right;
                    break;

                case Key.Up:
                case Key.W:
                    targetSide = MoveDirection.Up;
                    break;

                case Key.Down:
                case Key.S:
                    targetSide = MoveDirection.Down;
                    break;
            }

            if (targetSide.HasValue)
            {
                GameContext.MakeMove(targetSide.Value, OnSessionOver, OnSessionWon);
            }
        }

        private void OnResetSessionClick(object sender, RoutedEventArgs e)
        {
            var promptResult = MessageBox.Show(
                "Вы действительно хотите сбросить текущую сессию и начать заново?",
                "Подтверждение сброса",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);

            if (promptResult == MessageBoxResult.Yes)
            {
                GameContext.NewGame();
            }
        }

        private void OnSessionWon()
        {
            MessageBox.Show(
                "Цель достигнута! Плитка 2048 собрана.\n\nВы можете продолжить устанавливать новые рекорды!",
                "Успех!",
                MessageBoxButton.OK,
                MessageBoxImage.Asterisk);
        }

        private void OnSessionOver()
        {
            var promptResult = MessageBox.Show(
                $"Доступные ходы исчерпаны!\n\nФинальный счёт: {GameContext.Score}\nРекордная плитка: {GameContext.BestTile}\n\nПопробовать снова?",
                "Раунд завершен",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (promptResult == MessageBoxResult.Yes)
            {
                GameContext.NewGame();
            }
        }
    }
}