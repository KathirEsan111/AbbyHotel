using Abby.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AbbyWeb.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public OrderController(IUnitOfWork unitOfWork)
        {
            _unitOfWork=unitOfWork;
        }

        [HttpGet]
        public ActionResult Get()
        {
            var orderHeaderList = _unitOfWork.OrderHeader.GetAll(includeProperties:"ApplicationUser");
            return Json(new {data=orderHeaderList});
        }
    }
}
