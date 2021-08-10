using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coldairarrow.Entity.Base_Manage
{
    /// <summary>
    /// Base_Staff
    /// </summary>
    [Table("Base_Staff")]
    public class Base_Staff
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
        /// Deleted
        /// </summary>
        public Boolean Deleted { get; set; }

        /// <summary>
        /// 绑定用户ID
        /// </summary>
        public String UserId { get; set; }

        /// <summary>
        /// 钉钉ID
        /// </summary>
        public String DDid { get; set; }


        /// <summary>
        /// 考勤号
        /// </summary>
        public string AttId { get; set; }

        /// <summary>
        /// 员工姓名
        /// </summary>
        public String StaffName { get; set; }


        /// <summary>
        /// 部门ID
        /// </summary>
        public String DepartmentId { get; set; }

        /// <summary>
        /// 所属部门列表
        /// </summary>
        public string DepartmentList { get; set; }

        /// <summary>
        /// 部门名
        /// </summary>
        public string DeptName { get; set; }

        /// <summary>
        /// 手机
        /// </summary>
        public String Tel { get; set; }

    }
}