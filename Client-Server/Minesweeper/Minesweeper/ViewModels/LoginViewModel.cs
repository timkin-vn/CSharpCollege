using Minesweeper.BusinessProxy.Services;
using Minesweeper.Common.Dto;
using Minesweeper.Common.Request;
using Minesweeper.WpfClient.Helpers;
using Minesweeper.WpfClient.Services;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Minesweeper.WpfClient.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private readonly IUserServiceProxy _userService;
        private string _username;
        private bool _isLoading;
        private string _errorMessage;

        public event EventHandler<UserResponse> LoginSucceeded;

        public LoginViewModel(IUserServiceProxy userService)  
        {
            _userService = userService;  
            LoginCommand = new RelayCommand(async _ => await LoginAsync(), _ => CanLogin());
            PlayAsGuestCommand = new RelayCommand(_ => PlayAsGuest());
        }

        public string Username
        {
            get => _username;
            set
            {
                if (SetProperty(ref _username, value))
                {
                    OnPropertyChanged(nameof(CanLoginButton));
                }
            }
        }

        public bool IsLoading
        {
            get => _isLoading;
            set => SetProperty(ref _isLoading, value);
        }

        public string ErrorMessage
        {
            get => _errorMessage;
            set => SetProperty(ref _errorMessage, value);
        }

        public bool CanLoginButton => !string.IsNullOrWhiteSpace(Username) && !IsLoading;

        public ICommand LoginCommand { get; }
        public ICommand PlayAsGuestCommand { get; }

        private bool CanLogin()
        {
            return !string.IsNullOrWhiteSpace(Username) && !IsLoading;
        }

        private async Task LoginAsync()
        {
            try
            {
                IsLoading = true;
                ErrorMessage = null;

                var user = await _userService.GetOrCreateUser(Username);
                LoginSucceeded?.Invoke(this, user);
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Ошибка входа: {ex.Message}";
            }
            finally
            {
                IsLoading = false;
            }
        }

        private void PlayAsGuest()
        {
            var guest = new UserResponse
            {
                Id = 0,
                Username = "Гость",
                TotalGamesPlayed = 0,
                GamesWon = 0,
                WinRate = 0
            };
            LoginSucceeded?.Invoke(this, guest);
        }
    }
}