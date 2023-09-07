using Prism.Windows.Navigation;
using ProductsCRUD.Business.Services.Users;
using ProductsCRUD.Common.Exceptions;
using ProductsCRUD.Common.Util;
using ProductsCRUD.Common.Util.Labels;
using System;
using Windows.UI.Xaml.Controls;

namespace ProductsCRUD.Business.Services.Navigation
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
                navigationService.Navigate(PageTokens.UsersPage.LOGIN_PAGE, null);
                return false;
            }

            return true;
        }

        public void Logoff()
        {
            try
            {
                userService.Logout();
                ShowMessage("Atenção!", "Você foi desconectado da aplicação!");
                navigationService.Navigate(PageTokens.UsersPage.LOGIN_PAGE, null);
            }
            catch (UserNotLoggedException)
            {
                navigationService.Navigate(PageTokens.UsersPage.LOGIN_PAGE, null);
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
