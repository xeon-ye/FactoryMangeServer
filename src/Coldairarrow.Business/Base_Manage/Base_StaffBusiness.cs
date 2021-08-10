using Coldairarrow.Entity.Base_Manage;
using Coldairarrow.Util;
using EFCore.Sharding;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using System;

namespace Coldairarrow.Business.Base_Manage
{
    public class Base_StaffBusiness : BaseBusiness<Base_Staff>, IBase_StaffBusiness, ITransientDependency
    {
        public Base_StaffBusiness(IDbAccessor db)
            : base(db)
        {
        }

        #region 外部接口

        public async Task<PageResult<Base_Staff>> GetDataListAsync(PageInput<ConditionDTO> input)
        {
            var q = GetIQueryable();
            var where = LinqHelper.True<Base_Staff>();
            var search = input.Search;
            //筛选
            if (!search.Condition.IsNullOrEmpty() && !search.Keyword.IsNullOrEmpty())
            {
                var newWhere = DynamicExpressionParser.ParseLambda<Base_Staff, bool>(
                    ParsingConfig.Default, false, $@"{search.Condition}.Contains(@0)", search.Keyword);
                where = where.And(newWhere);
            }

            return await q.OrderBy(p => p.DepartmentId).Where(where).GetPageResultAsync(input);
        }

        public async Task<IEnumerable<Base_Staff>> GetAllDataListAsync()
        {

            return await GetIQueryable().ToListAsync();
        }

        public async Task<Base_Staff> GetTheDataAsync(string id)
        {
            return await GetEntityAsync(id);
        }

        public async Task AddDataAsync(Base_Staff data)
        {
            await InsertAsync(data);
        }

        public async Task UpdateDataAsync(Base_Staff data)
        {
            await UpdateAsync(data);
        }


        public void UpDataList(List<Base_Staff> list)
        {
            Update(list);


        }
        public async Task DeleteDataAsync(List<string> ids)
        {
            await DeleteAsync(ids);
        }

        public void UpDataOrAddStaffs(IEnumerable<Base_Staff> staffs)
        {
            staffs.ToList().ForEach(s =>
           {
               s.StaffName = s.StaffName.Trim();
               var staff = GetIQueryable().Where(p => p.Id == s.Id).FirstOrDefault();
             

               if (staff != null)
               {
                   s.AttId = staff.AttId;
                   s.UserId = staff.UserId;
                   Update(s);
               }
               else
               {
                   Insert(s);
               }
           });
           
           
        // int c= Db.Delete<Base_Staff>(p => p.StaffName=="王汉君2");


        }

        public Base_Staff GetStaffByDdId(string DdId)
        {
            return Db.GetIQueryable<Base_Staff>().FirstOrDefault(p => p.DDid == DdId);
           
        }

        #endregion

        #region 私有成员

        #endregion
    }
}