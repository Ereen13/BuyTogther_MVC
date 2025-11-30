using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace BulkyBookWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class GroupDealsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public GroupDealsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: Group Deals List
        public IActionResult Index()
        {
            var deals = _unitOfWork.GroupDeal.GetAll(includeProperties: "Product");
            return View(deals);
        }

        // GET Create
        public IActionResult Create()
        {
            ViewBag.Products = _unitOfWork.Product.GetAll();
            return View();
        }

        // POST Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(GroupDeal obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.GroupDeal.Add(obj);
                _unitOfWork.Save();
                return RedirectToAction("Index");
            }

            ViewBag.Products = _unitOfWork.Product.GetAll();
            return View(obj);
        }

        // GET Edit
        public IActionResult Edit(int id)
        {
            var deal = _unitOfWork.GroupDeal.Get(u => u.DealId == id);
            if (deal == null) return NotFound();

            ViewBag.Products = _unitOfWork.Product.GetAll();
            return View(deal);
        }

        // POST Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(GroupDeal obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.GroupDeal.Update(obj);
                _unitOfWork.Save();
                return RedirectToAction("Index");
            }

            ViewBag.Products = _unitOfWork.Product.GetAll();
            return View(obj);
        }

        // GET Details + Countdown + Join Button
        public IActionResult Details(int id)
        {
            var deal = _unitOfWork.GroupDeal
                .Get(u => u.DealId == id, includeProperties: "Product,GroupDealUsers");

            if (deal == null) return NotFound();

            return View(deal);
        }

        // POST Join Deal
        [HttpPost]
        public IActionResult JoinDeal(int dealId)
        {
            var deal = _unitOfWork.GroupDeal
                .Get(u => u.DealId == dealId, includeProperties: "GroupDealUsers");

            if (deal == null) return NotFound();

            var userId = "HardcodedUser"; // TODO: Replace with real logged user

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

            return RedirectToAction("Details", new { id = dealId });
        }

        // DELETE
        public IActionResult Delete(int id)
        {
            var deal = _unitOfWork.GroupDeal.Get(u => u.DealId == id);

            if (deal == null) return NotFound();

            _unitOfWork.GroupDeal.Remove(deal);
            _unitOfWork.Save();
            return RedirectToAction("Index");
        }
    }
}
