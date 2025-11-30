using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using BulkyBook.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

namespace BulkyBookWeb.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        // -----------------------------
        // Products / Shop
        // -----------------------------
        public IActionResult Index()
        {
            IEnumerable<Product> productList = _unitOfWork.Product.GetAll(includeProperties: "Category,ProductImages");
            return View(productList);
        }

        public IActionResult Details(int productId)
        {
            ShoppingCart cart = new()
            {
                Product = _unitOfWork.Product.Get(u => u.Id == productId, includeProperties: "Category,ProductImages"),
                Count = 1,
                ProductId = productId
            };
            return View(cart);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Details(ShoppingCart shoppingCart)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            shoppingCart.ApplicationUserId = userId;

            ShoppingCart cartFromDb = _unitOfWork.ShoppingCart.Get(u => u.ApplicationUserId == userId &&
                u.ProductId == shoppingCart.ProductId);

            if (cartFromDb != null)
            {
                // shopping cart exists
                cartFromDb.Count += shoppingCart.Count;
                _unitOfWork.ShoppingCart.Update(cartFromDb);
                _unitOfWork.Save();
            }
            else
            {
                // add new cart record
                _unitOfWork.ShoppingCart.Add(shoppingCart);
                _unitOfWork.Save();
                HttpContext.Session.SetInt32(SD.SessionCart,
                    _unitOfWork.ShoppingCart.GetAll(u => u.ApplicationUserId == userId).Count());
            }

            TempData["success"] = "Cart updated successfully";
            return RedirectToAction(nameof(Index));
        }

        // -----------------------------
        // Group Deals / Group Buy
        // -----------------------------
        public IActionResult GroupDeals()
        {
            // جلب كل العروض الفعّالة
            var deals = _unitOfWork.GroupDeal.GetAllActiveDeals();
            return View(deals);
        }

        public IActionResult DealDetails(int id)
        {
            var deal = _unitOfWork.GroupDeal.Get(u => u.DealId == id, includeProperties: "Product,GroupDealUsers");
            if (deal == null) return NotFound();

            return View(deal);
        }

        [HttpPost]
        [Authorize]
        public IActionResult JoinDeal(int dealId)
        {
            var deal = _unitOfWork.GroupDeal.Get(u => u.DealId == dealId, includeProperties: "GroupDealUsers");
            if (deal == null) return NotFound();

            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

            // check if already joined
            bool alreadyJoined = deal.GroupDealUsers.Any(u => u.UserId == userId);
            if (!alreadyJoined)
            {
                deal.JoinedUsersCount++;
                _unitOfWork.GroupDealUser.Add(new GroupDealUser
                {
                    DealId = dealId,
                    UserId = userId,
                    JoinedDate = DateTime.Now
                });

                if (deal.JoinedUsersCount >= deal.RequiredUsers)
                    deal.IsCompleted = true;

                _unitOfWork.GroupDeal.Update(deal);
                _unitOfWork.Save();
            }

            return RedirectToAction("DealDetails", new { id = dealId });
        }

        // -----------------------------
        // Privacy & Error
        // -----------------------------
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
