using Coldairarrow.Entity.BaseInfo;
using Coldairarrow.Util;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coldairarrow.Business.BaseInfo
{
    public interface IBaseInfo_NeedleBusiness
    {
        Task<PageResult<BaseInfo_Needle>> GetDataListAsync(PageInput<ConditionDTO> input);
        Task<BaseInfo_Needle> GetTheDataAsync(string id);
        Task AddDataAsync(BaseInfo_Needle data);
        Task UpdateDataAsync(BaseInfo_Needle data);
        Task DeleteDataAsync(List<string> ids);
        Task<List<BaseInfo_Needle>> GetAllDataListAsync();
    }
}