//using Microsoft.AspNetCore.Mvc;
//using BulkyBook.DataAccess.Repository.IRepository;
//using BulkyBook.Models;
//using Microsoft.AspNetCore.Authorization;
//using System.Security.Claims;

//namespace BulkyBookWeb.Areas.Customer.Controllers
//{
//    [Area("Customer")]
//    public class DealsController : Controller
//    {
//        private readonly IUnitOfWork _unitOfWork;

//        public DealsController(IUnitOfWork unitOfWork)
//        {
//            _unitOfWork = unitOfWork;
//        }

//        // عرض جميع الـ Active Deals
//        public IActionResult Index()
//        {
//            var deals = _unitOfWork.GroupDeal.GetActiveDeals();
//            return View(deals);
//        }

//        // عرض تفاصيل الديل
//        public IActionResult Details(int id)
//        {
//            var deal = _unitOfWork.GroupDeal.GetByIdWithUsers(id);
//            if (deal == null) return NotFound();
//            return View(deal);
//        }

//        // انضمام اليوزر
//        [HttpPost]
//        [Authorize]
//        public IActionResult Join(int dealId)
//        {
//            var deal = _unitOfWork.GroupDeal.GetByIdWithUsers(dealId);
//            if (deal == null)
//            {
//                TempData["error"] = "Deal not found.";
//                return RedirectToAction("Index");
//            }

//            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
//            if (userId == null)
//            {
//                TempData["error"] = "You must be logged in.";
//                return RedirectToAction("Details", new { id = dealId });
//            }

//            // نتاكد الليست مش null
//            deal.GroupDealUsers ??= new List<GroupDealUser>();

//            // لو الديل خلص أو منتهي
//            if (deal.IsCompleted || deal.EndDate < DateTime.UtcNow)
//            {
//                TempData["error"] = "This deal is no longer active.";
//                return RedirectToAction("Details", new { id = dealId });
//            }

//            // لو اليوزر منضم بالفعل
//            bool alreadyJoined = deal.GroupDealUsers.Any(u => u.UserId == userId);
//            if (alreadyJoined)
//            {
//                TempData["info"] = "You already joined this deal.";
//                return RedirectToAction("Details", new { id = dealId });
//            }

//            // إضافة اليوزر للديل
//            var join = new GroupDealUser
//            {
//                DealId = dealId,
//                UserId = userId,
//                JoinedDate = DateTime.UtcNow
//            };

//            // أضفه جوّا الديل نفسه (EF tracking)
//            deal.GroupDealUsers.Add(join);

//            // تحديث حالة الاكتمال
//            if (deal.GroupDealUsers.Count >= deal.RequiredUsers)
//                deal.IsCompleted = true;

//            // حفظ الداتا
//            _unitOfWork.GroupDeal.Update(deal);
//            _unitOfWork.Save();

//            TempData["success"] = "You have successfully joined the deal!";
//            return RedirectToAction("Details", new { id = dealId });
//        }

//    }
//}
