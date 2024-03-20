
using Abby.DataAccess.Repository.IRepository;
using Abby.Models;
using AbbyWeb.DataAccess.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace AbbyWeb.Pages.Admin.Categories
{
    public class EditModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;  
        
        public Category Category { get; set; }
        public EditModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void OnGet(int id)
        {
            Category= _unitOfWork.Category.GetFirstOrDefault(c => c.Id == id);
            //Category = _db.Category.FirstOrDefault(c => c.Id == id);
            //Category = _db.Category.SingleOrDefault(c => c.Id == id);//also use single
            //Category = _db.Category.Where(c => c.Id == id).FirstOrDefault;

        }
        public async Task<IActionResult> OnPost(Category category)           
        {
            if (category.Name == category.DisplayOrder.ToString())
            {
                ModelState.AddModelError(string.Empty, "Display Order and Name cannot be same!");/*use catetory.Name instead of to show error in Name field string.Empt*/
            }
            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Update(category);
                _unitOfWork.Save();
                TempData["Success"] = "Edited Successfully";
                return RedirectToPage("Index");
            }
            return Page();
        }
    }
}
