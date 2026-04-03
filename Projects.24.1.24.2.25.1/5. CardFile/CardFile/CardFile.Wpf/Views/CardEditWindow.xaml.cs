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
using CardFile.Wpf.Infrastructure; // Нужено

namespace CardFile.Wpf.Views
{

    // Логика окна редактирования.

    public partial class CardEditWindow : Window
    {
        public CardEditWindow() => InitializeComponent();

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void IsNotWrittenOffChecked(object sender, RoutedEventArgs e)
        {
            var viewModel = DataContext as CardViewModel;
            if (viewModel != null)
                viewModel.IsNotWrittenOff = true;
        }

        private void IsNotWrittenOffUnchecked(object sender, RoutedEventArgs e)
        {
            var viewModel = DataContext as CardViewModel;
            if (viewModel != null)
                viewModel.IsNotWrittenOff = false;
        }
    }
}
