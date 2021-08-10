using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coldairarrow.Entity.Att
{
    /// <summary>
    /// Att_Month
    /// </summary>
    [Table("Att_Month")]
    public class Att_Month
    {

        /// <summary>
        /// 加班时长
        /// </summary>
        public String Id { get; set; }
        /// <summary>
        /// 考勤ID
        /// </summary>
        public String AttId { get; set; }


        /// <summary>
        /// 员工ID
        /// </summary>
        public String StaffId { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public String Name { get; set; }

        /// <summary>
        /// 月份
        /// </summary>
        public DateTime Month { get; set; }

        /// <summary>
        /// 出勤天数
        /// </summary>
        public Int32 AttendanceDays { get; set; }

        /// <summary>
        /// 缺勤次数
        /// </summary>
        public int Absence { get; set; }

        /// <summary>
        /// OverTime
        /// </summary>
        public Double OverTime { get; set; }

        /// <summary>
        /// 部门
        /// </summary>
        public String DeptName { get; set; }

        /// <summary>
        /// DeptId
        /// </summary>
        public String DeptId { get; set; }

        /// <summary>
        /// CreateTime
        /// </summary>
        public DateTime? CreateTime { get; set; }

        /// <summary>
        /// CreatorId
        /// </summary>
        public String CreatorId { get; set; }

    }
}