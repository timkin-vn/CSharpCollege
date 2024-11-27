using CardFile.Wpf.View;
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

namespace CardFile.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        internal CardFileViewModel ViewModel => (CardFileViewModel)DataContext;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var window = new EditCardWindow();
            var cardViewModel = ViewModel.GetNewCard();
            window.ViewModel.CopyFrom(cardViewModel);
            if (window.ShowDialog() ?? false)
            {
                if (!ViewModel.Save(window.ViewModel))
                {
                    MessageBox.Show("Не удалось сохранить запись", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            var cardViewModel = ViewModel.GetSelectedCard();
            if (cardViewModel == null)
            {
                return;
            }

            var window = new EditCardWindow();
            window.ViewModel.CopyFrom(cardViewModel);
            if (window.ShowDialog() ?? false)
            {
                if (!ViewModel.Save(window.ViewModel))
                {
                    MessageBox.Show("Не удалось сохранить запись", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (!ViewModel.DeleteSelected())
            {
                MessageBox.Show("Не удалось удалить запись", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
