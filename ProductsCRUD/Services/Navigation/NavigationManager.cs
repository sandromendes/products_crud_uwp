﻿using Prism.Windows.Navigation;
using ProductsCRUD.Exceptions;
using ProductsCRUD.Services.Users;
using ProductsCRUD.Util;
using ProductsCRUD.Util.Labels;
using System;
using Windows.UI.Xaml.Controls;

namespace ProductsCRUD.Services.Navigation
{
    public class NavigationManager : INavigationManager
    {
        private readonly INavigationService navigationService;
        private readonly IUserService userService;
        private readonly IAuthManagerService authManagerService;

        public NavigationManager(INavigationService navigationService,
            IUserService userService,
            IAuthManagerService authManagerService)
        {
            this.navigationService = navigationService;
            this.userService = userService;
            this.authManagerService = authManagerService;
        }

        public bool IsValidAuth()
        {
            if (!authManagerService.IsAuthenticatedUserOnSession())
            {
                ShowMessage("Atenção!", "Faça login para acessar esta funcionalidade.");
                navigationService.Navigate(PageTokens.LOGIN, null);
                return false;
            }

            return true;
        }

        public void Logoff()
        {
            try
            {
                userService.Logout();
                ShowMessage("Atenção!", "Você foi desconectado! " +
                    "Faça login novamente para acessar novamente.");
            }
            catch (UserNotLoggedException)
            {
                navigationService.Navigate(PageTokens.LOGIN, null);
                ShowMessage("Atenção!", "Não há nenhum usuário autenticado.");
            }
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
