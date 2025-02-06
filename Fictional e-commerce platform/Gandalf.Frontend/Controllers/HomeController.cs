using System.Diagnostics;
using Gandalf.Backend.Services;
using Gandalf.Frontend.Models;
using Microsoft.AspNetCore.Mvc;

namespace Gandalf.Frontend.Controllers
{
    public class HomeController : Controller
    {
        private readonly CategoryService categoryService;

        public HomeController(CategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        public IActionResult Index()
        {
            ViewBag.Categories = categoryService.GetCategories();
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
