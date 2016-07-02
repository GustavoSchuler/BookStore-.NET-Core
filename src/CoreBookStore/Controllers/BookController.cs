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
    }
}
