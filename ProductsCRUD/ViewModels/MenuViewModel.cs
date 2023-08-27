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
                navigationService.Navigate(PageTokens.SETTINGS, null);
                return;
            }

            var tag = clickedItemContainer.Tag as string;

            var menuTag = EnumUtil.GetValueFromDescription<MenuViewTokens>(tag);

            if (navigationManager.IsValidAuth())
            {
                switch (menuTag)
                {
                    case MenuViewTokens.PRODUCTS_MAIN:
                        navigationService.Navigate(PageTokens.PRODUCT_MAIN, null);
                        break;
                    case MenuViewTokens.CUSTOMERS_MAIN:
                        break;
                    case MenuViewTokens.ORDERS_MAIN:
                        break;
                    case MenuViewTokens.SALES_MAIN:
                        navigationService.Navigate(PageTokens.SALES_MAIN, null);
                        break;
                    case MenuViewTokens.CUSTOMERS_REPORT:
                        break;
                    case MenuViewTokens.ORDERS_REPORT:
                        break;
                    case MenuViewTokens.SALES_REPORT:
                        break;
                    case MenuViewTokens.USER_REGISTER:
                        navigationService.Navigate(PageTokens.USER_REGISTER, null);
                        break;
                    case MenuViewTokens.LOGIN:
                        navigationService.Navigate(PageTokens.LOGIN, new AuthDto.Payload());
                        break;                
                    case MenuViewTokens.LOGOUT:
                        navigationManager.Logoff();
                        break;
                    case MenuViewTokens.MANAGE_USERS:

                            navigationService.Navigate(PageTokens.USERS_MANAGEMENT, null);
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
