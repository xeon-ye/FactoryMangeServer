using Coldairarrow.Entity.BaseInfo;
using Coldairarrow.Util;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coldairarrow.Business.BaseInfo
{
    public interface IBaseInfo_ProcessBusiness
    {


        Task<List<BaseInfo_Process>> GetAllDataListAsync();
        Task<PageResult<BaseInfo_Process>> GetDataListAsync(PageInput<ConditionDTO> input);
        Task<BaseInfo_Process> GetTheDataAsync(string id);
        Task AddDataAsync(BaseInfo_Process data);
        Task UpdateDataAsync(BaseInfo_Process data);
        Task DeleteDataAsync(List<string> ids);
    }
}