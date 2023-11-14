using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models.Model;
using Bulky.Models.ViewModel;
using BulkyWeb.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

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
            IEnumerable<SelectListItem> CategoryList = _unitofwork.Category.GetAll()
                .Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString(),

                });

            return View(product);


        }

        public IActionResult Upsert(int ? id)
        {
            //IEnumerable<SelectListItem> CategoryList = _unitofwork.Category.GetAll() // for viewbag set up
            //  .Select(x => new SelectListItem
            //  {
            //      Text = x.Name,
            //      Value = x.Id.ToString(),

            //  });

            //ViewBag.CategoryList = CategoryList;

            ProductVM productVM = new()
            {
                CategoryList = _unitofwork.Category.GetAll() // MVVM setup
                .Select(x => new SelectListItem 
                {
                 Text = x.Name,
                 Value = x.Id.ToString(),   
               
                }),
                Product = new Product()

            };

            if(id == null || id == 0)
            {
                return View(productVM);

            }
            else
            {
                productVM.Product = _unitofwork.Products.Get(x => x.Id == id);
                return View(productVM);
            }

          
        }


        [HttpPost]
        public IActionResult Upsert(ProductVM productVM , IFormFile? file)
        {


            if (ModelState.IsValid)
            {

                _unitofwork.Products.Add(productVM.Product);
                _unitofwork.Save();
                TempData["success"] = "Products created successfully";   
                 return  RedirectToAction("Index");

            }
            else
            {
              
                productVM.CategoryList = _unitofwork.Category.GetAll() // MVVM setup
               .Select(x => new SelectListItem
               {
                   Text = x.Name,
                   Value = x.Id.ToString(),

               });

                return View(productVM);
            }

           
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
