using Abby.DataAccess.Repository.IRepository;
using Abby.Models;
using Abby.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Stripe.Checkout;
using System.Security.Claims;

namespace AbbyWeb.Pages.Customer.Cart
{
    [Authorize]
    [BindProperties]
    public class SummaryModel : PageModel
    {
        public IEnumerable<ShoppingCart> ShoppingCartList { get; set; }
        public OrderHeader OrderHeader { get; set; }

        public readonly IUnitOfWork _unitOfWork;
        public SummaryModel(IUnitOfWork unitofWork)
        {
            _unitOfWork = unitofWork;
            OrderHeader = new OrderHeader();


        }
        public void OnGet()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            if (claim != null)
            {
                ShoppingCartList = _unitOfWork.ShoppingCart.GetAll(filter: u => u.ApplicationUserId == claim.Value, 
                    includeProperties: "MenuItem,MenuItem.Foodtype,MenuItem.Category");
            }
            foreach (var cartItem in ShoppingCartList)
            {
                OrderHeader.OrderTotal += (cartItem.Count * cartItem.MenuItem.Price);
            }
            ApplicationUser ApplicationUser = _unitOfWork.ApplicationUser.GetFirstOrDefault(u => u.Id == claim.Value);
            OrderHeader.PickupName = ApplicationUser.FirstName + " " + ApplicationUser.LastName;
            OrderHeader.PhoneNumber = ApplicationUser.PhoneNumber;

        }
        public IActionResult OnPost()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            if (claim != null)
            {
                ShoppingCartList = _unitOfWork.ShoppingCart.GetAll(filter: u => u.ApplicationUserId == claim.Value, includeProperties: "MenuItem,MenuItem.Foodtype,MenuItem.Category");

                foreach (var cartItem in ShoppingCartList)
                {
                    OrderHeader.OrderTotal += (cartItem.Count * cartItem.MenuItem.Price);
                }
                OrderHeader.Status = SD.StatusPending;
                OrderHeader.OrderDate = System.DateTime.Now;
                OrderHeader.UserId = claim.Value;
                OrderHeader.PickupTime = Convert.ToDateTime(OrderHeader.PickUpDate.ToShortDateString() + " " + OrderHeader.PickupTime.ToShortTimeString());
                _unitOfWork.OrderHeader.Add(OrderHeader);
                _unitOfWork.Save();

                foreach (var item in ShoppingCartList)
                {
                    OrderDetails orderDetails = new()
                    {
                        MenuItemId = item.MenuItemId,
                        OrderId = OrderHeader.Id,
                        Name = item.MenuItem.Name,
                        Price = item.MenuItem.Price,
                        Count = item.Count

                    };
                    _unitOfWork.OrderDetail.Add(orderDetails);

                }
                //int quantity = ShoppingCartList.ToList().Count;
                //_unitOfWork.ShoppingCart.RemoveRange(ShoppingCartList);
                _unitOfWork.Save();

                var domain = "http://localhost:32210/";
                var options = new SessionCreateOptions
                {
                    LineItems = new List<SessionLineItemOptions>(),                                
                    Mode = "payment",
                    SuccessUrl = domain + $"Customer/Cart/OrderConfirmation?id={OrderHeader.Id}",
                    CancelUrl = domain + "/cancel.html",
                };
                //add line items
                foreach (var item in ShoppingCartList)
                {
                    var sessionLineItem = new SessionLineItemOptions
                    {
                        // Provide the exact Price ID (for example, pr_1234) of the product you want to sell
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            UnitAmount = (long)(item.MenuItem.Price * 100),
                            Currency = "usd",
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = item.MenuItem.Name,
                                //Description = "Total Distinct Item - " + quantity

                            },

                        },
                        Quantity = item.Count,
                    };
                    options.LineItems.Add(sessionLineItem);

                }


                var service = new SessionService();
                Session session = service.Create(options);
                Response.Headers.Add("Location", session.Url);
                OrderHeader.SessionId = session.Id;
                //Session confirmedSession = service.Get(session.Id);

                //OrderHeader.PaymentIntendId = confirmedSession.PaymentIntentId;
                // OrderHeader.PaymentIntendId = session.PaymentIntentId;
                _unitOfWork.Save();
                return new StatusCodeResult(303);
            }
            return Page();

        }

    }
}
