using System;

namespace LibraryApi.Exceptions
{
    public class BookTakenException : Exception
    {
        public BookTakenException(string message) : base(message)
        {

        }
    }
}
