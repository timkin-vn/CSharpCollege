using CardFile.Wpf.ViewModels;
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

namespace CardFile.Wpf.Views
{
    /// <summary>
    /// Interaction logic for EditCardWindow.xaml
    /// </summary>
    public partial class EditCardWindow : Window
    {
        public CardViewModel ViewModel => (CardViewModel)DataContext;

        public EditCardWindow()
        {
            InitializeComponent();
        }

        private void WokrsTillNow_Checked(object sender, RoutedEventArgs e)
        {
            ViewModel.GetIsRepairCompleted();
        }

        private void WorksTillNow_Unchecked(object sender, RoutedEventArgs e)
        {
            ViewModel.GetIsRepairCompleted();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
