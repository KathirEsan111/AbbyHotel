using Abby.DataAccess.Repository.IRepository;
using Abby.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AbbyWeb.Pages.Customer.Home
{
    public class IndexModel : PageModel
    {
        private readonly IUnitOfWork _unitofwork;
        public IndexModel(IUnitOfWork unitofwork)
        {
            _unitofwork = unitofwork;
        }
        public IEnumerable<MenuItem> MenuItemList { get; set; }
        public IEnumerable<Category> CategoryList { get; set; }
        public void OnGet()
        {
            MenuItemList=_unitofwork.MenuItem.GetAll(includeProperties: "Category,Foodtype");
            CategoryList = _unitofwork.Category.GetAll(orderby: u=>u.OrderBy(c=>c.DisplayOrder));
        }

    }
}
