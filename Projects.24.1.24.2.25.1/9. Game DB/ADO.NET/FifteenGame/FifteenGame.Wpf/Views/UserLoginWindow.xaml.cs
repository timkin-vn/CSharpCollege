using CheckersGame.Business.Models;
using CheckersGame.Wpf;
using CheckersGame.Wpf.ViewModels;
using FifteenGame.Common.Contracts.Services;
using FifteenGame.Common.Infrastructure;
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
using CheckersGame.Business.Contracts;
using Ninject;
using GameModel = CheckersGame.Business.Models.GameModel;
using Ninject.Modules;
using FifteenGame.Wpf.ViewModels;


namespace FifteenGame.Wpf.Views
{
    public partial class UserLoginWindow : Window
    {
        private UserLoginWindowViewModel _viewModel;

        public UserLoginWindow()
        {
            InitializeComponent();

            // Попробуем получить ViewModel из DataContext (если задан в XAML)
            _viewModel = DataContext as UserLoginWindowViewModel;
            if (_viewModel == null)
            {
                var kernel = NinjectKernel.Instance;
                if (kernel != null)
                {
                    _viewModel = kernel.Get<UserLoginWindowViewModel>();
                    DataContext = _viewModel;
                }
            }

            if (_viewModel != null)
                _viewModel.UserLoggedIn += OnUserLoggedIn;
        }

        private void OnUserLoggedIn(GameModel model)
        {
            var kernel = NinjectKernel.Instance;
            var gameService = kernel.Get<ICheckersGameService>();

            var mainWindow = new MainWindow();
            var mainVM = new MainWindowViewModel(model, false, gameService);
            mainWindow.DataContext = mainVM;
            mainWindow.Show();
            this.Close();
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            if (_viewModel == null) return;

            try
            {
                if (_viewModel.FindUser())
                    _viewModel.CommitUser();
                else
                {
                    var result = MessageBox.Show("Пользователь не найден. Создать нового?",
                        "Вход", MessageBoxButton.YesNo);
                    if (result == MessageBoxResult.Yes)
                    {
                        _viewModel.CreateUser();
                        _viewModel.CommitUser();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при входе: {ex.Message}", "Ошибка",
                                MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}