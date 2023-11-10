using Bulky.DataAccess.Repository.IRepository;
using BulkyWeb.DataAccess.Context;
using BulkyWeb.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BulkyWeb.MVC.Controllers
{
    public class CategoryController : Controller
    {

        private readonly ICategoryRepository _categoryRepository;
        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public IActionResult Index()
        {
            List<Category> categories = _categoryRepository.GetAll().ToList();
            return View(categories);
        }

        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Create(Category category)
        {

            if(category.Name == category.Display_Order.ToString())  //Custom modelstate validation
            {
                ModelState.AddModelError("Name", "The DisplayOrder cannot be exactly match the Name");
            }

            if(ModelState.IsValid)
            {
                var categories = new Category
                {
                    Name = category.Name,
                    Display_Order = category.Display_Order

                };

                _categoryRepository.Add(categories);
                _categoryRepository.Save();
                TempData["success"] = "Category created successfully";
                return RedirectToAction("Index");
                     
            }

            return View(category);
        }

                                             
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var category = _categoryRepository.Get(x => x.Id == id);


            return View(category);
        }


        [HttpPost]
        public IActionResult Edit(Category category)
        {
            if (category.Name == category.Display_Order.ToString())  //Custom modelstate validation
            {
                ModelState.AddModelError("Name", "The DisplayOrder cannot be exactly match the Name");
            }

            if (ModelState.IsValid)
            {


                _categoryRepository.Update(category);
                TempData["success"] = "Category update successfully";
                _categoryRepository.Save();
                return RedirectToAction("Index");

            }

            return View(category);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var category = _categoryRepository.Get(x => x.Id == id);


            return View(category);
        }


        [HttpPost , ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {

            var category = _categoryRepository.Get(x => x.Id == id);

            _categoryRepository.Remove(category);
            TempData["success"] = "Category delete successfully";
            _categoryRepository.Save();
            return RedirectToAction("Index");


        }

    }
}
