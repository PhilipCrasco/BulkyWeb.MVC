using Bulky.DataAccess.Repository.IRepository;
using Bulky.Utility;
using BulkyWeb.DataAccess.Context;
using BulkyWeb.MVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BulkyWeb.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class CategoryController : Controller
    {

        private readonly IUnitofWork _unitofwork;
        public CategoryController(IUnitofWork unitofwork)
        {
            _unitofwork = unitofwork;
        }
        public IActionResult Index()
        {
            List<Category> categories = _unitofwork.Category.GetAll().ToList();
            return View(categories);
        }

        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Create(Category category)
        {

            if (category.Name == category.Display_Order.ToString())  //Custom modelstate validation
            {
                ModelState.AddModelError("Name", "The DisplayOrder cannot be exactly match the Name");
            }

            if (ModelState.IsValid)
            {
                var categories = new Category
                {
                    Name = category.Name,
                    Display_Order = category.Display_Order

                };

                _unitofwork.Category.Add(categories);
                _unitofwork.Save();
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

            var category = _unitofwork.Category.Get(x => x.Id == id);


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


                _unitofwork.Category.Update(category);
                TempData["success"] = "Category update successfully";
                _unitofwork.Save();
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

            var category = _unitofwork.Category.Get(x => x.Id == id);


            return View(category);
        }


        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {

            var category = _unitofwork.Category.Get(x => x.Id == id);

            _unitofwork.Category.Remove(category);
            TempData["success"] = "Category delete successfully";
            _unitofwork.Save();
            return RedirectToAction("Index");


        }

    }
}
