using Prism.Windows.Mvvm;
using Prism.Windows.Navigation;
using ProductsCRUD.Common.Util;

namespace ProductsCRUD.ViewModels
{
    public class HomePageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;

        public HomePageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public void NavigateToProductsPage()
        {
            _navigationService.Navigate(PageTokens.ProductsPage.MAIN, null);
        }

        public void NavigateToClientsPage()
        {
            _navigationService.Navigate(PageTokens.CustomersPage.MAIN, null);
        }

        public void NavigateToOrdersPage()
        {
            _navigationService.Navigate(PageTokens.OrdersPage.MAIN, null);
        }

        public void NavigateToSalesPage()
        {
            _navigationService.Navigate(PageTokens.SalesPage.MAIN, null);
        }
    }
}
