
using Abby.Models;
using AbbyWeb.DataAccess.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace AbbyWeb.Pages.Admin.Categories
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public Category Category { get; set; }
        public EditModel(ApplicationDbContext db)
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
            if (category.Name == category.DisplayOrder.ToString())
            {
                ModelState.AddModelError(string.Empty, "Display Order and Name cannot be same!");/*use catetory.Name instead of to show error in Name field string.Empt*/
            }
            if (ModelState.IsValid)
            {
                 _db.Category.Update(category);
                await _db.SaveChangesAsync();
                TempData["Success"] = "Edited Successfully";
                return RedirectToPage("Index");
            }
            return Page();
        }
    }
}
