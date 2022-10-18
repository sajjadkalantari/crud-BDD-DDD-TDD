using System;
namespace Mc2.CrudTest.Presentation.Infrustructure.Exceptions
{
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException(string message) : base(message)
        {
        }
    }
}