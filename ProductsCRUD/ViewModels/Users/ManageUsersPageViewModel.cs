using Prism.Windows.Mvvm;
using Prism.Windows.Navigation;
using ProductsCRUD.Models.Users;
using ProductsCRUD.Services.Users;
using ProductsCRUD.Util;
using System;
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

        private readonly IUserService _userService;
        private readonly INavigationService navigationService;

        public ManageUsersPageViewModel(IUserService userService,
            INavigationService navigationService)
        {
            Users = new ObservableCollection<User>();
            _userService = userService;
            this.navigationService = navigationService;
            LoadUsers();
        }

        private void LoadUsers()
        {
            Users.Clear();
            var allUsers = _userService.GetAllUsers();

            foreach (var user in allUsers)
            {
                Users.Add(user);
            }
        }

        public override void OnNavigatedTo(NavigatedToEventArgs e, Dictionary<string, object> viewModelState)
        {
            base.OnNavigatedTo(e, viewModelState);

            LoadUsers();
        }

        public void RemoveUser(string userId)
        {
            _userService.RemoveUser(userId);
            LoadUsers();
        }

        public void UpdateUser(string userId)
        {
            navigationService.Navigate(PageTokens.USER_EDITION, userId);
            LoadUsers();
        }
    }
}
