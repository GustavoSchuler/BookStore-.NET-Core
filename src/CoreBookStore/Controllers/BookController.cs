using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CoreBookStore.Models;
using Microsoft.EntityFrameworkCore;

namespace CoreBookStore.Controllers
{
    public class BookController : Controller
    {
        private WebsiteDbContext dbContext;

        public BookController(WebsiteDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            var books = await dbContext.Books.OrderByDescending(b => b.Id)
                .Take(10)
                .ToArrayAsync();

            return View(new BookViewModel { Books = books });
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            var books = await dbContext.Books.Where(b => b.Id == id)
                .Take(1)
                .ToArrayAsync();

            return View(new BookViewModel { Books = books });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind("Id", "Title", "IdCategory")] Book book)
        {
            if (ModelState.IsValid)
            {
                dbContext.Books.Update(book);
                dbContext.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(book);
        }
    }
}
