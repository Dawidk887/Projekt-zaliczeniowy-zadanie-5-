namespace Zadanie5.ViewModels
{
    public class AuthorWithBooksViewModel
    {
        public long AuthorId { get; set; }
        public string AuthorName { get; set; }
        public List<BookViewModel> Books { get; set; }
    }
}
