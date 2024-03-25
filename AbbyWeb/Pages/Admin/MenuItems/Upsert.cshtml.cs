using Abby.DataAccess.Repository;
using Abby.DataAccess.Repository.IRepository;
using Abby.Models;
using AbbyWeb.DataAccess.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AbbyWeb.Pages.Admin.MenuItems
{
    [BindProperties]//this helps to avoid @Model..
    public class UpsertModel : PageModel
    {
        private readonly IUnitOfWork _IunitOfWork;
        public MenuItem MenuItem { get; set; }
        public IEnumerable <SelectListItem> CateoryList { get; set; }
        public IEnumerable <SelectListItem> FoodTypeList { get; set; }
        public UpsertModel(IUnitOfWork IunitOfWork)
        {
            _IunitOfWork = IunitOfWork;
            MenuItem = new();
        }
        public void OnGet()
        {
          CateoryList= _IunitOfWork.Category.GetAll().Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.Id.ToString()
            });
            FoodTypeList= _IunitOfWork.Foodtype.GetAll().Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.Id.ToString()
            });
        }

        public async Task<ActionResult> OnPost() 
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
