
using Abby.DataAccess.Repository.IRepository;
using Abby.Models;
using AbbyWeb.DataAccess.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace AbbyWeb.Pages.Admin.Categories
{
    public class CreateModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        public Category Category { get; set; }
        public CreateModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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
                _unitOfWork.Category.Add(category);
                 _unitOfWork.Save();
                TempData["Success"] = "Created Successfully";
                return RedirectToPage("Index");
            }
            return Page();
        }
    }
}
