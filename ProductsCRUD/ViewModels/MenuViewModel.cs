using Prism.Logging;
using Prism.Windows.Mvvm;
using Prism.Windows.Navigation;
using ProductsCRUD.Models;
using ProductsCRUD.Util;
using System;
using System.Collections.ObjectModel;
using Windows.UI.Xaml.Controls;

namespace ProductsCRUD.ViewModels
{
    public class MenuViewModel : ViewModelBase
    {
        private readonly INavigationService navigationService;

        public MenuViewModel(INavigationService navigationService)
        {
            this.navigationService = navigationService;
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
                default:
                    break;
            }
        }
    }
}
