using AbbyWeb.Data;
using AbbyWeb.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace AbbyWeb.Pages
{
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public Category Category { get; set; }
        public DeleteModel(ApplicationDbContext db)
        {
            _db = db;
        }
        public void OnGet(int id)
        {
            Category=_db.Category.First(c => c.Id == id);
            //Category = _db.Category.FirstOrDefault(c => c.Id == id);
            //Category = _db.Category.SingleOrDefault(c => c.Id == id);//also use single
            //Category = _db.Category.Where(c => c.Id == id);

        }
        public async Task<IActionResult> OnPost(Category category)           
        {           
                _db.Category.Remove(category);
                await _db.SaveChangesAsync();
                return RedirectToPage("Categories/Index");          
            
        }
    }
}