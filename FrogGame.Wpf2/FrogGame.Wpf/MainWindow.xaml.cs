using FrogGame.Wpf.ViewModels;
using System.Windows;

namespace FrogGame.Wpf
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel();
        }
    }
}