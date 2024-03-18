using Abby.Models;
using AbbyWeb.DataAccess.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AbbyWeb.Pages.Admin.FoodTypes
{
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public Foodtype Foodtype { get; set; }
        public DeleteModel(ApplicationDbContext db)
        {
            _db = db; 
        }

        public void OnGet(int id)
        {
            Foodtype = _db.Foodtype.First(c=>c.Id==id);
        }
        public async Task<IActionResult> OnPost(Foodtype foodtype)
        {
                
               _db.Foodtype.Remove(foodtype);
                await _db.SaveChangesAsync();
                TempData["Success"] = "Deleted Successfully";
                return RedirectToPage("Index");
            
        }
    }
}
