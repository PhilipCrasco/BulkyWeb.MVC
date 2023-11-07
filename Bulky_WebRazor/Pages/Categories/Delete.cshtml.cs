using Bulky_WebRazor.Context;
using Bulky_WebRazor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Bulky_WebRazor.Pages.Categories
{

    [BindProperties]
    public class DeleteModel : PageModel
    {
        private readonly DataContext _context;

        public Category Category { get; set; }

        public DeleteModel(DataContext context)
        {
            _context = context;
        }

        public void OnGet(int ? id)
        {
              Category = _context.Categories.Find(id);
        }

        public  IActionResult OnPost ()
        {
         
         
                var categories = _context.Categories.FirstOrDefault(x => x.Id == Category.Id);

                _context.Remove(categories);
                _context.SaveChangesAsync();
                return RedirectToPage("Index");
            
      

        }


    }
}
