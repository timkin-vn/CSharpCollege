using FifteenGame.Business.Models;
using FifteenGame.Wpf.ViewModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace FifteenGame.Wpf
{
    public partial class MainWindow : Window
    {
        public MainWindowViewModel ViewModel => (MainWindowViewModel)DataContext;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CreateGameGrid();
        }

        private void CreateGameGrid()
        {

            var itemsControl = GameBoard;

            if (itemsControl.ItemsPanel != null)
            {
                var grid = new Grid();

                for (int i = 0; i < SnakeGameModel.Height; i++)
                {
                    grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
                }

                for (int i = 0; i < SnakeGameModel.Width; i++)
                {
                    grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                }

                grid.Background = System.Windows.Media.Brushes.LightYellow;

                itemsControl.ItemsPanel = new ItemsPanelTemplate();
                var factory = new FrameworkElementFactory(typeof(Grid));
                itemsControl.ItemsPanel.VisualTree = factory;

                itemsControl.UpdateLayout();
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Up:
                    ViewModel.ChangeDirection(Direction.Up);
                    break;
                case Key.Down:
                    ViewModel.ChangeDirection(Direction.Down);
                    break;
                case Key.Left:
                    ViewModel.ChangeDirection(Direction.Left);
                    break;
                case Key.Right:
                    ViewModel.ChangeDirection(Direction.Right);
                    break;
                case Key.Space:
                    ViewModel.NewGame();
                    break;
            }
        }
    }
}