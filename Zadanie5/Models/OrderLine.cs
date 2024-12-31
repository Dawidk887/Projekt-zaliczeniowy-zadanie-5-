using System;
using System.Collections.Generic;

namespace Zadanie5.Models
{
    public partial class OrderLine
    {
        public long LineId { get; set; }
        public long? OrderId { get; set; }
        public long? BookId { get; set; }
        public double? Price { get; set; }

        public virtual Book? Book { get; set; }
        public virtual CustOrder? Order { get; set; }
    }
}
