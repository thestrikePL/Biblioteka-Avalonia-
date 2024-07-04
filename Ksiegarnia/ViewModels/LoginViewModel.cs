using Ksiegarnia.DTO;
using Ksiegarnia.Model;
using Ksiegarnia.Persistence;
using Ksiegarnia.Service;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;

namespace Ksiegarnia.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private string _username;
        private string _password;
        private string _errorMessage;

        private UserService _userService;
        private AuthService _authService;

        public string Username
        {
            get => _username;
            set => this.RaiseAndSetIfChanged(ref _username, value);
        }

        public string Password
        {
            get => _password;
            set => this.RaiseAndSetIfChanged(ref _password, value);
        }

        public string ErrorMessage
        {
            get => _errorMessage;
            set => this.RaiseAndSetIfChanged(ref _errorMessage, value);
        }

        public ReactiveCommand<Unit, Unit> LoginCommand { get; }
        public ReactiveCommand<Unit, Unit> RegisterCommand { get; }

        private MainWindowViewModel _mainWindowViewModel;

        public LoginViewModel(MainWindowViewModel mainWindowViewModel, IAppDbContext appDbContext)
        {
            _userService = new UserService(appDbContext);
            _authService = new AuthService(_userService);
            LoginCommand = ReactiveCommand.Create(LoginUser);
            RegisterCommand = ReactiveCommand.Create(RegisterUser);
            _mainWindowViewModel = mainWindowViewModel;
        }

        private void LoginUser()
        {
            // Auth process
            //if success 
            var loginRequest = new LoginRequest()
            {
                Username = _username,
                Password = _password
            };

            try
            {
                var result = _authService.Login(loginRequest);
                if (result != null)
                {
                    _mainWindowViewModel.CurrentUser = result;
                    _mainWindowViewModel.ShowBooksView();
                }
            } 
            catch (Exception e)
            {
                ErrorMessage = "Invalid login data!";
            }
            
        }

        private void RegisterUser()
        {
            _mainWindowViewModel.ShowRegisterView();
        }
    }
}
