using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.DataAcess.Data;
using BulkyBook.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BulkyBook.DataAccess.Repository
{
    public class GroupDealRepository : Repository<GroupDeal>, IGroupDealRepository
    {
        private readonly ApplicationDbContext _db;

        public GroupDealRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        // GET جميع الصفقات النشطة
        public IEnumerable<GroupDeal> GetAllActiveDeals()
        {
            return _db.GroupDeals
                      .Include(g => g.Product)
                      .Where(g => !g.IsCompleted && g.ExpirationDate >= DateTime.Now)
                      .ToList();
        }

        // GET صفقة واحدة مع المستخدمين
        public GroupDeal GetDealWithUsers(int dealId)
        {
            return _db.GroupDeals
                      .Include(g => g.Product)
                      .Include(g => g.GroupDealUsers)
                      .FirstOrDefault(g => g.DealId == dealId);
        }

        // UPDATE صفقة
        public void Update(GroupDeal obj)
        {
            var dealFromDb = _db.GroupDeals.FirstOrDefault(d => d.DealId == obj.DealId);

            if (dealFromDb != null)
            {
                dealFromDb.ProductId = obj.ProductId;
                dealFromDb.RequiredUsers = obj.RequiredUsers;
                dealFromDb.DiscountPrice = obj.DiscountPrice;
                dealFromDb.ExpirationDate = obj.ExpirationDate;
                dealFromDb.IsCompleted = obj.IsCompleted;
                dealFromDb.JoinedUsersCount = obj.JoinedUsersCount;

                // لو حابة تحدث Navigation Properties
                // dealFromDb.Product = obj.Product;
                // dealFromDb.GroupDealUsers = obj.GroupDealUsers;
            }
        }
    }
}
