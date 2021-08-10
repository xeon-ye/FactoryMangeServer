using Coldairarrow.Business.Base_Manage;
using Coldairarrow.Entity.Base_Manage;
using Coldairarrow.Util;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using System.Collections.Generic;
using System.Threading.Tasks;
using DingDingApi;

namespace Coldairarrow.Api.Controllers.Base_Manage
{
    /// <summary>
    /// 部门
    /// </summary>
    [Route("/Base_Manage/[controller]/[action]")]
    [OpenApiTag("部门")]
    public class Base_DepartmentController : BaseApiController
    {
        #region DI

        public Base_DepartmentController(IBase_DepartmentBusiness departmentBus, IDdDeptStaffSerivces ddServices)
        {
            _departmentBus = departmentBus;
            _ddDeptStaffServices = ddServices;
        }

        IBase_DepartmentBusiness _departmentBus { get; }
        IDdDeptStaffSerivces _ddDeptStaffServices { get; }

        #endregion

        #region 获取



        [HttpPost]
        public async Task<Base_Department> GetTheData(IdInputDTO input)
        {
            return await _departmentBus.GetTheDataAsync(input.id) ?? new Base_Department();
        }

        [HttpPost]
        public async Task<List<Base_DepartmentTreeDTO>> GetTreeDataList(DepartmentsTreeInputDTO input)
        {
            return await _departmentBus.GetTreeDataListAsync(input);
        }

        #endregion
        [HttpGet]
        public async Task<List<Base_Department>> GetDataList()
        {

            return await Task.Run(() =>
             {
                 return _departmentBus.GetDeptList();
             });

        }

        #region 提交



        [HttpPost]
        public async Task SaveData(Base_Department theData)
        {
            if (theData.Id.IsNullOrEmpty())
            {
                InitEntity(theData);

                await _departmentBus.AddDataAsync(theData);
            }
            else
            {
                await _departmentBus.UpdateDataAsync(theData);
            }
        }

        [HttpPost]
        public async Task DeleteData(List<string> ids)
        {
            await _departmentBus.DeleteDataAsync(ids);
        }

        #endregion

        #region 执行
        /// <summary>
        /// 从钉钉同步部门列表
        /// </summary>
        /// <returns></returns>
        public ActionResult SyncDeptFromDingDing()
        {
            var depts = _ddDeptStaffServices.GetDepts();
            _departmentBus.SaveDepts(depts);

            return Ok("hello");
        }
        #endregion
    }
}