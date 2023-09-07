using Prism.Windows.Mvvm;
using Prism.Windows.Navigation;
using ProductsCRUD.Business.Services.Users;
using ProductsCRUD.Common.Util;
using ProductsCRUD.Common.Util.Labels;
using ProductsCRUD.Domain.Models.Users;
using System;
using Windows.UI.Xaml.Controls;

namespace ProductsCRUD.ViewModels
{
    public class LoginPageViewModel : ViewModelBase
    {
        private string _email;
        public string UserLogin
        {
            get => _email;
            set => SetProperty(ref _email, value);
        }

        private string _password;
        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        private string _errorMessage;
        public string ErrorMessage
        {
            get => _errorMessage;
            set => SetProperty(ref _errorMessage, value);
        }

        private readonly IUserService userService;
        private readonly INavigationService navigationService;

        public LoginPageViewModel(IUserService userService,
            INavigationService navigationService)
        {
            this.userService = userService;
            this.navigationService = navigationService;
        }

        public void Login()
        {
            bool isSuccess;
            User user = null;
            if (UserLogin.Contains("@"))
            {
                userService.TryLogin(UserLogin, Password, out isSuccess);
                user = userService.GetUserByEmail(UserLogin);
            }
            else
            {
                userService.TryLoginWithUserName(UserLogin, Password, out isSuccess);
                user = userService.GetUser(u => u.FirstName == UserLogin);
            }

            if (isSuccess)
            {
                ShowMessage("Bem vindo!", $"Seja bem vindo(a) {user.FirstName}");
                navigationService.Navigate(PageTokens.HomePage.MAIN, null);
            }
            else
                ShowMessage("Acesso negado!", $"Usuário e/ou senha inválidos.");
        }

        private async void ShowMessage(string title, string content)
        {
            var dialog = new ContentDialog
            {
                Title = title,
                Content = content,

                CloseButtonText = ButtonLabel.CLOSE_OK
            };
            await dialog.ShowAsync();
        }
    }
}
