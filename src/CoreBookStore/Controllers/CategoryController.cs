﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CoreBookStore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

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

        public ActionResult Edit(int id)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Category category)
        {
            return View();
        }

        public ActionResult Delete(int id)
        {
            return View();
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmado(int id)
        {
            return View();
        }
    }
}
