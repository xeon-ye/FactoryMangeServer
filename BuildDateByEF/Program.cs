using System;
using Coldairarrow.Entity.DAL;
using Coldairarrow.Entity.ERP.BaseInfo;
using Microsoft.EntityFrameworkCore;

namespace BuildDateByEF
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            DbContextOptions<ErpDbContext> op = new DbContextOptions<ErpDbContext>();
           
            
           
            ErpDbContext dc = new ErpDbContext(op);
            dc.processModels.Add(new ProcessModel
            {
                Id = "a001",
                CreateTime = DateTime.Now,
                CreatorId="a0012",
                DeptId="aad",
                DeptName="sugin",
                Name="whj"

            }) ;
            dc.SaveChanges();
            Console.ReadKey();

        }
    }
}
