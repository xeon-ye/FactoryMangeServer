using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DingDingApi
{
    public class DdUser
    {
        public string UserId { get; set; }

        public string Name { get; set; }

        public string Avatar { get; set; }

        public string ManagerUserId { get; set; }

        public string JobNumber { get; set; }

        public string Email { get; set; }

        public bool IsAdmin { get; set; }


    }
}
