using System;
using System.Collections.Generic;

namespace Zadanie5.Models
{
    public partial class Address
    {
        public Address()
        {
            CustOrders = new HashSet<CustOrder>();
            CustomerAddresses = new HashSet<CustomerAddress>();
        }

        public long AddressId { get; set; }
        public string? StreetNumber { get; set; }
        public string? StreetName { get; set; }
        public string? City { get; set; }
        public long? CountryId { get; set; }

        public virtual Country? Country { get; set; }
        public virtual ICollection<CustOrder> CustOrders { get; set; }
        public virtual ICollection<CustomerAddress> CustomerAddresses { get; set; }
    }
}
