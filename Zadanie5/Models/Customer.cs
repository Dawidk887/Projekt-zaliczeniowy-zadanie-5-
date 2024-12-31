using System;
using System.Collections.Generic;

namespace Zadanie5.Models
{
    public partial class Customer
    {
        public Customer()
        {
            CustOrders = new HashSet<CustOrder>();
            CustomerAddresses = new HashSet<CustomerAddress>();
        }

        public long CustomerId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }

        public virtual ICollection<CustOrder> CustOrders { get; set; }
        public virtual ICollection<CustomerAddress> CustomerAddresses { get; set; }
    }
}
