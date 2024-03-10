using AbbyWeb.Data;
using AbbyWeb.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AbbyWeb.Pages
{
    public class Create1Model : PageModel
    {
        private readonly ApplicationDbContext _db;
        public Category Category { get; set; }
        public Create1Model(ApplicationDbContext db)
        {
            _db = db;
        }
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPost(Category category)
        {
           await _db.Category.AddAsync(category);
            await _db.SaveChangesAsync();
            return RedirectToPage("Categories/Index");
        }
    }
}
