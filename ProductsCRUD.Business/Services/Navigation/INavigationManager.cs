namespace ProductsCRUD.Business.Services.Navigation
{
    public interface INavigationManager
    {
        void Logoff();
        bool IsValidAuth();
    }
}