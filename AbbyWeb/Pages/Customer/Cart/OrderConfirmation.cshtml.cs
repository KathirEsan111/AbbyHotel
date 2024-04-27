using Abby.DataAccess.Repository.IRepository;
using Abby.Models;
using Abby.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Stripe.Checkout;

namespace AbbyWeb.Pages.Customer.Cart
{
    public class OrderConfirmationModel : PageModel
    {



        public readonly IUnitOfWork _unitOfWork;
        public int OrderId { get; set; }
        public OrderConfirmationModel(IUnitOfWork unitofWork)
        {
            _unitOfWork = unitofWork;
            


        }
        public void OnGet(int id)
        {
            OrderHeader orderHeader=_unitOfWork.OrderHeader.GetFirstOrDefault(c=>c.Id==id);
            if(orderHeader.SessionId!=null)
            {
                var service = new SessionService();
                Session session=service.Get(orderHeader.SessionId);
                if (session.PaymentStatus.ToLower() == "paid")
                {
                    orderHeader.Status = SD.StatusSubmitted;
                    _unitOfWork.Save();
                }
            }
            List<ShoppingCart> shoppingCarts = 
                _unitOfWork.ShoppingCart.GetAll(c => c.ApplicationUserId == orderHeader.UserId).ToList();
            _unitOfWork.ShoppingCart.RemoveRange(shoppingCarts);
            _unitOfWork.Save();
            OrderId = id;
        }
    }
}
