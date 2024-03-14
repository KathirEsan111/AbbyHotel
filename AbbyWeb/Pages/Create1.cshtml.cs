using AbbyWeb.Data;
using AbbyWeb.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
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
            if (category.Name == category.DisplayOrder.ToString())
            {
                ModelState.AddModelError(string.Empty, "Display Order and Name cannot be same!");/*use catetory.Name instead of to show error in Name field string.Empt*/
            }
            if (ModelState.IsValid)
            {
                await _db.Category.AddAsync(category);
                await _db.SaveChangesAsync();
                TempData["Success"] = "Created Successfully";
                return RedirectToPage("Categories/Index");
            }
            return Page();
        }
    }
}
