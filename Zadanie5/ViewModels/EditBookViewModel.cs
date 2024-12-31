namespace Zadanie5.ViewModels
{
    public class EditBookViewModel
    {
        public long BookId { get; set; }
        public string Title { get; set; }
        public string? Isbn13 { get; set; }
        public int? NumPages { get; set; }
        public DateTime? PublicationDate { get; set; }
        public int? PublisherId { get; set; }
        public List<int>? AuthorIds { get; set; } 
    }

}
