using Prism.Windows.Mvvm;
using Prism.Windows.Navigation;
using ProductsCRUD.Models.Auth;
using ProductsCRUD.Services.Navigation;
using ProductsCRUD.Util;
using Windows.UI.Xaml.Controls;

namespace ProductsCRUD.ViewModels
{
    public class MenuViewModel : ViewModelBase
    {
        private readonly INavigationService navigationService;
        private readonly INavigationManager navigationManager;

        public MenuViewModel(INavigationService navigationService,
            INavigationManager navigationManager)
        {
            this.navigationService = navigationService;
            this.navigationManager = navigationManager;
        }

        public void OnItemInvoked(object sender, NavigationViewItemInvokedEventArgs e)
        {
            var clickedItemContainer = e.InvokedItemContainer;

            if (e.IsSettingsInvoked)
            {
                navigationService.Navigate(PageTokens.SettingsPage.MAIN, null);
                return;
            }

            var tag = clickedItemContainer.Tag as string;

            var menuTag = EnumUtil.GetValueFromDescription<MenuViewTokens>(tag);

            if (navigationManager.IsValidAuth())
            {
                switch (menuTag)
                {
                    case MenuViewTokens.HOME_PAGE:
                        navigationService.Navigate(PageTokens.HomePage.MAIN, null);
                        break;
                    case MenuViewTokens.PRODUCTS_MAIN:
                        navigationService.Navigate(PageTokens.ProductsPage.MAIN, null);
                        break;
                    case MenuViewTokens.CUSTOMERS_MAIN:
                        navigationService.Navigate(PageTokens.CustomersPage.MAIN, null);
                        break;
                    case MenuViewTokens.VENDORS_MAIN:
                        navigationService.Navigate(PageTokens.VendorsPage.MAIN, null);
                        break;
                    case MenuViewTokens.ORDERS_MAIN:
                        navigationService.Navigate(PageTokens.OrdersPage.MAIN, null);
                        break;
                    case MenuViewTokens.SALES_MAIN:
                        navigationService.Navigate(PageTokens.SalesPage.MAIN, null);
                        break;
                    case MenuViewTokens.CUSTOMERS_REPORT:
                        break;
                    case MenuViewTokens.ORDERS_REPORT:
                        break;
                    case MenuViewTokens.SALES_REPORT:
                        break;
                    case MenuViewTokens.USER_REGISTER:
                        navigationService.Navigate(PageTokens.UsersPage.USER_REGISTER_PAGE, null);
                        break;
                    case MenuViewTokens.PASSWORD_RECOVER:
                        navigationService.Navigate(PageTokens.UsersPage.USER_PASSWORD_RECOVER_PAGE, null);
                        break;                
                    case MenuViewTokens.LOGOUT:
                        navigationManager.Logoff();
                        break;
                    case MenuViewTokens.MANAGE_USERS:
                        navigationService.Navigate(PageTokens.UsersPage.USERS_MANAGEMENT_PAGE, null);
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
