using DingDingApi;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using AttApi;
namespace Coldairarrow.Api.DependencyInjection
{
    public class DiList
    {
        /// <summary>
        /// 手工注入自定义的业务类
        /// </summary>
        /// <param name="services"></param>
        public static  void DoDiList(IServiceCollection services)
        {

            //注入钉钉服务器
            services.AddSingleton<IDdServices, DdServices>();
            services.AddScoped<IDdDeptStaffSerivces, DdDeptStaffSerivces>();
            services.AddScoped<IDdUserServices, DdUserServices>();
            
            services.AddScoped<IAttDb, AttDb>();

                   
        }
    }
}
