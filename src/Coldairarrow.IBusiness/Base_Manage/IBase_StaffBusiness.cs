using Coldairarrow.Entity.Base_Manage;
using Coldairarrow.Util;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coldairarrow.Business.Base_Manage
{
    public interface IBase_StaffBusiness
    {
        Task<PageResult<Base_Staff>> GetDataListAsync(PageInput<ConditionDTO> input);
        Task<Base_Staff> GetTheDataAsync(string id);
        Task AddDataAsync(Base_Staff data);
        Task UpdateDataAsync(Base_Staff data);
        Task DeleteDataAsync(List<string> ids);
        Task<IEnumerable<Base_Staff>> GetAllDataListAsync();
        void UpDataOrAddStaffs(IEnumerable<Base_Staff> staffs);
        void UpDataList(List<Base_Staff> list);
        Base_Staff GetStaffByDdId(string DdId);


    }
}