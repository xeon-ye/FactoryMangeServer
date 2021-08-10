using Coldairarrow.Business.Base_Manage;
using Coldairarrow.IBusiness;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NSwag.Annotations;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Coldairarrow.Entity.Base_Manage;
using System.Linq;
using Coldairarrow.Util;
using DingDingApi;
namespace Coldairarrow.Api.Controllers.Base_Manage
{
    /// <summary>
    /// 首页控制器
    /// </summary>
    [Route("/Base_Manage/[controller]/[action]")]
    [OpenApiTag("主页")]
    public class HomeController : BaseApiController
    {
        readonly IHomeBusiness _homeBus;
        readonly IPermissionBusiness _permissionBus;
        readonly IBase_UserBusiness _userBus;
        readonly IOperator _operator;
        readonly IBase_DepartmentBusiness _deptBus;
        readonly IDdUserServices _ddUserServices;
        readonly IBase_StaffBusiness _staffBusiness;

        private readonly JwtOptions _jwtOptions;
        public HomeController(
            IHomeBusiness homeBus,
            IPermissionBusiness permissionBus,
            IBase_UserBusiness userBus,
            IOperator @operator,
            IBase_DepartmentBusiness deputBus,
            IOptions<JwtOptions> jwtOptions,
            IDdUserServices ddUserServices,
            IBase_StaffBusiness staffBusiness


            )
        {
            _homeBus = homeBus;
            _permissionBus = permissionBus;
            _userBus = userBus;
            _operator = @operator;
            _jwtOptions = jwtOptions.Value;
            _deptBus = deputBus;
            _ddUserServices = ddUserServices;
            _staffBusiness = staffBusiness;

        }
        [HttpGet]

        public string GetTset(string name)
        {

            return "hello world!" + name;
        }
        /// <summary>
        /// 订订免登码认证
        /// </summary>
        /// <param name="authCode"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> DdLogin(authCodeDTO authCode)
        {
            return await Task.Run(() =>
             {
                 Console.WriteLine("钉钉登录:" + authCode.authCode);
                 var ddid = _ddUserServices.GetDdIdByAuthCode(authCode.authCode);
                 Console.WriteLine("取得DDid:" + ddid);
                 if (ddid == null) return (ActionResult)BadRequest();
                 var user = _userBus.GetUserIdByDdid(ddid);
                 Console.WriteLine("取得用户:" + user.ToJson());

                 var Avatar = _ddUserServices.GetAvatar(ddid);
                 var Staff = _staffBusiness.GetStaffByDdId(ddid);
                 var jwtToken = TokenHelper.GetToen(user.Id, _jwtOptions.Secret, _jwtOptions.AccessExpireHours);
                 return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(jwtToken), avatar = Avatar, name = Staff.StaffName, deptName = Staff.DeptName, ddId = ddid, attId = Staff.AttId });
      
             });

        }

        /// <summary>
        /// 用户登录(获取token)
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<string> SubmitLogin(LoginInputDTO input)
        {
           // Console.WriteLine("user login:"+ input.userName);
            var userId = await _homeBus.SubmitLoginAsync(input);
            var jwtToken = TokenHelper.GetToen(userId, _jwtOptions.Secret, _jwtOptions.AccessExpireHours);
            return new JwtSecurityTokenHandler().WriteToken(jwtToken);
        }
        [HttpGet]
        public async Task<List<SelectOption>> GetCurrentUserDeptList()
        {

            var u = await _userBus.GetCurrentUser();
            var deptlist = _deptBus.GetDeptList();
            List<SelectOption> selectRertunObjects = new List<SelectOption>();
            u.DepartmentId.Split(',').ToList().ForEach(p =>
            {
                var deptname = deptlist.First(d => d.Id == p) != null ? deptlist.First(d => d.Id == p).Name : "";
                selectRertunObjects.Add(new SelectOption
                {
                    value = p,
                    label = deptname
                });
            });

            return selectRertunObjects;
        }

        [HttpPost]
        public async Task ChangePwd(ChangePwdInputDTO input)
        {
            await _homeBus.ChangePwdAsync(input);


        }

        [HttpPost]
        public async Task<object> GetOperatorInfo()
        {
            var theInfo = await _userBus.GetTheDataAsync(_operator.UserId);
            var permissions = await _permissionBus.GetUserPermissionValuesAsync(_operator.UserId);
            var resObj = new
            {
                UserInfo = theInfo,
                Permissions = permissions
            };

            return resObj;
        }

        [HttpPost]
        public async Task<List<Base_ActionDTO>> GetOperatorMenuList()
        {
            return await _permissionBus.GetUserMenuListAsync(_operator.UserId);
        }
    }
}