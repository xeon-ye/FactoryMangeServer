using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coldairarrow.Entity.BaseInfo
{
    /// <summary>
    /// 针型表
    /// </summary>
    [Table("BaseInfo_Needle")]
    public class BaseInfo_Needle
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
        /// NeedleName
        /// </summary>
        public String NeedleName { get; set; }

    }
}