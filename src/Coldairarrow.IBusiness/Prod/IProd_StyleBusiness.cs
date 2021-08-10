using Coldairarrow.Entity.Prod;
using Coldairarrow.Util;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coldairarrow.Business.Prod
{
    public interface IProd_StyleBusiness
    {
        Task<PageResult<Prod_Style>> GetDataListAsync(PageInput<ConditionDTO> input);
        Task<Prod_Style> GetTheDataAsync(string id);
        Task AddDataAsync(Prod_Style data);
        Task UpdateDataAsync(Prod_Style data);
        Task DeleteDataAsync(List<string> ids);
    }
}