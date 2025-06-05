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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CardFile.Wpf.Views
{
    /// <summary>
    /// Логика взаимодействия для Page1.xaml
    /// </summary>
    public partial class EditCardWindow : Window
    {
        public CardViewModel ViewModel => (CardViewModel)DataContext;

        public EditCardWindow()
        {
            InitializeComponent();
        }

        private void AvaliableChecked(object sender, RoutedEventArgs e)
        {
            
        }

        private void AvaliableUnchecked(object sender, RoutedEventArgs e)
        { 
            
        }

        private void SeasonChecked(object sender, RoutedEventArgs e)
            { }
        private void SeasonUnchecked(object sender, RoutedEventArgs e) { }

        private void VeganChecked(object sender, RoutedEventArgs e)
        {

        }
        private void VeganUnchecked(object sender, RoutedEventArgs e) { }

        private void SpicyChecked(object sender, RoutedEventArgs e)
        {

        }

        private void SpicyUnchecked(object sender, RoutedEventArgs e)
        { }


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

