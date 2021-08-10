using Coldairarrow.Entity.BaseInfo;
using Coldairarrow.Util;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coldairarrow.Business.BaseInfo
{
    public interface IBaseInfo_ColorBusiness
    {
        Task<PageResult<BaseInfo_Color>> GetDataListAsync(PageInput<ConditionDTO> input);
        Task<BaseInfo_Color> GetTheDataAsync(string id);
        Task AddDataAsync(BaseInfo_Color data);
        Task UpdateDataAsync(BaseInfo_Color data);
        Task DeleteDataAsync(List<string> ids);
    }
}