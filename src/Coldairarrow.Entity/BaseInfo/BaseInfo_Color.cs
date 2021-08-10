using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coldairarrow.Entity.BaseInfo
{
    /// <summary>
    /// 颜色表
    /// </summary>
    [Table("BaseInfo_Color")]
    public class BaseInfo_Color
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
        /// 颜色名
        /// </summary>
        public String Name { get; set; }

        /// <summary>
        /// 英文名
        /// </summary>
        public String EnName { get; set; }

    }
}