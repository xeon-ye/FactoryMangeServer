using AttApi;
using Coldairarrow.Entity.Att;
using Coldairarrow.Util;
using EFCore.Sharding;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Coldairarrow.Entity.Base_Manage;
using Coldairarrow.Business.Base_Manage;
using System;

namespace Coldairarrow.Business.Att
{
    public class Att_MonthBusiness : BaseBusiness<Att_Month>, IAtt_MonthBusiness, ITransientDependency
    {
        readonly IAttDb _attDb;
        readonly IBase_StaffBusiness _staffBus;
        public Att_MonthBusiness(IDbAccessor db, IAttDb attDb, IBase_StaffBusiness staffBus)
            : base(db)
        {
            _attDb = attDb;
            _staffBus = staffBus;
        }

        #region 外部接口

        public async Task<PageResult<Att_Month>> GetDataListAsync(PageInput<DeptMonthDTO> input)
        {

            string year = input.Search.SelectMonth.Split('-')[0];
            string month = input.Search.SelectMonth.Split('-')[1];
            var source = Db.GetIQueryable<Base_Staff>().Where(p => p.DepartmentId == input.Search.SelectDeptId);
            int count = await source.CountAsync();
            var staffList = source.OrderBy($@"{input.SortField} {input.SortType}")
                .Skip((input.PageIndex - 1) * input.PageRows)
                .Take(input.PageRows)
                .ToList();
            List<Att_Month> amList = new();
            staffList.ForEach(p =>
            {
                Att_Month am = new()
                {
                    Name = p.StaffName,
                    DeptId = p.DepartmentId,
                    DeptName = p.DeptName,
                    StaffId = p.Id,
                    AttId = p.AttId,
                    Month = DateTime.Parse(input.Search.SelectMonth + "-01")

                };
                var re = _attDb.GetMonthInfo(am.AttId, am.Month);
                if (re != null)
                {
                    am.AttendanceDays = re.AttendanceDays;
                    am.OverTime = re.OverTime;
                    am.Absence = re.Absence;
                    amList.Add(am);
                }

            });
            return new PageResult<Att_Month> { Data = amList, Total = count, Success = true };



        }

        public async Task<Att_Month> GetTheDataAsync(string id)
        {
            return await GetEntityAsync(id);
        }

        public async Task AddDataAsync(Att_Month data)
        {
            await InsertAsync(data);
        }

        public async Task UpdateDataAsync(Att_Month data)
        {
            await UpdateAsync(data);
        }

        public async Task DeleteDataAsync(List<string> ids)
        {
            await DeleteAsync(ids);
        }

        #endregion

        #region 私有成员

        #endregion
    }
}