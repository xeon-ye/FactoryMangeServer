using Dapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttApi
{
   
    public class CheckInOutEntity
    {
        public string Name { get; set; }

        public string Badgenumber { get; set; }

        public int UserId { get; set; }

        public DateTime CheckTime { get; set; }

        public char? CheckType { get; set; }

        public int? VerifyCode { get; set; }

        public string? SensorId { get; set; }

        public string? Memoinfo { get; set; }

        public int? WorkCode { get; set; }

        public string? SN { get; set; }

        public int? UserExtFmt { get; set; }
    }
}
