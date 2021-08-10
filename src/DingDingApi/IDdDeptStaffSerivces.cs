using Coldairarrow.Entity.Base_Manage;
using System.Collections.Generic;

namespace DingDingApi
{
    public interface IDdDeptStaffSerivces
    {
        IEnumerable<Base_Department> GetDepts();
        IEnumerable<Base_Staff> GetStaffs();
    }
}