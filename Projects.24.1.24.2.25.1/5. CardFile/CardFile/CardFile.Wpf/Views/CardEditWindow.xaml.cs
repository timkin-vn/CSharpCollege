﻿using CardFile.Wpf.ViewModels;
using System.Windows;

namespace CardFile.Wpf.Views
{
    /// <summary>
    /// Interaction logic for CardEditWindow.xaml
    /// </summary>
    public partial class CardEditWindow : Window
    {
        public CardViewModel ViewModel => (CardViewModel)DataContext;

        public CardEditWindow()
        {
            InitializeComponent();
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void AddHeroButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.AddHeroClicked();
        }

        private void AddItemButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.AddItemClicked();
        }

        private void AddNeutralButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.AddNeutralClicked();
        }
    }
}
