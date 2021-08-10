using Coldairarrow.Entity.BaseInfo;
using Coldairarrow.Util;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coldairarrow.Business.BaseInfo
{
    public interface IBaseInfo_ClientBusiness
    {
        Task<PageResult<BaseInfo_Client>> GetDataListAsync(PageInput<ConditionDTO> input);
        Task<BaseInfo_Client> GetTheDataAsync(string id);
        Task AddDataAsync(BaseInfo_Client data);
        Task UpdateDataAsync(BaseInfo_Client data);
        Task DeleteDataAsync(List<string> ids);
    }
}