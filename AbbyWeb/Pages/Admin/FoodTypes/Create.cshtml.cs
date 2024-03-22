using Abby.DataAccess.Repository.IRepository;
using Abby.Models;
using AbbyWeb.DataAccess.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AbbyWeb.Pages.Admin.FoodTypes
{
    public class CreateModel : PageModel
    {
        private readonly IUnitOfWork _IunitOfWork;

        public Foodtype Foodtype { get; set; }
        public CreateModel(IUnitOfWork IunitOfWork)
        {
            _IunitOfWork = IunitOfWork;   
        }
        public void OnGet()
        {
        }

        public async Task<ActionResult> OnPost(Foodtype foodtype) 
        {
            if (ModelState.IsValid)
            {
                 _IunitOfWork.Foodtype.Add(foodtype);
                 _IunitOfWork.Save();
                TempData["success"] = "Created Successfully";
                return RedirectToPage("Index");
            }
            return Page();
        }
    }
}
