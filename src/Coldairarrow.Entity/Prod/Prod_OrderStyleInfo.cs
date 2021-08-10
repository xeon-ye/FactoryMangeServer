using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coldairarrow.Entity.Prod
{
    /// <summary>
    /// Prod_OrderStyleInfo
    /// </summary>
    [Table("Prod_OrderStyleInfo")]
    public class Prod_OrderStyleInfo
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
        /// 订单ID
        /// </summary>
        public String OrderId { get; set; }

        /// <summary>
        /// 款号ID
        /// </summary>
        public String StyleId { get; set; }

        /// <summary>
        /// 尺码
        /// </summary>
        public String Size { get; set; }

        /// <summary>
        /// 颜色
        /// </summary>
        public String Color { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public Int32 Qty { get; set; }

        /// <summary>
        /// 国家
        /// </summary>
        public String Country { get; set; }

        /// <summary>
        /// 港口
        /// </summary>
        public String Port { get; set; }

        /// <summary>
        /// 装运日期
        /// </summary>
        public DateTime? ShippingData { get; set; }

    }
}