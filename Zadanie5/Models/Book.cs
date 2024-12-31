using System;
using System.Collections.Generic;

namespace Zadanie5.Models
{
    public partial class Book
    {
        public Book()
        {
            OrderLines = new HashSet<OrderLine>();
            Authors = new HashSet<Author>();
        }

        public long BookId { get; set; }
        public string? Title { get; set; }
        public string? Isbn13 { get; set; }
        public long? LanguageId { get; set; }
        public long? NumPages { get; set; }
        public byte[]? PublicationDate { get; set; }
        public long? PublisherId { get; set; }

        public virtual BookLanguage? Language { get; set; }
        public virtual Publisher? Publisher { get; set; }
        public virtual ICollection<OrderLine> OrderLines { get; set; }

        public virtual ICollection<Author> Authors { get; set; }
    }
}
