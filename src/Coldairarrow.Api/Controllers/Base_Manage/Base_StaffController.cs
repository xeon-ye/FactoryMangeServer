using Coldairarrow.Business.Base_Manage;
using Coldairarrow.Entity.Base_Manage;
using Coldairarrow.Util;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using System.Collections.Generic;
using System.Threading.Tasks;
using DingDingApi;
using AttApi;
using System.Linq;
namespace Coldairarrow.Api.Controllers.Base_Manage
{
    /// <summary>
    /// 员工管理
    /// </summary>
    [Route("/Base_Manage/[controller]/[action]")]
    [OpenApiTag("员工管理")]
    public class Base_StaffController : BaseApiController
    {
        #region DI

        public Base_StaffController(IBase_StaffBusiness base_StaffBus, IDdDeptStaffSerivces ddDeptStaffSerivces, IAttDb attdb)
        {
            _base_StaffBus = base_StaffBus;
            _ddDeptStaffSerivces = ddDeptStaffSerivces;
            _attdb = attdb;

        }
        IAttDb _attdb;
        IBase_StaffBusiness _base_StaffBus { get; }
        IDdDeptStaffSerivces _ddDeptStaffSerivces { get; }
        #endregion

        #region 获取

        [HttpPost]
        public async Task<PageResult<Base_Staff>> GetDataList(PageInput<ConditionDTO> input)
        {
            return await _base_StaffBus.GetDataListAsync(input);
        }

        [HttpPost]
        public async Task<Base_Staff> GetTheData(IdInputDTO input)
        {
            return await _base_StaffBus.GetTheDataAsync(input.id);
        }

        #endregion

        /// <summary>
        /// 从钉钉同步用户
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> SyncStaffByDingDing()
        {
            return await Task.Run(() =>
             {
                 var staffs = _ddDeptStaffSerivces.GetStaffs();
                 _base_StaffBus.UpDataOrAddStaffs(staffs);
                 return Ok();
             });

        }

        /// <summary>
        /// 同步考勤ID
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> SyncStaffAttID()
        {
            return await Task.Run(() =>
            {
                var attlist = _attdb.GetUserList();
                var stafflist = _base_StaffBus.GetAllDataListAsync().Result;

                //添加考勤信息
                stafflist.ForEach(s =>
                {
                    var slist = attlist.Where(p => p.Name.Trim() == s.StaffName);
                    if (slist.Count() > 0)
                    {
                        s.AttId = slist.First().BadgeNumber;
                        slist.ForEach(sl =>
                        {
                            if (sl.Pager == s.Tel || sl.Ophone == s.Tel) s.AttId = sl.BadgeNumber;
                        });
                    }
                });

                //更新到数据库
                //stafflist.ForEach(p =>
                //{
                //    _base_StaffBus.UpdateDataAsync(p);
                //});
                _base_StaffBus.UpDataList(stafflist.ToList());
                return Ok();

            });

        }

        public ActionResult GetDdStaffInfo(string ddid)
        {

            if (ddid == "") return BadRequest();
            return null;
        }

        #region 提交

        [HttpPost]
        public async Task SaveData(Base_Staff data)
        {
            if (data.Id.IsNullOrEmpty())
            {
                InitEntity(data);

                await _base_StaffBus.AddDataAsync(data);
            }
            else
            {
                await _base_StaffBus.UpdateDataAsync(data);
            }
        }

        [HttpPost]
        public async Task DeleteData(List<string> ids)
        {
            await _base_StaffBus.DeleteDataAsync(ids);
        }

        #endregion
    }
}