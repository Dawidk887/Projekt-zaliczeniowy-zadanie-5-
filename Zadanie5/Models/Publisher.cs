using System;
using System.Collections.Generic;

namespace Zadanie5.Models
{
    public partial class Publisher
    {
        public Publisher()
        {
            Books = new HashSet<Book>();
        }

        public long PublisherId { get; set; }
        public string? PublisherName { get; set; }

        public virtual ICollection<Book> Books { get; set; }
    }
}
