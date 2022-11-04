using System;

namespace Mc2.CrudTest.Shared.Exceptions
{
    public class CustomSystemException : Exception
    {
        public int Code { get; set; }
        public CustomSystemException(int code, string message) : base(message)
        {
            this.Code = code;
        }
    }

}
