using Coldairarrow.Entity.BaseInfo;
using Coldairarrow.Util;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coldairarrow.Business.BaseInfo
{
    public interface IBaseInfo_SizeBusiness
    {
        Task<PageResult<BaseInfo_Size>> GetDataListAsync(PageInput<ConditionDTO> input);
        Task<BaseInfo_Size> GetTheDataAsync(string id);
        Task AddDataAsync(BaseInfo_Size data);
        Task UpdateDataAsync(BaseInfo_Size data);
        Task DeleteDataAsync(List<string> ids);
        Task<List<BaseInfo_Size>> GetAllDataListAsync();
    }
}