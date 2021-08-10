using Coldairarrow.Entity.BaseInfo;
using Coldairarrow.Util;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coldairarrow.Business.BaseInfo
{
    public interface IBaseInfo_ProofTypeBusiness
    {
        Task<PageResult<BaseInfo_ProofType>> GetDataListAsync(PageInput<ConditionDTO> input);
        Task<BaseInfo_ProofType> GetTheDataAsync(string id);
        Task AddDataAsync(BaseInfo_ProofType data);
        Task UpdateDataAsync(BaseInfo_ProofType data);
        Task DeleteDataAsync(List<string> ids);
        Task<List<BaseInfo_ProofType>> GetAllDataListAsync();
    }
}