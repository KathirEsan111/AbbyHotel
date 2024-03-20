
using Abby.DataAccess.Repository.IRepository;
using Abby.Models;
using AbbyWeb.DataAccess.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace AbbyWeb.Pages.Admin.Categories
{
    public class DeleteModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        public Category Category { get; set; }
        public DeleteModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void OnGet(int id)
        {
            Category=_unitOfWork.Category.GetFirstOrDefault(c => c.Id == id);
            //Category = _db.Category.FirstOrDefault(c => c.Id == id);
            //Category = _db.Category.SingleOrDefault(c => c.Id == id);//also use single
            //Category = _db.Category.Where(c => c.Id == id);

        }
        public async Task<IActionResult> OnPost(Category category)           
        {
            _unitOfWork.Category.Remove(category);
                 _unitOfWork.Save();
                TempData["Success"] = "Deleted Successfully";
                return RedirectToPage("Index");          
            
        }
    }
}
