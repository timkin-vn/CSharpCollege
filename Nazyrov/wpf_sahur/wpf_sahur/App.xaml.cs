using wpf_sahur.ViewModels;
using wpf_sahur.Views;
using wpf_sahur_business.Services;
using System.Configuration;
using System.Data;
using System.Windows;
using static wpf_sahur.Views.LoginView;

namespace wpf_sahur;

public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        string connectionString = "Host=localhost;Port=5432;Username=postgres;Password=postgres;Database=TicTacToedb";

        var authService = new AuthService(connectionString);

        var loginView = new LoginView();
        loginView.DataContext = new LoginViewModel(authService);
        loginView.Show();
    }
}

