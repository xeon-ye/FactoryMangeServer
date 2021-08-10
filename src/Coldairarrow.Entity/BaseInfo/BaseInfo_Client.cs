using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coldairarrow.Entity.BaseInfo
{
    /// <summary>
    /// 客户表
    /// </summary>
    [Table("BaseInfo_Client")]
    public class BaseInfo_Client
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
        /// 客户名
        /// </summary>
        public String Name { get; set; }

        /// <summary>
        /// 联系人
        /// </summary>
        public String Contact { get; set; }

        /// <summary>
        /// 电话
        /// </summary>
        public String Tel { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        public String Addr { get; set; }

    }
}