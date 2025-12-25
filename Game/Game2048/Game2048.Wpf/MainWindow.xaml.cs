using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Game2048.Business.Models;
using Game2048.Wpf.ViewModels;

namespace Game2048.Wpf;

public partial class MainWindow : Window
{
    private MainWindowViewModel _viewModel;

    public MainWindow()
    {
        InitializeComponent();
        _viewModel = new MainWindowViewModel();
        DataContext = _viewModel;
    }

    private void Window_KeyDown(object sender, KeyEventArgs e)
    {
        switch (e.Key)
        {
            case Key.Up:
                _viewModel.MakeMove(MoveDirection.Up);
                break;
            case Key.Down:
                _viewModel.MakeMove(MoveDirection.Down);
                break;
            case Key.Left:
                _viewModel.MakeMove(MoveDirection.Left);
                break;
            case Key.Right:
                _viewModel.MakeMove(MoveDirection.Right);
                break;
        }
    }
}