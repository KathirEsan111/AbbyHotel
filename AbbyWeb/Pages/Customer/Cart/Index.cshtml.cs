using Abby.DataAccess.Repository;
using Abby.DataAccess.Repository.IRepository;
using Abby.Models;
using Abby.Utility;
using AbbyWeb.DataAccess.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace AbbyWeb.Pages.Customer.Cart
{
    [Authorize]
    
    public class IndexModel : PageModel
    {
        public IEnumerable<ShoppingCart> ShoppingCartList { get; set; }
        public double CartTotal { get; set; }
        private readonly IUnitOfWork _iunitOfWork;
        public IndexModel(IUnitOfWork unitOfWork)
        {
            _iunitOfWork = unitOfWork;
            CartTotal = 0;
        }
        public void OnGet()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            if (claim != null)
            {
                ShoppingCartList=_iunitOfWork.ShoppingCart.GetAll(filter:u=>u.ApplicationUserId==claim.Value, 
                    includeProperties:"MenuItem,MenuItem.Foodtype,MenuItem.Category");
            }
            foreach(var cartItem in ShoppingCartList)
            {
                CartTotal += (cartItem.Count * cartItem.MenuItem.Price);
            }

        }
        public IActionResult OnPostPlus(int cartId)
        {
            var cart = _iunitOfWork.ShoppingCart.GetFirstOrDefault(u => u.Id == cartId);
            _iunitOfWork.ShoppingCart.IncrementCount(cart, 1);
            return RedirectToPage("/Customer/Cart/Index");
        }
        public IActionResult OnPostMinus(int cartId)
        {
           
            var cart = _iunitOfWork.ShoppingCart.GetFirstOrDefault(u => u.Id == cartId);
            if (cart.Count == 1)
            {
                var count = _iunitOfWork.ShoppingCart.GetAll(u => u.ApplicationUserId == cart.ApplicationUserId).ToList().Count - 1;
                _iunitOfWork.ShoppingCart.Remove(cart);                
                _iunitOfWork.Save();
                HttpContext.Session.SetInt32(SD.SessionCart, count);

            }
            else 
            {
                _iunitOfWork.ShoppingCart.DecrementCount(cart, 1);
                
            }
            return RedirectToPage("/Customer/Cart/Index");

        }
        public IActionResult OnPostRemove(int cartId)
        {
            var cart = _iunitOfWork.ShoppingCart.GetFirstOrDefault(u => u.Id == cartId);
            _iunitOfWork.ShoppingCart.Remove(cart);
          var count= _iunitOfWork.ShoppingCart.GetAll(u => u.ApplicationUserId == cart.ApplicationUserId).ToList().Count-1;            
            _iunitOfWork.Save();
            HttpContext.Session.SetInt32(SD.SessionCart, count);
            return RedirectToPage("/Customer/Cart/Index");
        }


    }
}
