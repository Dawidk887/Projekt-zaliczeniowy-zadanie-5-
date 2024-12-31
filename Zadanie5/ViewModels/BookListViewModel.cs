namespace Zadanie5.ViewModels
{
    public class BookListViewModel
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public string Isbn13 { get; set; }
        public int NumPages { get; set; }
        public DateTime? PublicationDateFormatted { get; set; }
        public int AuthorCount { get; set; }
        public int? SoldCopies { get; set; } 
    }
}
