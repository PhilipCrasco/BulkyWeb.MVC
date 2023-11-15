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
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductsController(IUnitofWork unitofwork, IWebHostEnvironment webHostEnvironment)
        {
            _unitofwork = unitofwork;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
 
            List<Product> product = _unitofwork.Products.GetAll(includeProperties: "Category").ToList();
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

                string wwwRoothPath = _webHostEnvironment.WebRootPath;
                if(file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    var productPath = Path.Combine(wwwRoothPath, @"images\product");

                    if(!string.IsNullOrEmpty (productVM.Product.ImageUrl))
                    {
                        var oldimagePath = 
                            Path.Combine(wwwRoothPath, productVM.Product.ImageUrl.TrimStart('\\'));

                        if(System.IO.File.Exists (oldimagePath))
                        {
                            System.IO.File.Delete (oldimagePath);
                        }
                    }

                    using (var fileStream = new FileStream(Path.Combine(productPath , fileName),FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    productVM.Product.ImageUrl = @"\images\product\" + fileName;
                }

                if(productVM.Product.Id == 0)
                {
                    _unitofwork.Products.Add(productVM.Product);
                }
                else
                {
                   _unitofwork.Products.Update(productVM.Product);
                }

                _unitofwork.Save();
                TempData["success"] = "Transaction created/updates successfully";   
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
