using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CoreBookStore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;

namespace CoreBookStore.Controllers
{
    [Authorize]
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
        public ActionResult Create()
        {
            ViewBag.IdCategory = new SelectList(dbContext.Categories, "Id", "Description");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("Title", "IdCategory")] Book book)
        {
            if (ModelState.IsValid)
            {
                dbContext.Books.Add(book);
                dbContext.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(book);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            Book book = dbContext.Books.SingleOrDefault(b => b.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            ViewBag.IdCategory = new SelectList(dbContext.Categories, "Id", "Description", book.IdCategory);
            return View(book);
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

        public ActionResult Delete(int id)
        {
            Book book = dbContext.Books.SingleOrDefault(b => b.Id == id);
            if (book == null)
            {
                return NotFound();
            }
            
            return View(book);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmado([Bind("Id", "Title")] Book book)
        {
            if (ModelState.IsValid)
            {
                dbContext.Books.Remove(book);
                dbContext.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(book);
        }
    }
}
