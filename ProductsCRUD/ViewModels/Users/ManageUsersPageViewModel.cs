using Prism.Windows.Mvvm;
using Prism.Windows.Navigation;
using ProductsCRUD.Business.Services;
using ProductsCRUD.Business.Services.Users;
using ProductsCRUD.Common.Util;
using ProductsCRUD.Common.Util.Labels;
using ProductsCRUD.Common.Util.Messages.Users;
using ProductsCRUD.Domain.Models.Users;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Windows.UI.Xaml.Controls;

namespace ProductsCRUD.ViewModels
{
    public class ManageUsersPageViewModel : ViewModelBase
    {
        private ObservableCollection<User> _users;
        public ObservableCollection<User> Users
        {
            get { return _users; }
            set { SetProperty(ref _users, value); }
        }

        private readonly IUserService userService;
        private readonly INavigationService navigationService;
        private readonly IAuthManagerService authManagerService;

        public ManageUsersPageViewModel(IUserService userService,
            INavigationService navigationService,
            IAuthManagerService authManagerService)
        {
            this.navigationService = navigationService;
            this.authManagerService = authManagerService;
            this.userService = userService;
            Users = new ObservableCollection<User>();
        }

        public override void OnNavigatedTo(NavigatedToEventArgs e, Dictionary<string, object> viewModelState)
        {
            base.OnNavigatedTo(e, viewModelState);

            if (authManagerService.IsAuthenticatedUserOnSession())
                LoadUsers();
        }

        public async void RemoveUser(string userId)
        {
            var confirmDialog = new ContentDialog
            {
                Title = "Confirmar Remoção",
                Content = "O usuário será removido. Deseja continuar?",
                PrimaryButtonText = "Sim",
                CloseButtonText = "Não"
            };

            var result = await confirmDialog.ShowAsync();

            if (result == ContentDialogResult.Primary)
            {
                userService.RemoveUser(userId);
                ShowRemoveMessage();
                LoadUsers();
            }
            else
            {
                ShowCanceledMessage();
            }

        }

        public void UpdateUser(string userId)
        {
            navigationService.Navigate(PageTokens.UsersPage.USER_EDITION_PAGE, userId);
            LoadUsers();
        }

        private async void LoadUsers()
        {
            Users.Clear();
            var allUsers = await userService.GetAllUsers();

            foreach (var user in allUsers)
            {
                Users.Add(user);
            }
        }

        private async void ShowRemoveMessage()
        {
            var dialog = new ContentDialog
            {
                Title = UserMessages.USER_REMOVED_TITLE,
                Content = UserMessages.USER_REMOVED_CONTENT,

                CloseButtonText = ButtonLabel.CLOSE_OK
            };
            await dialog.ShowAsync();
        }

        private async void ShowCanceledMessage()
        {
            var dialog = new ContentDialog
            {
                Title = "Não executado",
                Content = "Ação cancelada pelo usuário",

                CloseButtonText = ButtonLabel.CLOSE_OK
            };
            await dialog.ShowAsync();
        }
    }
}
