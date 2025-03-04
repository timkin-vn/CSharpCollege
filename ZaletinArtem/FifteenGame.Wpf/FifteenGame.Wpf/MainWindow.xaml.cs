using FifteenGame.Business.Models;
using FifteenGame.Wpf.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FifteenGame.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindowViewModel ViewModel => (MainWindowViewModel)DataContext;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel();
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);

            switch (e.Key)
            {
                case Key.Left:
                    ViewModel.MakeMove(MoveDirection.Left);
                    break;
                case Key.Right:
                    ViewModel.MakeMove(MoveDirection.Right);
                    break;
                case Key.Up:
                    ViewModel.MakeMove(MoveDirection.Up);
                    break;
                case Key.Down:
                    ViewModel.MakeMove(MoveDirection.Down);
                    break;
            }
        }
    }
}
