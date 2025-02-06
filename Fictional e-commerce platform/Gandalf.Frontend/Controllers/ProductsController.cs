using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Gandalf.Backend;
using Gandalf.Backend.Models;
using Gandalf.Backend.Services;

namespace Gandalf.Frontend.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ProductService productService;
        private readonly CategoryService categoryService;
        
        public ProductsController(ProductService productService, CategoryService categoryService)
        {
            this.productService = productService;
            this.categoryService = categoryService;
        }
        // Get: Products
        public IActionResult Index()
        {
            return View(productService.GetProducts());
        }
        // GET: Products/ByCategory/{id}
        public IActionResult ByCategory(int id)
        {
            var products = productService.GetProducts().Where(p => p.CategoryId == id).ToList();

            ViewBag.Category = categoryService.GetCategories()
                .FirstOrDefault(c => c.CategoryId == id)?.Name;

            return View(products);
        }
        // Get: Products/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = productService.GetProduct(id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }
        //Get: Products/Create
        public IActionResult Create()
        {
            ViewBag.Categories = new SelectList(categoryService.GetCategories(), "CategoryId", "Name");
            return View();
        }
        // Post: Products/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("ProductId,Name,Price,Description,CategoryId")] Product product)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = new SelectList(categoryService.GetCategories(), "CategoryId", "Name", product.CategoryId);
                return View(product);
            }
            if (ModelState.IsValid)
            {
                productService.CreateProduct(product.Name,product.Image, product.Price, product.Description, product.CategoryId);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Categories = new SelectList(categoryService.GetCategories(), "CategoryId", "Name", product.CategoryId);
            return View(product);
        }
        //Get: Product/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = productService.GetProduct(id);

            if (product == null)
            {
                return NotFound();
            }
            ViewBag.Categories = new SelectList(categoryService.GetCategories(), "CategoryId", "Name", product.CategoryId);
            return View(product);
        }
        //Post: Product/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, [Bind("ProductId,Name,Price,Description,CategoryId")] Product product)
        {
            if (id != product.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                productService.Update(product);

                return RedirectToAction(nameof(Index));
            }
            ViewBag.Categories = new SelectList(categoryService.GetCategories(), "CategoryId", "Name", product.CategoryId);
            return View(product);
        }
        // GET: Products/Delete/5
        public IActionResult Delete(int? id)
        {
            var product = productService.GetProduct(id);

            return View(product);
        }
        // Post: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var product = productService.DeleteProduct(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
