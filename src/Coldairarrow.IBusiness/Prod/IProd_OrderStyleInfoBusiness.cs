using Coldairarrow.Entity.Prod;
using Coldairarrow.Util;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coldairarrow.Business.Prod
{
    public interface IProd_OrderStyleInfoBusiness
    {
        Task<PageResult<Prod_OrderStyleInfo>> GetDataListAsync(PageInput<ConditionDTO> input);
        Task<Prod_OrderStyleInfo> GetTheDataAsync(string id);
        Task AddDataAsync(Prod_OrderStyleInfo data);
        Task UpdateDataAsync(Prod_OrderStyleInfo data);
        Task DeleteDataAsync(List<string> ids);
    }
}