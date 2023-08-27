using Prism.Events;
using Prism.Windows.Mvvm;
using Prism.Windows.Navigation;
using ProductsCRUD.Models.Auth;
using ProductsCRUD.Models.Users;
using ProductsCRUD.Services;
using ProductsCRUD.Services.Users;
using ProductsCRUD.Util;
using System.Collections.Generic;
using System.Collections.ObjectModel;

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

        public void RemoveUser(string userId)
        {
            userService.RemoveUser(userId);
            LoadUsers();
        }

        public void UpdateUser(string userId)
        {
            navigationService.Navigate(PageTokens.USER_EDITION, userId);
            LoadUsers();
        }

        private void LoadUsers()
        {
            Users.Clear();
            var allUsers = userService.GetAllUsers();

            foreach (var user in allUsers)
            {
                Users.Add(user);
            }
        }
    }
}
