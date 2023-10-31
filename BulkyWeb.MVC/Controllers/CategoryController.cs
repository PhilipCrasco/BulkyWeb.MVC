using BulkyWeb.MVC.Context;
using BulkyWeb.MVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyWeb.MVC.Controllers
{
    public class CategoryController : Controller
    {

        private readonly DataContext _context;
        public CategoryController(DataContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<Category> categories = _context.Categories.ToList();
            return View(categories);
        }

        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Create(Category category)
        {

            var categories = new Category
            {
                Name = category.Name,
                Display_Order = category.Display_Order

            };

            _context.Add(categories);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

    }
}
