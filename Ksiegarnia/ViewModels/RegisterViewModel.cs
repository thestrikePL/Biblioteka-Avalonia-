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
    public class RegisterViewModel : ViewModelBase
    {
        private string _username;
        private string _password;

        private MainWindowViewModel _mainWindowViewModel;
        private UserService _userService;

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

        public ReactiveCommand<Unit, Unit> RegisterCommand { get; }
        public ReactiveCommand<Unit, Unit> BackCommand { get; }

        public RegisterViewModel(MainWindowViewModel mainWindowViewModel, IAppDbContext appDbContext)
        {
            _mainWindowViewModel = mainWindowViewModel;
            _userService = new UserService(appDbContext);

            RegisterCommand = ReactiveCommand.Create(RegisterUser);
            BackCommand = ReactiveCommand.Create(GoBack);
        }

        private async void RegisterUser()
        {
            var newUser = await _userService.CreateUser(Username, Password);
            _mainWindowViewModel.CurrentUser = newUser;
            _mainWindowViewModel.ShowBooksView();
        }

        private void GoBack()
        {
            _mainWindowViewModel.ShowLoginView();
        }

    }
}
