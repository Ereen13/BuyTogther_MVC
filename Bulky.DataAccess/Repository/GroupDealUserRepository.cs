using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.DataAcess.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BulkyBook.DataAccess.Repository
{
    public class GroupDealUserRepository : Repository<GroupDealUser>, IGroupDealUserRepository
    {
        private readonly ApplicationDbContext _db;

        public GroupDealUserRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public IEnumerable<GroupDealUser> GetUsersForDeal(int dealId)
        {
            return _db.GroupDealUsers
                     .Where(u => u.DealId == dealId)
                     .Include(u => u.User)
                     .Include(u => u.GroupDeal)
                     .ToList();
        }
    }
}
