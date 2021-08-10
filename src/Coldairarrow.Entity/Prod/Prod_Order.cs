using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coldairarrow.Entity.Prod
{
    /// <summary>
    /// Prod_Order
    /// </summary>
    [Table("Prod_Order")]
    public class Prod_Order
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
        /// 订单编号
        /// </summary>
        public String OrderNo { get; set; }

        /// <summary>
        /// 客户ID
        /// </summary>
        public String ClientId { get; set; }

        /// <summary>
        /// 客户名
        /// </summary>
        public String ClientName { get; set; }

        /// <summary>
        /// 交期
        /// </summary>
        public DateTime? DeliveryTime { get; set; }

        /// <summary>
        /// 订单日期
        /// </summary>
        public DateTime? OrderDate { get; set; }

        /// <summary>
        /// 订单金额
        /// </summary>
        public Double? OrderAmount { get; set; }

        /// <summary>
        /// 订单数量
        /// </summary>
        public Int32? OrderQty { get; set; }

        /// <summary>
        /// 跟单员ID
        /// </summary>
        public String OrderSuperId { get; set; }

        /// <summary>
        /// 跟单姓名
        /// </summary>
        public String OrderSuperName { get; set; }

        /// <summary>
        /// 装运日期
        /// </summary>
        public DateTime? ShippingDate { get; set; }

    }
}