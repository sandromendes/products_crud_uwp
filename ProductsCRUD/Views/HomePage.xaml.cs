using ProductsCRUD.ViewModels;
using Windows.UI.Xaml.Controls;

namespace ProductsCRUD.Views
{
    public sealed partial class HomePage : Page
    {
        public HomePageViewModel ViewModel => (HomePageViewModel)DataContext;
        public HomePage()
        {
            this.InitializeComponent();
        }
    }
}
