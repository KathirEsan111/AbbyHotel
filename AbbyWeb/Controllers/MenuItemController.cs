using Abby.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace AbbyWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuItemController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _WebHostEnvironment;

        public MenuItemController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _WebHostEnvironment=webHostEnvironment;
        }
        [HttpGet]
        public IActionResult Get()
        {
            var menuItemList=_unitOfWork.MenuItem.GetAll(includeProperties: "Category,Foodtype");
            return Json(new
            {
                data = menuItemList
            });
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var objFromdb=_unitOfWork.MenuItem.GetFirstOrDefault(c => c.Id == id);
            var oldImagePath = Path.Combine(_WebHostEnvironment.WebRootPath, objFromdb.Image.TrimStart('\\'));
            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }
            _unitOfWork.MenuItem.Remove(objFromdb);
            _unitOfWork.Save();

            return Json(new { success = true, message = "Delete successful." });
        }
    }
}
