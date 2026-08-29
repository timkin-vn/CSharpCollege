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
using System.Windows.Shapes;
using CardFile.WPF.ViewModels;

namespace CardFile.WPF.Views
{
    /// <summary>
    /// Логика взаимодействия для CardEditWindow.xaml
    /// </summary>
    public partial class CardEditWindow : Window
    {
        public CardViewModel ViewModel => (CardViewModel)DataContext;

        public CardEditWindow()
        {
            InitializeComponent();
        }

        private void OkButtonClick(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void CancelButtonClick(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void TillCheck(object sender, RoutedEventArgs e)
        {
            ViewModel.IsWorkingTillNowChecked();
        }
        private void TillUnchecked(object sender, RoutedEventArgs e)
        {
            ViewModel.IsWorkingTillNowUnChecked();
        }
    }
}
