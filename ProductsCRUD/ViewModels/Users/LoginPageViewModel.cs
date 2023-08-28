﻿using Prism.Windows.Mvvm;
using Prism.Windows.Navigation;
using ProductsCRUD.Services.Users;
using ProductsCRUD.Util;
using ProductsCRUD.Util.Labels;
using System;
using Windows.UI.Xaml.Controls;

namespace ProductsCRUD.ViewModels
{
    public class LoginPageViewModel : ViewModelBase
    {
        private string _email;
        public string Email
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
            if (userService.TryLogin(Email, Password, out _))
            {
                var user = userService.GetUserByEmail(Email);
                ShowMessage("Sucesso!", $"Seja bem vindo(a) {user.FirstName} {user.LastName}");
                navigationService.Navigate(PageTokens.PRODUCT_MAIN, null);
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
