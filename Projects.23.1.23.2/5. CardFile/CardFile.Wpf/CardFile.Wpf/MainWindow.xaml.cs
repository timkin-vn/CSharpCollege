﻿using CardFile.Wpf.ViewModels;
using CardFile.Wpf.Views;
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
        public MainWindowViewModel ViewModel => (MainWindowViewModel)DataContext;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ViewModel.Loaded();
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            var newCard = ViewModel.GetNewCard();
            var editWindow = new EditCardWindow();
            editWindow.ViewModel.LoadFromViewModel(newCard);

            if (editWindow.ShowDialog() == true)
            {
                ViewModel.InsertNewCard(editWindow.ViewModel);
            }
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            var card = ViewModel.GetSelectedCard();
            if (card == null)
            {
                return;
            }

            var editWindow = new EditCardWindow();
            editWindow.ViewModel.LoadFromViewModel(card);

            if (editWindow.ShowDialog() == true)
            {
                ViewModel.UpdateCard(editWindow.ViewModel);
            }
        }

        private void Cards_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ViewModel.SelectionChanged();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.DeleteSelectedCard();
        }
    }
}
