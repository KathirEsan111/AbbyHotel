using Abby.DataAccess.Repository.IRepository;
using Abby.Models;
using AbbyWeb.DataAccess.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AbbyWeb.Pages.Admin.FoodTypes
{
    public class EditModel : PageModel
    {
        private readonly IUnitOfWork _IunitOfWork;
        public Foodtype Foodtype { get; set; }
        public EditModel(IUnitOfWork IunitOfWork)
        {
            _IunitOfWork = IunitOfWork;
        }
        public void OnGet(int id)
        {
            Foodtype = _IunitOfWork.Foodtype.GetFirstOrDefault(c => c.Id == id);

        }
        public async Task<IActionResult> OnPost(Foodtype foodtype) 
        {
            
            if (ModelState.IsValid)
            {
                _IunitOfWork.Foodtype.Update(foodtype);
                _IunitOfWork.Save();
                TempData["Success"] = "Edited Successfully";
                return RedirectToPage("Index");
            }
            return Page();
        }

    }
}
