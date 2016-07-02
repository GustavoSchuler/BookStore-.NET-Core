using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CoreBookStore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CoreBookStore.Controllers
{
    [Authorize]
    public class CategoryController : Controller
    {
        private WebsiteDbContext dbContext;

        public CategoryController(WebsiteDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            var categories = await dbContext.Categories.OrderByDescending(b => b.Id)
                .Take(10)
                .ToArrayAsync();

            return View(new CategoryViewModel { Categories = categories });
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("Description")] Category category)
        {
            if (ModelState.IsValid)
            {
                dbContext.Categories.Add(category);
                dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            
            return View(category);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            Category category = dbContext.Categories.SingleOrDefault(b => b.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            ViewBag.IdCategory = new SelectList(dbContext.Categories, "Description");
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind("Id", "Description")] Category category)
        {
            if (ModelState.IsValid)
            {
                dbContext.Categories.Update(category);
                dbContext.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(category);
        }

        public ActionResult Delete(int id)
        {
            Category category = dbContext.Categories.SingleOrDefault(b => b.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            
            return View(category);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmado([Bind("Id", "Description")] Category category)
        {
            if (ModelState.IsValid)
            {
                dbContext.Categories.Remove(category);
                dbContext.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(category);
        }
    }
}
