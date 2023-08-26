using ProductsCRUD.ViewModels;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace ProductsCRUD.Views
{
    public sealed partial class ManageUsersPage : Page
    {
        ManageUsersPageViewModel ViewModel => (ManageUsersPageViewModel)DataContext;
        public ManageUsersPage()
        {
            InitializeComponent();
        }

        public void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button != null && button.Tag is string userId)
            {
                ViewModel.RemoveUser(userId);
            }
        }

        public void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button != null && button.Tag is string userId)
            {
                ViewModel.UpdateUser(userId);
            }
        }
    }
}
