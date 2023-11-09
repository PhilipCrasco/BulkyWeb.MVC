using Bulky_WebRazor.Context;
using Bulky_WebRazor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bulky_WebRazor.Pages.Categories
{
    [BindProperties]
    public class EditModel : PageModel
    {
        private readonly DataContext _context;
        public Category Category { get; set; }

        public EditModel(DataContext context)
        {
            _context = context;
        }

        public void OnGet(int? id)
        {
            Category = _context.Categories.Find(id);
        }

        public IActionResult OnPost()
        {
            if (Category.Name == Category.Display_Order.ToString())  //Custom modelstate validation
            {
                ModelState.AddModelError("Category.Name", "The DisplayOrder cannot be exactly match the Name");
            }

            if (ModelState.IsValid)
            {

                _context.Update(Category);
                TempData["success"] = "Category update successfully";
                _context.SaveChanges();
                return RedirectToPage("Index");

            }

            return Page();
        }


    }
}
