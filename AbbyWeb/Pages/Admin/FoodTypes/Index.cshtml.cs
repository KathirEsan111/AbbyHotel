using Abby.DataAccess.Repository.IRepository;
using Abby.Models;
using AbbyWeb.DataAccess.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AbbyWeb.Pages.Admin.FoodTypes
{
    public class IndexModel : PageModel
    {
        private readonly IUnitOfWork _IunitOfWork;
        public IEnumerable<Foodtype> FoodTypes { get; set; }

        public IndexModel(IUnitOfWork unitOfWork)
        {
            _IunitOfWork = unitOfWork;
        }
        public void OnGet()
        {
            FoodTypes = _IunitOfWork.Foodtype.GetAll();
        }
    }
}
