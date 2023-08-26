using ProductsCRUD.ViewModels;
using Windows.UI.Xaml.Controls;

namespace ProductsCRUD.Views
{
    public sealed partial class RegisterUserPage : Page
    {
        public RegisterUserPageViewModel ViewModel => (RegisterUserPageViewModel)DataContext;
        public RegisterUserPage()
        {
            this.InitializeComponent();
        }
    }
}
