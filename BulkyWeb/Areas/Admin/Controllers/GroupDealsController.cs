using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using BulkyBook.Models.ViewModels;
using BulkyBook.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;

namespace BulkyBookWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class GroupDealsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public GroupDealsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: Index
        public IActionResult Index()
        {
            var deals = _unitOfWork.GroupDeal.GetAll(includeProperties: "Product").ToList();
            return View(deals);
        }

        // GET: Upsert
        public IActionResult Upsert(int? id)
        {
            var dealVM = new GroupDealVM()
            {
                ProductList = _unitOfWork.Product.GetAll().Select(p => new SelectListItem
                {
                    Text = p.Title,
                    Value = p.Id.ToString()
                }),
                GroupDeal = new GroupDeal()
            };

            if (id != null && id != 0)
                dealVM.GroupDeal = _unitOfWork.GroupDeal.Get(u => u.DealId == id);

            return View(dealVM);
        }

        // POST: Upsert
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(GroupDealVM dealVM)
        {
            if (!ModelState.IsValid)
            {
                // إعادة ProductList لو في خطأ
                dealVM.ProductList = _unitOfWork.Product.GetAll().Select(p => new SelectListItem
                {
                    Text = p.Title,
                    Value = p.Id.ToString()
                });
                return View(dealVM);
            }

            // التحقق من وجود ديل نشط لنفس المنتج
            bool exists = _unitOfWork.GroupDeal.GetAll()
                .Any(d => d.ProductId == dealVM.GroupDeal.ProductId
                          && !d.IsCompleted
                          && d.EndDate >= System.DateTime.UtcNow
                          && d.DealId != dealVM.GroupDeal.DealId);

            if (exists)
            {
                TempData["error"] = "A group deal for this product already exists!";
                dealVM.ProductList = _unitOfWork.Product.GetAll().Select(p => new SelectListItem
                {
                    Text = p.Title,
                    Value = p.Id.ToString()
                });
                return View(dealVM);
            }

            if (dealVM.GroupDeal.DealId == 0)
                _unitOfWork.GroupDeal.Add(dealVM.GroupDeal);
            else
                _unitOfWork.GroupDeal.Update(dealVM.GroupDeal);

            _unitOfWork.Save();
            TempData["success"] = "Group Deal saved!";
            return RedirectToAction("Index");
        }

        #region API Calls

        [HttpGet]
        public IActionResult GetAll()
        {
            var deals = _unitOfWork.GroupDeal.GetAll(includeProperties: "Product").ToList();
            return Json(new { data = deals });
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var deal = _unitOfWork.GroupDeal.Get(u => u.DealId == id);
            if (deal == null) return Json(new { success = false, message = "Error while deleting" });

            _unitOfWork.GroupDeal.Remove(deal);
            _unitOfWork.Save();

            return Json(new { success = true, message = "Delete Successful" });
        }

        #endregion
    }
}
