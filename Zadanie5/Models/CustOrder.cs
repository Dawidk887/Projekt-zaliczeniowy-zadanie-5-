using System;
using System.Collections.Generic;

namespace Zadanie5.Models
{
    public partial class CustOrder
    {
        public CustOrder()
        {
            OrderHistories = new HashSet<OrderHistory>();
            OrderLines = new HashSet<OrderLine>();
        }

        public long OrderId { get; set; }
        public byte[]? OrderDate { get; set; }
        public long? CustomerId { get; set; }
        public long? ShippingMethodId { get; set; }
        public long? DestAddressId { get; set; }

        public virtual Customer? Customer { get; set; }
        public virtual Address? DestAddress { get; set; }
        public virtual ShippingMethod? ShippingMethod { get; set; }
        public virtual ICollection<OrderHistory> OrderHistories { get; set; }
        public virtual ICollection<OrderLine> OrderLines { get; set; }
    }
}
