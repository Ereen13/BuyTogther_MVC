using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using System;
using System.Collections.Generic;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface IGroupDealRepository : IRepository<GroupDeal>
{
    IEnumerable<GroupDeal> GetAllActiveDeals();
    GroupDeal GetDealWithUsers(int dealId);
    void Update(GroupDeal obj);
}

