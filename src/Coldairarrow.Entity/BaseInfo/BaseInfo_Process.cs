using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coldairarrow.Entity.BaseInfo
{
    /// <summary>
    /// 工序表
    /// </summary>
    [Table("BaseInfo_Process")]
    public class BaseInfo_Process
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
        /// 工序名
        /// </summary>
        public String ProcessName { get; set; }

        /// <summary>
        /// 部门ID
        /// </summary>
        public String DeptId { get; set; }

        /// <summary>
        /// 所属部门名
        /// </summary>
        public String DeptName { get; set; }

        /// <summary>
        /// 是否检验工序
        /// </summary>
        public bool IsCheckProcess { get; set; }

    }
}