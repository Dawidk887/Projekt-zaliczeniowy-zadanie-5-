using System;
using System.Collections.Generic;

namespace Zadanie5.Models
{
    public partial class BookLanguage
    {
        public BookLanguage()
        {
            Books = new HashSet<Book>();
        }

        public long LanguageId { get; set; }
        public string? LanguageCode { get; set; }
        public string? LanguageName { get; set; }

        public virtual ICollection<Book> Books { get; set; }
    }
}
