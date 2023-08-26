using ProductsCRUD.ViewModels;
using Windows.UI.Xaml.Controls;

namespace ProductsCRUD.Views
{
    public sealed partial class LoginPage : Page
    {
        public LoginPageViewModel ViewModel => (LoginPageViewModel)DataContext;
        public LoginPage()
        {
            this.InitializeComponent();
        }
    }
}
