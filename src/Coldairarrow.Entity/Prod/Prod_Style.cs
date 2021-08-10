using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coldairarrow.Entity.Prod
{
    /// <summary>
    /// Prod_Style
    /// </summary>
    [Table("Prod_Style")]
    public class Prod_Style
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
        /// 款式ID
        /// </summary>
        public String StyleId { get; set; }

        /// <summary>
        /// 款号
        /// </summary>
        public String StyleNo { get; set; }

        /// <summary>
        /// 客户款号
        /// </summary>
        public String ClientStyleNo { get; set; }

        /// <summary>
        /// 客户ID
        /// </summary>
        public String ClientId { get; set; }

        /// <summary>
        /// 客户名
        /// </summary>
        public String ClientName { get; set; }

        /// <summary>
        /// 克重
        /// </summary>
        public Double? Weight { get; set; }

        /// <summary>
        /// 针型
        /// </summary>
        public String Gauge { get; set; }

        /// <summary>
        /// 搜索字符串
        /// </summary>
        public String SeachStr { get; set; }

        /// <summary>
        /// PicUrl
        /// </summary>
        public String PicUrl { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public String Notes { get; set; }

        /// <summary>
        /// 品牌
        /// </summary>
        public String Brand { get; set; }

        /// <summary>
        /// 季节
        /// </summary>
        public String Season { get; set; }

    }
}