using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Coldairarrow.Entity.ERP.BaseInfo;
namespace Coldairarrow.Entity.DAL
{
    public class ErpDbContext : DbContext
    {
        public ErpDbContext(DbContextOptions<ErpDbContext> options) : base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                "Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=ckd;Data Source=192.168.188.250;Uid=sa;Pwd=sungin998013;");//数据库连接字符串，其中TestDB是数据库名称
        }

        public DbSet<ProcessModel> processModels { get; set; }


    }
}
