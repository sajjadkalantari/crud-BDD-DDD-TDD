using Mc2.CrudTest.Presentation.Domain.AggregatesModel.CustomerAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mc2.CrudTest.Presentation.Domain.Events
{
    public class CustomerCreatedDomainEvent : INotification
    {
        public CustomerCreatedDomainEvent(Customer customer)
        {
            Customer = customer;
        }
        public Customer Customer { get; }

    }
}
