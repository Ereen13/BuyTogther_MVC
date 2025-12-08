using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BulkyBook.Models;
namespace BulkyBook.DataAccess.Repository.IRepository
{
    public interface IGroupDealUserRepository : IRepository<GroupDealUser>
    {
        bool UserJoinedBefore(int dealId, string userId);
    }
}