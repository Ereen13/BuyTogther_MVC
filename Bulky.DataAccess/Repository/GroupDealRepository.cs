using BulkyBook.DataAcess.Data;
using BulkyBook.Models;
using BulkyBook.DataAccess.Repository.IRepository;
using System.Linq;

namespace BulkyBook.DataAccess.Repository
{
    public class GroupDealRepository : Repository<GroupDeal>, IGroupDealRepository
    {
        private ApplicationDbContext _db;

        public GroupDealRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(GroupDeal obj)
        {
            var objFromDb = _db.GroupDeals.FirstOrDefault(u => u.DealId == obj.DealId);
            if (objFromDb != null)
            {
                objFromDb.ProductId = obj.ProductId;
                objFromDb.OriginalPrice = obj.OriginalPrice;
                objFromDb.GroupPrice = obj.GroupPrice;
                objFromDb.RequiredUsers = obj.RequiredUsers;
                objFromDb.StartDate = obj.StartDate;
                objFromDb.EndDate = obj.EndDate;
                objFromDb.IsActive = obj.IsActive;
                objFromDb.IsCompleted = obj.IsCompleted;
            }
        }
    }
}
