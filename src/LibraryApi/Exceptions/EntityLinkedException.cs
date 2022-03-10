using System;

namespace LibraryApi.Exceptions
{
    public class EntityLinkedException : Exception
    {
        public EntityLinkedException(string message) : base(message)
        {

        }
    }
}
