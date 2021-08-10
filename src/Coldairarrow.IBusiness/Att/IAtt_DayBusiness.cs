using Coldairarrow.Entity.Att;
using Coldairarrow.Util;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace Coldairarrow.Business.Att
{
    public interface IAtt_DayBusiness
    {
        Task<PageResult<Att_Day>> GetDataListAsync(Att_DayPageInput input);
        Task<Att_Day> GetTheDataAsync(string id);
        Task AddDataAsync(Att_Day data);
        Task UpdateDataAsync(Att_Day data);
        void UpdateData(Att_Day data);
        void AddData(Att_Day data);
        Task DeleteDataAsync(List<string> ids);
        Task<List<Att_Day>> GetTheDayList(IdMonthDto input);
        List<AttDayReturnEntiry> GetDeptAttDayList(IdDeptDateDto input);

    }

    public class Att_DayPageInput : PageInput<ConditionDTO>
    {
        public string SelectMonth { get; set; }

        public string DeptId { get; set; }
    }
}