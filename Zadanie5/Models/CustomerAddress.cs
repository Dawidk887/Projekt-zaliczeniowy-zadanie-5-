using System;
using System.Collections.Generic;

namespace Zadanie5.Models
{
    public partial class CustomerAddress
    {
        public long CustomerId { get; set; }
        public long AddressId { get; set; }
        public long? StatusId { get; set; }

        public virtual Address Address { get; set; } = null!;
        public virtual Customer Customer { get; set; } = null!;
    }
}
