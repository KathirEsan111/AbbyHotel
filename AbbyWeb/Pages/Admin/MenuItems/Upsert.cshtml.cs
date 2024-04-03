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
        private readonly IWebHostEnvironment _WebHostEnvironment;
        public MenuItem MenuItem { get; set; }

        public IEnumerable<SelectListItem> CateoryList { get; set; }
        public IEnumerable<SelectListItem> FoodTypeList { get; set; }
        public UpsertModel(IUnitOfWork IunitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _IunitOfWork = IunitOfWork;
            _WebHostEnvironment = webHostEnvironment;
            MenuItem = new();

        }
        public void OnGet(int? id)
        {
            if (id != null)
            {
                //Edit
                MenuItem = _IunitOfWork.MenuItem.GetFirstOrDefault(c => c.Id == id);
            }
            CateoryList = _IunitOfWork.Category.GetAll().Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.Id.ToString()
            });
            FoodTypeList = _IunitOfWork.Foodtype.GetAll().Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.Id.ToString()
            });
        }

        public async Task<ActionResult> OnPost()
        {

            string webRootPath = _WebHostEnvironment.WebRootPath;
            var files = HttpContext.Request.Form.Files;
            if (MenuItem.Id == 0)
            {
                //Create
                string fileName_new = Guid.NewGuid().ToString();
                var uploads = Path.Combine(webRootPath, @"Images\MenuItems");
                var extension = Path.GetExtension(files[0].FileName);
                using (var filestream = new FileStream(Path.Combine(uploads, fileName_new + extension), FileMode.Create))
                {
                    files[0].CopyTo(filestream);
                }
                MenuItem.Image = @"\Images\MenuItems\" + fileName_new + extension;
                _IunitOfWork.MenuItem.Add(MenuItem);
                _IunitOfWork.Save();

            }
            else
            {
                //edit
                var objFromDb = _IunitOfWork.MenuItem.GetFirstOrDefault(c => c.Id == MenuItem.Id);
                if (files.Count > 0)
                {
                    string fileName_new = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(webRootPath, @"Images\MenuItems");
                    var extension = Path.GetExtension(files[0].FileName);
                    //deleting the old image
                    var oldImagePath = Path.Combine(webRootPath, objFromDb.Image.TrimStart('\\'));
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                    using (var filestream = new FileStream(Path.Combine(uploads, fileName_new + extension), FileMode.Create))
                    {
                        files[0].CopyTo(filestream);
                    }
                    MenuItem.Image = @"\Images\MenuItems\" + fileName_new + extension;
                }
                else
                {
                    MenuItem.Image = objFromDb.Image;
                }
                _IunitOfWork.MenuItem.Update(MenuItem);
                _IunitOfWork.Save();
            }
            return RedirectToPage("./Index");
        }
    }
}
