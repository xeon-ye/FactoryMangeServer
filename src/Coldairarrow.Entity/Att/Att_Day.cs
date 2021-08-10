using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coldairarrow.Entity.Att
{
    /// <summary>
    /// Att_Day
    /// </summary>
    [Table("Att_Day")]
    public class Att_Day
    {

        /// <summary>
        /// Id
        /// </summary>
        [Key, Column(Order = 1)]
        public String Id { get; set; }

        /// <summary>
        /// CreateTime
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// CreatorId
        /// </summary>
        public String CreatorId { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 部门ID
        /// </summary>
        public string DeptId { get; set; }
        /// <summary>
        /// 部门
        /// </summary>
        public string DeptName { get; set; }
        /// <summary>
        /// 员工ID
        /// </summary>
        public String StaffId { get; set; }

        /// <summary>
        /// 日期
        /// </summary>
        public DateTime AttDate { get; set; }

        /// <summary>
        /// 打卡记录
        /// </summary>
        public String AttRecord { get; set; }

        /// <summary>
        /// 考勤计算加班时间
        /// </summary>
        public Double OverTime { get; set; }

        /// <summary>
        /// 上班打卡时间
        /// </summary>
        public DateTime? CheckInTime { get; set; }

        /// <summary>
        /// 下班打卡时间
        /// </summary>
        public DateTime? CheckOutTime { get; set; }

        /// <summary>
        /// 加班开始时间
        /// </summary>
        public DateTime? BeginOverTime { get; set; }

        /// <summary>
        /// 加班结束时间
        /// </summary>
        public DateTime? EndOverTime { get; set; }

        /// <summary>
        /// 加班申报时间
        /// </summary>
        public Double? DeclareOverTime { get; set; }


    }

    public class AttDayReturnEntiry : Att_Day
    {
       // public string Name { get; set; }

      //  public string DeptName { get; set; }

        public string AttId { get; set; }


       
    }


}