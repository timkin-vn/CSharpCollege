using FifteenGame.Common.BusinessDtos;
using FifteenGame.Common.Services;
using FifteenGame.Wpf.Infrastructure; 
using Ninject;
using System.ComponentModel;
using System.Windows;

namespace FifteenGame.Wpf.ViewModels
{
    public class UserLoginWindowViewModel : INotifyPropertyChanged
    {
        private readonly IUserService _userService;

        
        private string _userName = "Player1";
        public string UserName
        {
            get => _userName;
            set
            {
                _userName = value;
                OnPropertyChanged(nameof(UserName));
            }
        }

        
        public UserReply LoginResult { get; private set; }

        public UserLoginWindowViewModel()
        {
           
            _userService = NinjectKernel.Instance.Get<IUserService>();
        }

        public bool TryLogin()
        {
            if (string.IsNullOrWhiteSpace(UserName))
            {
                MessageBox.Show("Введите имя!");
                return false;
            }

            try
            {
                
                var request = new UserNameRequest { Username = UserName };

                
                LoginResult = _userService.Login(request);

                return true;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show($"Ошибка соединения с сервером:\n{ex.Message}\n\nУбедитесь, что сервер запущен!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string prop) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}