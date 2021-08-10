using System;
using System.Collections.Generic;

namespace Coldairarrow.Util
{
    public class IdInputDTO
    {
        /// <summary>
        /// id
        /// </summary>
        public string id { get; set; }
    }

    public class IdMonthDto : IdInputDTO
    {
        public string attId { get; set; }
        public string month { get; set; }

    }

    public class IdDeptDateDto : IdInputDTO
    {
        public string deptId { get; set; }
        public DateTime declareDate { get; set; }

    }

    public class IdMonthListDto : IdInputDTO
    {
        public string attId { get; set; }
        public List<string> monthList { get; set; }

    }

    
    public class OverTimeDTO
    {
        public string StaffId { get; set; }
        public DateTime? BeginOverTime { get; set; }
        public DateTime? EndOverTime { get; set; }
        public float? DeclareOverTime { get; set; }
        public DateTime? AttDate { get; set; }

        public string? AttRecord { get; set; }

        public DateTime? CheckInTime { get; set; }

        public DateTime? CheckOutTime { get; set; }

        public float? OverTime { get; set; }


    }
}
