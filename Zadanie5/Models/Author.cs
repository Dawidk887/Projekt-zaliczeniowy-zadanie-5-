using System;
using System.Collections.Generic;

namespace Zadanie5.Models
{
    public partial class Author
    {
        public Author()
        {
            Books = new HashSet<Book>();
        }

        public long AuthorId { get; set; }
        public string? AuthorName { get; set; }

        public virtual ICollection<Book> Books { get; set; }
    }
}
