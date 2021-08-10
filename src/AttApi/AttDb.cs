using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;
using Microsoft.Extensions.Options;

namespace AttApi
{
    public interface IAttDb
    {
        UserInfoEntity GetUserById(string Badgenumber);
        List<UserInfoEntity> GetUserList();
        IEnumerable<UserInfoEntity> GetUserListByName(string UserName);
        AttMonthInfo GetMonthInfo(string attId, DateTime month);
        List<AttDayRecord> GetDetpAttDayLIst(DateTime date, List<string> userAttList);
    }


    public class AttDb : IAttDb
    {
        private AttOption _option;
        IDbConnection _conn;

        string baseUserQuery = @"SELECT   USERINFO.USERID, USERINFO.Badgenumber, USERINFO.SSN, USERINFO.Name, USERINFO.PAGER, 
                USERINFO.OPHONE, USERINFO.DEFAULTDEPTID, DEPARTMENTS.DEPTNAME
                FROM  USERINFO LEFT OUTER JOIN DEPARTMENTS
                ON USERINFO.DEFAULTDEPTID = DEPARTMENTS.DEPTID";
        public AttDb(IOptions<AttOption> option)
        {
            _option = option.Value;
            _conn = new SqlConnection(_option.AttConString);

        }

        public UserInfoEntity GetUserById(string Badgenumber)
        {
            string query = baseUserQuery + @"  where Badgenumber=@AttId";

            var result = _conn.Query<UserInfoEntity>(query, new { Attid = Badgenumber }).FirstOrDefault();
            return result;
        }

        public IEnumerable<UserInfoEntity> GetUserListByName(string UserName)
        {
            string query = baseUserQuery + " where Name=@name";
            return _conn.Query<UserInfoEntity>(query, new { name = UserName });

        }

        public List<UserInfoEntity> GetUserList()
        {
            string query = baseUserQuery;
            return _conn.Query<UserInfoEntity>(query).ToList();

        }

        public List<Holiday> GetHolidays()
        {
            string query = "select HolidayName,StartTime from HOLIDAYS";
            return _conn.Query<Holiday>(query).ToList();
        }

        public List<CheckInOutEntity> GetCheckListByUserId(int _userid, DateTime _begin, DateTime _end)
        {
            string query = "select * from checkinout where (USERID = @userid) AND (CHECKTIME >=@begin) AND (CHECKTIME < @end) ";
            return _conn.Query<CheckInOutEntity>(query, new { userid = _userid, begin = _begin, end = _end }).ToList();
        }


        public AttMonthInfo GetMonthInfo(string attId, DateTime month)
        {
            var user = GetUserById(attId);
            if (user == null) return null;
            var clist = GetCheckListByUserId(user.UserId, month, month.AddMonths(1));
            var holidays = GetHolidays();
            AttMonthInfo ami = new(user, month, holidays);
            ami.AddList(clist);
            return ami;
        }

        public List<AttDayRecord> GetDetpAttDayLIst(DateTime date, List<string> userAttList)
        {
            var bt = date.Date;
            var et = date.Date.AddDays(1);
            string query2 = string.Format(@"SELECT   TOP (1000) USERINFO.Name, USERINFO.Badgenumber, CHECKINOUT.USERID, CHECKINOUT.CHECKTIME, 
                             CHECKINOUT.CHECKTYPE, CHECKINOUT.VERIFYCODE, CHECKINOUT.SENSORID, CHECKINOUT.Memoinfo, 
                             CHECKINOUT.WorkCode, CHECKINOUT.sn, CHECKINOUT.UserExtFmt
                             FROM      CHECKINOUT LEFT OUTER JOIN USERINFO
                             ON CHECKINOUT.USERID = USERINFO.USERID
                             WHERE   (USERINFO.Badgenumber IN @ulist) AND (CHECKINOUT.CHECKTIME >  @beginTime) AND (CHECKINOUT.CHECKTIME < @endTime)");
            var result = _conn.Query<CheckInOutEntity>(query2, new { ulist = userAttList, beginTime = bt, endTime = et });
            List<AttDayRecord> latt = new();
            
            result.ToList().ForEach(p =>
            {
                var adr = latt.SingleOrDefault(s => s.AttId == p.Badgenumber);
                if (adr == null)
                {
                    adr = new AttDayRecord
                    {
                        Name = p.Name,
                        UserId = p.UserId,
                        AttId = p.Badgenumber,
                        AttDay = p.CheckTime.Date,
                    };
                    latt.Add(adr);
                }
                adr.CheckTimes.Add(p.CheckTime);
                
            });
            return latt;
        }
    }
}
