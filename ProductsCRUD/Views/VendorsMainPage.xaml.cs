using ProductsCRUD.ViewModels;
using Windows.UI.Xaml.Controls;

namespace ProductsCRUD.Views
{
    public sealed partial class VendorsMainPage : Page
    {
        VendorsMainPageViewModel ViewModel => (VendorsMainPageViewModel)DataContext;

        public VendorsMainPage()
        {
            this.InitializeComponent();
        }
    }
}
