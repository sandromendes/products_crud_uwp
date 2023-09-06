namespace ProductsCRUD.Services.Navigation
{
    public interface INavigationManager
    {
        void Logoff();
        bool IsValidAuth();
    }
}