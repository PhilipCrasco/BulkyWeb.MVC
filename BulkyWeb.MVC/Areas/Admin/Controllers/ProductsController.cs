using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models.Model;
using BulkyWeb.MVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyWeb.MVC.Areas.Admin.Controllers
{

    [Area("Admin")]
    public class ProductsController : Controller
    {
      
        private readonly IUnitofWork _unitofwork;
        public ProductsController(IUnitofWork unitofwork)
        {
            _unitofwork = unitofwork;
        }
        public IActionResult Index()
        {
 
            List<Product> product = _unitofwork.Products.GetAll().ToList();
            return View(product);


        }

        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Create(Product product)
        {


            if (ModelState.IsValid)
            {
                var products = new Product
                {
                    Title = product.Title,
                    Description = product.Description,
                    ISBN = product.ISBN,
                    Author = product.Author,
                    ListPrice = product.ListPrice,
                    Price = product.Price,
                    Price50 = product.Price50,
                    Price500 = product.Price50,


                };

                _unitofwork.Products.Add(products);
                _unitofwork.Save();
                TempData["success"] = "Products created successfully";   
                 return  RedirectToAction("Index");

            }

            return View(product);
        }


        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var product = _unitofwork.Products.Get(x => x.Id == id);


            return View(product);
        }


        [HttpPost]
        public IActionResult Edit(Product product)
        {
          
            if (ModelState.IsValid)
            {


                _unitofwork.Products.Update(product);
                TempData["success"] = "Products update successfully";
                _unitofwork.Save();
                return RedirectToAction("Index");

            }

            return View(product);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var product = _unitofwork.Products.Get(x => x.Id == id);


            return View(product);
        }


        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {

            var product = _unitofwork.Products.Get(x => x.Id == id);

            _unitofwork.Products.Remove(product);
            TempData["success"] = "Products delete successfully";
            _unitofwork.Save();
            return RedirectToAction("Index");

        }


    }
}
