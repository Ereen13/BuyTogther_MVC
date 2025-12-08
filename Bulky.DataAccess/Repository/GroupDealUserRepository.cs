using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.DataAcess.Data;
using BulkyBook.Models;
using System.Linq;

namespace BulkyBook.DataAccess.Repository
{
    public class GroupDealUserRepository : Repository<GroupDealUser>, IGroupDealUserRepository
    {
        private readonly ApplicationDbContext _db;

        public GroupDealUserRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public bool UserJoinedBefore(int dealId, string userId)
        {
            return _db.GroupDealUsers.Any(u => u.DealId == dealId && u.UserId == userId);
        }
    }
}
