using Coldairarrow.Entity.Att;
using Coldairarrow.Util;
using EFCore.Sharding;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using AttApi;
using System;
using Coldairarrow.Entity.Base_Manage;
namespace Coldairarrow.Business.Att
{

    public class Att_DayBusiness : BaseBusiness<Att_Day>, IAtt_DayBusiness, ITransientDependency
    {
        readonly IAttDb _attDb;
        public Att_DayBusiness(IDbAccessor db, IAttDb attDb)
            : base(db)
        {
            _attDb = attDb;
        }

        #region 外部接口

        public async Task<PageResult<Att_Day>> GetDataListAsync(Att_DayPageInput input)
        {
            var q = GetIQueryable();
            var where = LinqHelper.True<Att_Day>();
            if (input.DeptId != null) where = where.And(p => p.DeptId == input.DeptId);
            if (input.SelectMonth != null)
            {
                var bdt = DateTime.Parse(input.SelectMonth + "-01");
                var edt = bdt.AddMonths(1);
                where = where.And(p => p.AttDate >= bdt && p.AttDate < edt);
            }

            var search = input.Search;

            //筛选


            if (!search.Condition.IsNullOrEmpty() && !search.Keyword.IsNullOrEmpty())
            {
                DateTime dt;
                if (search.Condition == "AttDate" && DateTime.TryParse(search.Keyword,out dt))
                {
                    where = where.And(p => p.AttDate == dt);
                }
                else
                {
                    var newWhere = DynamicExpressionParser.ParseLambda<Att_Day, bool>(
                                        ParsingConfig.Default, false, $@"{search.Condition}.Contains(@0)", search.Keyword);
                    where = where.And(newWhere);
                }

            }

            return await q.Where(where).GetPageResultAsync(input);
        }

        public async Task<Att_Day> GetTheDataAsync(string id)
        {
            return await GetEntityAsync(id);
        }

        public void AddData(Att_Day data)
        {
            Insert(data);
        }

        public void UpdateData(Att_Day data)
        {
            Update(data);
        }

        public async Task AddDataAsync(Att_Day data)
        {
            await InsertAsync(data);
        }

        public async Task UpdateDataAsync(Att_Day data)
        {
            await UpdateAsync(data);
        }

        public async Task DeleteDataAsync(List<string> ids)
        {
            await DeleteAsync(ids);
        }

        /// <summary>
        /// 按月从考勤数据库和加班数据库取数据合成返回
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<List<Att_Day>> GetTheDayList(IdMonthDto input)
        {

            var facTask = Db.GetIQueryable<Att_Day>().Where(p => p.StaffId == input.id).ToListAsync();
            var attm = _attDb.GetMonthInfo(input.id, DateTime.Parse(input.month + "-01"));
            if (attm == null) return null;
            var facList = await facTask;
            var t = Task.Run<List<Att_Day>>(() =>
            {
                attm.DayList.ForEach(p =>
                {
                    Att_Day ad = facList.SingleOrDefault(f => f.AttDate == p.AttDay);
                    if (ad == null)
                    {
                        ad = new()
                        {
                            AttDate = p.AttDay,
                            StaffId = input.id,
                            CheckInTime = p.FirstCheck,
                            CheckOutTime = p.LastCheck,
                            OverTime = p.OverTime,
                            AttRecord = p.AttRecordString
                        };
                        facList.Add(ad);
                    }

                });
                return facList;
            });

            return t.Result;
        }

        public List<AttDayReturnEntiry> GetDeptAttDayList(IdDeptDateDto input)
        {
            var userlist = Db.GetIQueryable<Base_Staff>().Where(p => p.DepartmentId == input.deptId).ToList();
            var userStaffList = userlist.Select(p => p.Id);
            var userAttList = userlist.Select(p => p.AttId).ToList();

            var DayList = Db.GetIQueryable<Att_Day>().Where(p => p.AttDate == input.declareDate.Date && userStaffList.Contains(p.StaffId)).ToList();
            var attList = _attDb.GetDetpAttDayLIst(input.declareDate, userAttList);

            List<AttDayReturnEntiry> adreList = new();
            //设置用户信息
            userlist.ForEach(p =>
            {
                AttDayReturnEntiry adre = new();
                adre.SetStaffInfo(p);
                adre.AttDate = input.declareDate.Date;
                adreList.Add(adre);
            });

            //设置加班信息-如有
            DayList.ForEach(p =>
            {
                var re = adreList.FirstOrDefault(u => u.StaffId == p.StaffId);
                if (re != null) re.SetAttDay(p);

            });
            //设置考勤信息
            attList.ForEach(p =>
            {
                var re = adreList.FirstOrDefault(u => u.AttId == p.AttId);
                if (re != null) re.SetDayRecord(p);
            });
            return adreList;
        }


        #endregion

        #region 私有成员

        #endregion
    }
}