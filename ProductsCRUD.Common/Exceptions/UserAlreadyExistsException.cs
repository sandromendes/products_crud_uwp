using System;

namespace ProductsCRUD.Common.Exceptions
{
    [Serializable]
    public class UserAlreadyExistsException : Exception
    {
        public UserAlreadyExistsException(string message) : base(message)
        {
        }
    }
}
