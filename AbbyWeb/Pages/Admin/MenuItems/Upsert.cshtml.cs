using Abby.DataAccess.Repository.IRepository;
using Abby.Models;
using AbbyWeb.DataAccess.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AbbyWeb.Pages.Admin.MenuItems
{
    public class UpsertModel : PageModel
    {
        private readonly IUnitOfWork _IunitOfWork;

        public MenuItem MenuItem { get; set; }
        public UpsertModel(IUnitOfWork IunitOfWork)
        {
            _IunitOfWork = IunitOfWork;
            MenuItem = new();
        }
        public void OnGet()
        {
        }

        public async Task<ActionResult> OnPost(Foodtype foodtype) 
        {
            //if (ModelState.IsValid)
            //{
            //     _IunitOfWork.Foodtype.Add(foodtype);
            //     _IunitOfWork.Save();
            //    TempData["success"] = "Created Successfully";
            //    return RedirectToPage("Index");
            //}
            return Page();
        }
    }
}
