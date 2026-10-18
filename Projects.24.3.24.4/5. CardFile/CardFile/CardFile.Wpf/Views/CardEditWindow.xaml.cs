using CardFile.Wpf.ViewModels;
using Microsoft.Win32;
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
    public partial class CardEditWindow : Window
    {
        public CardViewModel ViewModel
        {
            get => (CardViewModel)DataContext;
            set => DataContext = value;
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        public CardEditWindow()
        {
            InitializeComponent();
            Loaded += CardEditWindow_Loaded;
        }

        private void CardEditWindow_Loaded(object sender, RoutedEventArgs e)
        {
            ExpulsionDatePicker.IsEnabled = (StudyingCheckBox.IsChecked != true);
        }

        private void StudyingCheckBox_Click(object sender, RoutedEventArgs e)
        {
            ExpulsionDatePicker.IsEnabled = (StudyingCheckBox.IsChecked != true);
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void SelectPhotoButton_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog
            {
                Title = "Выберите фотографию студента",
                Filter = "Изображения (*.jpg;*.jpeg;*.png;*.bmp)|*.jpg;*.jpeg;*.png;*.bmp|Все файлы (*.*)|*.*"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                if (DataContext is CardViewModel vm)
                {
                    vm.PhotoPath = openFileDialog.FileName;
                }
            }
        }

        private void ClearPhotoButton_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is CardViewModel vm)
            {
                vm.PhotoPath = null;
            }
        }
    }
}
