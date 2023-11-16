using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models.Model;
using Bulky.Models.ViewModel;
using BulkyWeb.MVC.Models;
using Ganss.Xss;
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

                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if(file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    var productPath = Path.Combine(wwwRootPath, @"images\product");

                    if(!string.IsNullOrEmpty (productVM.Product.ImageUrl))
                    {
                        var oldimagePath = 
                            Path.Combine(wwwRootPath, productVM.Product.ImageUrl.TrimStart('\\'));

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

                var sanitizer = new HtmlSanitizer();
                productVM.Product.Description = sanitizer.Sanitize(productVM.Product.Description);

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



        #region ApiCalls 

        [HttpGet]
        public IActionResult GetAll()
        {
            List<Product> product = _unitofwork.Products.GetAll(includeProperties: "Category").ToList();
            return Json(new {data = product});
        }


        [HttpDelete]
        public IActionResult Delete(int ? id)
        {

            var productTobeDeleted = _unitofwork.Products.Get(x => x.Id == id);
            if (productTobeDeleted == null)
            {
                return Json(new { succes = false, message = "Error while deleting" });
            }

            var oldimagePath = Path.Combine(_webHostEnvironment.WebRootPath, productTobeDeleted.ImageUrl.TrimStart('\\'));

            if (System.IO.File.Exists(oldimagePath))
            {
                System.IO.File.Delete(oldimagePath);
            }

            _unitofwork.Products.Remove(productTobeDeleted);
            _unitofwork.Save();

            return Json(new { success = true , message = "Delete Successful" });
        }

        #endregion


    }
}
