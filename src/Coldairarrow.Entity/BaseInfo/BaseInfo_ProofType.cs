using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coldairarrow.Entity.BaseInfo
{
    /// <summary>
    /// 打样类别
    /// </summary>
    [Table("BaseInfo_ProofType")]
    public class BaseInfo_ProofType
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
        /// 打样类别
        /// </summary>
        public String ProofName { get; set; }

        /// <summary>
        /// 打样价格
        /// </summary>
        public Double ProofPrice { get; set; }

    }
}