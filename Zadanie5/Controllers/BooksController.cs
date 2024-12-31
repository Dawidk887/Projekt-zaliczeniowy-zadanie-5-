using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Zadanie5.Data;
using Zadanie5.Models;
using Zadanie5.ViewModels;

namespace Zadanie5.Controllers
{
    public class BooksController : Controller
    {
        private readonly gravityContext _context;

        public BooksController(gravityContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int page = 1, int pageSize = 15)
        {
            var totalRecords = await _context.Books.CountAsync();
            var totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);

            if (page < 1) page = 1;
            if (page > totalPages) page = totalPages;

            var books = await _context.Books
                .Include(b => b.Authors)
                .Select(b => new BookListViewModel
                {
                    BookId = ((int)b.BookId),
                    Title = b.Title,
                    Isbn13 = b.Isbn13,
                    NumPages = ((int)b.NumPages),
                    PublicationDateFormatted = b.PublicationDate != null && b.PublicationDate.Length > 0
                        ? DateTime.Parse(System.Text.Encoding.UTF8.GetString(b.PublicationDate))
                        : (DateTime?)null,
                    AuthorCount = b.Authors.Count,
                    SoldCopies = _context.OrderLines.Count(ol => ol.BookId == b.BookId)
                })
                .OrderBy(b => b.Title)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;

            return View(books);
        }


        public IActionResult Create()
        {
            var publishers = _context.Publishers.ToList();
            var authors = _context.Authors.ToList();
            var book = _context.Books;

            var viewModel = new AddBookViewModel
            {
                AuthorIds = new List<int>(),
            };

            ViewBag.Publishers = new SelectList(publishers, "PublisherId", "PublisherName");
            ViewBag.Authors = new MultiSelectList(authors, "AuthorId", "AuthorName");

            return View(viewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AddBookViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var maxBookId = await _context.Books.MaxAsync(b => (long?)b.BookId) ?? 0;

                var book = new Book
                {
                    BookId = maxBookId + 1,
                    Title = viewModel.Title,
                    Isbn13 = viewModel.Isbn13,
                    NumPages = viewModel.NumPages,
                    PublisherId = viewModel.PublisherId,
                };

                _context.Books.Add(book);
                await _context.SaveChangesAsync();

                if (viewModel.AuthorIds != null && viewModel.AuthorIds.Any())
                {
                    foreach (var authorId in viewModel.AuthorIds)
                    {
                        var author = await _context.Authors
                            .Include(a => a.Books) 
                            .FirstOrDefaultAsync(a => a.AuthorId == authorId);

                        if (author != null)
                        {
                            author.Books.Add(book);
                        }
                    }

                    await _context.SaveChangesAsync();
                }

                return RedirectToAction(nameof(Index));
            }

            var publishers = _context.Publishers.ToList();
            var authors = _context.Authors.ToList();
            ViewBag.Publishers = new SelectList(publishers, "PublisherId", "PublisherName");
            ViewBag.Authors = new MultiSelectList(authors, "AuthorId", "AuthorName");

            return View(viewModel);
        }


        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books
                .Include(b => b.Authors)
                .FirstOrDefaultAsync(b => b.BookId == id);

            if (book == null)
            {
                return NotFound();
            }

            var viewModel = new EditBookViewModel
            {
                BookId = book.BookId,
                Title = book.Title,
                Isbn13 = book.Isbn13,
                NumPages = (int?)book.NumPages,
                PublicationDate = book.PublicationDate != null
                    ? DateTime.Parse(System.Text.Encoding.UTF8.GetString(book.PublicationDate))
                    : null,
                PublisherId = (int?)book.PublisherId,
                AuthorIds = book.Authors.Select(a => (int)a.AuthorId).ToList()
            };

            ViewBag.Publishers = new SelectList(_context.Publishers, "PublisherId", "PublisherName", viewModel.PublisherId);
            ViewBag.Authors = new MultiSelectList(_context.Authors, "AuthorId", "AuthorName", viewModel.AuthorIds);
            return View(viewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditBookViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Publishers = new SelectList(_context.Publishers, "PublisherId", "PublisherName", viewModel.PublisherId);
                ViewBag.Authors = new MultiSelectList(_context.Authors, "AuthorId", "AuthorName", viewModel.AuthorIds);
                return View(viewModel);
            }

            var book = await _context.Books
                .Include(b => b.Authors)
                .FirstOrDefaultAsync(b => b.BookId == viewModel.BookId);

            if (book == null)
            {
                return NotFound();
            }

            book.Title = viewModel.Title;
            book.Isbn13 = viewModel.Isbn13;
            book.NumPages = viewModel.NumPages;
            book.PublicationDate = viewModel.PublicationDate.HasValue
                ? System.Text.Encoding.UTF8.GetBytes(viewModel.PublicationDate.Value.ToString("yyyy-MM-dd"))
                : null;
            book.PublisherId = viewModel.PublisherId;

            book.Authors.Clear();
            if (viewModel.AuthorIds != null)
            {
                var authors = await _context.Authors.Where(a => viewModel.AuthorIds.Contains((int)a.AuthorId)).ToListAsync();
                foreach (var author in authors)
                {
                    book.Authors.Add(author);
                }
            }

            _context.Update(book);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Authors(long id)
        {
            var book = await _context.Books
                .Include(b => b.Authors)
                .FirstOrDefaultAsync(b => b.BookId == id);

            if (book == null)
            {
                return NotFound();
            }

            var authors = book.Authors.Select(a => new AuthorViewModel
            {
                AuthorId = a.AuthorId,
                AuthorName = a.AuthorName
            }).ToList();

            return View(authors);
        }


        private bool BookExists(int id)
        {
            return _context.Books.Any(e => e.BookId == id);
        }
    }
}
