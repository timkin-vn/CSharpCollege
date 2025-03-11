using System.Windows;
using WordGame.ViewModels;

namespace WordGame
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