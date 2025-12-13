using System.Windows;
using MinesweeperEF.Client.BusinessProxy;

namespace MinesweeperEF.Client;

public partial class MainWindow {
    private readonly UserServiceProxy _users;

    public MainWindow() {
        InitializeComponent();

        var api = new ApiClient("http://localhost:5000");
        _users = new UserServiceProxy(api);
    }

    private async void Register_Click(object sender, RoutedEventArgs e) {
        await _users.RegisterAsync(UserBox.Text, PassBox.Password);
        MessageBox.Show("Registered");
    }

    private async void Login_Click(object sender, RoutedEventArgs e) {
        await _users.LoginAsync(UserBox.Text, PassBox.Password);
        MessageBox.Show("Logged in, token received");
    }
}
