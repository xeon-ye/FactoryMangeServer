using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttApi
{
 
    public class UserInfoEntity
    {
        public int UserId { get; set; }

        public string BadgeNumber { get; set; }

        public string SSN { get; set; }

        public string Name { get; set; }

        public string Pager{get;set;}

        public string Ophone { get; set; }

        public int DefaultDeptId { get; set; } 

        public string DeptName { get; set; }

    }
}
