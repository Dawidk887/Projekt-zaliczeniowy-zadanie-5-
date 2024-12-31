using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Zadanie5.Data;
using Zadanie5.Models;
using Zadanie5.ViewModels;

namespace Zadanie5.Controllers
{
    public class AuthorsController : Controller
    {
        private readonly gravityContext _context;

        public AuthorsController(gravityContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int page = 1, int pageSize = 15)
        {
            var totalAuthors = await _context.Authors.CountAsync();
            var totalPages = (int)Math.Ceiling(totalAuthors / (double)pageSize);

            if (page < 1)
                page = 1;
            if (page > totalPages)
                page = totalPages;

            var authors = await _context.Authors
                .OrderBy(a => a.AuthorName)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(a => new AuthorWithBooksViewModel
                {
                    AuthorId = a.AuthorId,
                    AuthorName = a.AuthorName,
                    Books = a.Books.Select(b => new BookViewModel
                    {
                        BookId = b.BookId,
                        Title = b.Title
                    }).ToList()
                })
                .ToListAsync();

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;

            return View(authors);
        }

    }

}
