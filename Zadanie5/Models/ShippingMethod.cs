using System;
using System.Collections.Generic;

namespace Zadanie5.Models
{
    public partial class ShippingMethod
    {
        public ShippingMethod()
        {
            CustOrders = new HashSet<CustOrder>();
        }

        public long MethodId { get; set; }
        public string? MethodName { get; set; }
        public double? Cost { get; set; }

        public virtual ICollection<CustOrder> CustOrders { get; set; }
    }
}
