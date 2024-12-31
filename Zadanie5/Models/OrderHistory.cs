using System;
using System.Collections.Generic;

namespace Zadanie5.Models
{
    public partial class OrderHistory
    {
        public long HistoryId { get; set; }
        public long? OrderId { get; set; }
        public long? StatusId { get; set; }
        public byte[]? StatusDate { get; set; }

        public virtual CustOrder? Order { get; set; }
        public virtual OrderStatus? Status { get; set; }
    }
}
