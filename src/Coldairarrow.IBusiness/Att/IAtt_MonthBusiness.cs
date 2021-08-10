using Coldairarrow.Entity.Att;
using Coldairarrow.Util;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coldairarrow.Business.Att
{
    public interface IAtt_MonthBusiness
    {
        Task<PageResult<Att_Month>> GetDataListAsync(PageInput<DeptMonthDTO> input);
        Task<Att_Month> GetTheDataAsync(string id);
        Task AddDataAsync(Att_Month data);
        Task UpdateDataAsync(Att_Month data);
        Task DeleteDataAsync(List<string> ids);
    }
}