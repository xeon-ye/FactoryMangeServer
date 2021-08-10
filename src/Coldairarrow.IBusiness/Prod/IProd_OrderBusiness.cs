using Coldairarrow.Entity.Prod;
using Coldairarrow.Util;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coldairarrow.Business.Prod
{
    public interface IProd_OrderBusiness
    {
        Task<PageResult<Prod_Order>> GetDataListAsync(PageInput<ConditionDTO> input);
        Task<Prod_Order> GetTheDataAsync(string id);
        Task AddDataAsync(Prod_Order data);
        Task UpdateDataAsync(Prod_Order data);
        Task DeleteDataAsync(List<string> ids);
    }
}