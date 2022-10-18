using System;
namespace Mc2.CrudTest.Presentation.Infrustructure.Exceptions
{
    public class DuplicateEntityException : Exception
    {
        public DuplicateEntityException(string message) : base(message)
        {
        }
    }
}