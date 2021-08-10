using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttApi
{
    public class AttMonthInfo
    {
        public string Name { get; set; }
        public string DeptName { get; set; }
        public int DeptId { get; set; }
        public string AttId { get; set; }
        public int UserId { get; set; }


        public string AttMonthStr { get; set; }
        private List<Holiday> Holidays { get; set; }
        /// <summary>
        /// 缺卡次数
        /// </summary>
        public int Absence
        {
            get
            {
                return AttDayList.Values.Where(p =>
                {
                    if (p.AttDay.DayOfWeek == DayOfWeek.Sunday || Holidays.Count(h => h.StartTime == p.AttDay) > 0) return false;
                    return true;
                }).Sum(p => p.Absence);
            }
        }


        /// <summary>
        /// 出勤天数
        /// </summary>
        public int AttendanceDays
        {
            get
            {
                return AttDayList.Values.Count(p => p.Absence < 2);
            }
        }
        public double OverTime
        {
            get
            {
                return AttDayList.Values.Sum(p => p.OverTime);
            }
        }
        private Dictionary<int, AttDayRecord> AttDayList { get; set; } = new();

        public List<AttDayRecord> DayList
        {
            get
            {
                return AttDayList.Values.ToList();
            }
        }
        public AttMonthInfo(UserInfoEntity uie, DateTime month, List<Holiday> holidays)
        {
            Name = uie.Name;
            DeptId = uie.DefaultDeptId;
            DeptName = uie.DeptName;
            AttId = uie.BadgeNumber;
            UserId = uie.UserId;
            Holidays = holidays;
            AttMonthStr = month.Year + "-" + month.Month;
            DateTime endDay = month.AddMonths(1).AddDays(-1);

            if (endDay >= DateTime.Now.Date) endDay = DateTime.Now.Date.AddDays(-1);

            for (int i = 1; i <= endDay.Day; i++)
            {
                AttDayRecord adr = new();
                adr.UserId = UserId;
                adr.AttDay = month.AddDays(i - 1);
                AttDayList.Add(i, adr);
            }

        }
        public void Add(CheckInOutEntity check)
        {
            int day = check.CheckTime.Day;
            if (AttDayList.ContainsKey(day)){

                AttDayList[day].CheckTimes.Add(check.CheckTime);
            };

        }

        public void AddList(List<CheckInOutEntity> checks)
        {
            checks.ForEach(p => Add(p));
        }
    }
}
