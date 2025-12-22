using Pacman.Client.Infrastructure; 
using Pacman.Common.Dtos;
using Pacman.Common.Services;
using System.ComponentModel;
using System.Windows;
using Ninject;

namespace Pacman.Client.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        private readonly IUserService _userService;

        private string _username = "Player1";
        public string Username
        {
            get => _username;
            set { _username = value; OnPropertyChanged(nameof(Username)); }
        }

        public UserDto LoggedUser { get; private set; }

        public LoginViewModel()
        {
            
            _userService = NinjectKernel.Instance.Get<IUserService>();
        }

        public void TryLogin(Window window)
        {
            if (string.IsNullOrWhiteSpace(Username)) return;

            try
            {
                
                LoggedUser = _userService.Login(Username);

                window.DialogResult = true;
                window.Close();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show($"Ошибка входа: {ex.Message}\nПроверь, запущен ли сервер!");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string prop) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}