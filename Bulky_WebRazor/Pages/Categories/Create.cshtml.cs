using Bulky_WebRazor.Context;
using Bulky_WebRazor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bulky_WebRazor.Pages.Categories
{
    [BindProperties]
    public class CreateModel : PageModel
    {
        
        private readonly DataContext _context;

        public Category Category { get; set; }

        public CreateModel(DataContext context)
        {
            _context = context;
        }


        public void OnGet()
        {
        }

        public IActionResult OnPost(Category category)
        {
            var categories = new Category
            {
                Name = category.Name,
                Display_Order = category.Display_Order
            };

            _context.Categories.Add(categories);
            _context.SaveChanges();

            return RedirectToPage("Index");

        }

    }
}
