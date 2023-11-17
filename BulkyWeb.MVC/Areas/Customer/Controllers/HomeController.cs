using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models.Model;
using BulkyWeb.Models.Model;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BulkyWeb.MVC.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitofWork _unitofwork;

        public HomeController(ILogger<HomeController> logger , IUnitofWork unitofwork)
        {
            _unitofwork = unitofwork;
            _logger = logger;
        }

        public IActionResult Index()
        {
            IEnumerable<Product> product = _unitofwork.Products.GetAll(includeProperties: "Category").ToList();
            return View(product);
        }

        public IActionResult Details(int productId)
        {
            Product product = _unitofwork.Products.Get(x => x.Id == productId, includeProperties: "Category" );
            return View(product);
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