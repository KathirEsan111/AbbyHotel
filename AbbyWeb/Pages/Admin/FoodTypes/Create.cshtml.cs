using Abby.Models;
using AbbyWeb.DataAccess.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AbbyWeb.Pages.Admin.FoodTypes
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public Foodtype Foodtype { get; set; }
        public CreateModel(ApplicationDbContext db)
        {
            _db = db;   
        }
        public void OnGet()
        {
        }

        public async Task<ActionResult> OnPost(Foodtype foodtype) 
        {
            if (ModelState.IsValid)
            {
                await _db.Foodtype.AddAsync(foodtype);
                await _db.SaveChangesAsync();
                TempData["success"] = "Created Successfully";
                return RedirectToPage("Index");
            }
            return Page();
        }
    }
}
