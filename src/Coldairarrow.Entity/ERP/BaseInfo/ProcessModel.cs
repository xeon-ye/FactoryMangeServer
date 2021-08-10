using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Coldairarrow.Entity.ERP;
namespace Coldairarrow.Entity.ERP.BaseInfo
{
    public class ProcessModel : BaseModel
    {

        /// <summary>
        /// 工序名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 所属部门名
        /// </summary>
        public string DeptName { get; set; }

        /// <summary>
        /// 所属部门ID
        /// </summary>
        public string DeptId { get; set; }
        

    }
}
