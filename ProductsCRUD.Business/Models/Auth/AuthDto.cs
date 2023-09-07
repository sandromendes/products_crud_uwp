namespace ProductsCRUD.Business.Models.Auth
{
    public class AuthDto
    {
        public class Payload
        {
            private bool _isLogoutRequired = false;

            public bool IsLogoutRequired
            {
                get { return _isLogoutRequired; }
                set { _isLogoutRequired = value; }
            }
        }
    }
}
