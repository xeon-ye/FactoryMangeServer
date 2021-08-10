using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coldairarrow.Entity.Base_Manage
{
    /// <summary>
    /// KMS
    /// </summary>
    [Table("KMS")]
    public class KMS
    {

        /// <summary>
        /// Id
        /// </summary>
        public String Id { get; set; }

        /// <summary>
        /// CreateTime
        /// </summary>
        public DateTime? CreateTime { get; set; }

        /// <summary>
        /// CreatorId
        /// </summary>
        public String CreatorId { get; set; }

        /// <summary>
        /// KeyName
        /// </summary>
        public String KeyName { get; set; }

        /// <summary>
        /// KeyValue
        /// </summary>
        public Int32? KeyValue { get; set; }

        /// <summary>
        /// BeginKdy
        /// </summary>
        public String BeginKdy { get; set; }

        /// <summary>
        /// Comment
        /// </summary>
        public String Comment { get; set; }

    }
}