using EntityFrameworkProject.Data;
using EntityFrameworkProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EntityFrameworkProject.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    public class BookController : Controller
    {
        private readonly AppDbContext _context;

        public BookController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var book = _context.Books.Include(m => m.BookTags).ThenInclude(x => x.Book).ToList();
            return View(book);
        }

    
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Tags = _context.Tags.ToList();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public  IActionResult Create(Book book)
        {
            CheckTags(book);
            if (!ModelState.IsValid)
            {
                ViewBag.Tags = _context.Tags.ToList();
            }
            if (book.TagIds != null)
            {
                foreach (var tagid in book.TagIds)
                {
                    BookTag bookTag = new BookTag
                    {
                        TagId = tagid
                    };
                }
            }
            _context.Books.Add(book);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Edit(int id)
        {
            Book existbook = await _context.Books.Include(m => m.BookTags).FirstOrDefaultAsync(x => x.Id == id);
            if (existbook == null)
            {
                return RedirectToAction("Dashboard");
            }
            ViewBag.Tags = await _context.Tags.ToListAsync();
            existbook.TagIds =  existbook.BookTags.Select(m => m.TagId).ToList();
            return View(existbook);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Book book)
        {
            Book existbook = await _context.Books.Include(m => m.BookTags).FirstOrDefaultAsync(y => y.Id == book.Id);
            if (existbook == null)
            {
                return RedirectToAction("error", "dashboard");
            }
            CheckTags(book);
            if (ModelState.IsValid)
            {
                ViewBag.Tags = await _context.Tags.ToListAsync();
                return View();
            }
            existbook.BookTags.RemoveAll(b => !book.TagIds.Contains(b.TagId));
            foreach (var tagid in book.TagIds.Where(x=>!existbook.BookTags.Any(b=>b.TagId == x)))
            {
                BookTag bookTag = new BookTag
                {
                    TagId = tagid
                };
                existbook.BookTags.Add(bookTag);
            }
            existbook.Name = book.Name;
            existbook.Description = book.Description;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }












        private void CheckTags(Book book)
        {
            if (book.TagIds != null)
            {
                foreach (var tagid in book.TagIds)
                {
                    if (_context.Tags.Any(t => t.Id == tagid))
                    {
                        ModelState.AddModelError("TagIds", "Tag not found");
                        return;
                    }
                }
            }
        }

    }
}
