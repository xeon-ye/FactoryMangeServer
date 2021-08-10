using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttApi
{
    public class AttDayRecord
    {
        public string Name { get; set; }

        public string AttId { get; set; }
        public int UserId { get; set; }
        public DateTime AttDay { get; set; }
        public List<DateTime> CheckTimes { get; set; } = new List<DateTime>();

        public string AttRecordString
        {
            get
            {
                string s = "";
                CheckTimes.ForEach(p =>
                {
                    s += p.ToString("HH:mm") + " ";
                });


                return s;

            }
        }

        public DateTime? FirstCheck
        {
            get
            {
                if (CheckTimes.Count > 0) return CheckTimes.OrderBy(p => p).First();
                else return null;
            }
        }

        public DateTime? LastCheck
        {
            get
            {
                if (CheckTimes.Count > 1)
                {
                    TimeSpan t = CheckTimes.OrderBy(p => p).Last() - (DateTime)FirstCheck;
                    if (t.TotalMinutes <= 10) return null;
                    return CheckTimes.OrderBy(p => p).Last();
                }
                else return null;
            }
        }

        /// <summary>
        /// 缺勤次数
        /// </summary>
        public int Absence
        {
            get
            {
                if (FirstCheck == null) return 2;
                if (LastCheck == null) return 1;
                return 0;
            }

        }
        /// <summary>
        /// 总出勤时间
        /// </summary>
        public double TotalTime
        {
            get
            {
                if (Absence > 0) return 0;
                return Math.Round((LastCheck - FirstCheck).Value.TotalHours, 1);
            }
        }


        /// <summary>
        /// 加班时长
        /// </summary>

        public double OverTime
        {
            get
            {
                if (FirstCheck != null && LastCheck != null && (LastCheck.Value.Hour * 60 + LastCheck.Value.Minute) > 1110)
                    return Math.Round((double)(LastCheck.Value.Hour * 60+ LastCheck.Value.Minute - 1080) / 60, 1);
                return 0;
            }
        }

    }
}
