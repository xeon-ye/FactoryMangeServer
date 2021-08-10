using Coldairarrow.Entity.Base_Manage;
using DingTalk.Api;
using DingTalk.Api.Request;
using DingTalk.Api.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DingDingApi
{
    public class DdDeptStaffSerivces : IDdDeptStaffSerivces
    {
        private IDdServices _ddServices;
        public DdDeptStaffSerivces(IDdServices ddServices)
        {
            _ddServices = ddServices;
        }

        /// <summary>
        /// 从钉钉获取部门列表并转成本地部门
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Base_Department> GetDepts()
        {

            List<Base_Department> depts = new();
            GetDept(1L, depts);
            return depts;

        }

        private void GetDept(long id, List<Base_Department> depts)
        {
            IDingTalkClient client = new DefaultDingTalkClient("https://oapi.dingtalk.com/topapi/v2/department/listsub");
            OapiV2DepartmentListsubRequest req = new();
            req.DeptId = id;
            req.Language = "zh_CN";
            OapiV2DepartmentListsubResponse rsp = client.Execute(req, _ddServices.AccessToken);
            if (rsp.Result == null) return;
            rsp.Result.ForEach(p =>
            {
                depts.Add(new Base_Department
                {
                    Id = p.DeptId.ToString(),
                    DdDeptId = p.DeptId,
                    Name = p.Name,
                    ParentId = p.ParentId.ToString(),
                });
                GetDept(p.DeptId, depts);

            });
        }

        /// <summary>
        /// 返回钉钉所有用户
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Base_Staff> GetStaffs()
        {
            List<Base_Staff> staffs = new();

            Parallel.ForEach(GetDepts(), dept =>
            {
                GetDeptStaffs(dept, staffs);
            });

            //GetDepts().ToList().ForEach(dept =>
            //{
            //    GetDeptStaffs(dept, staffs);

            //});

            return staffs;
        }
        private string ListLongToString(List<long> l)
        {
            string str = "";
            l.ForEach(p =>
            {
                if (str == "") str = p.ToString();
                else str = str + "," + p;

            });
            return str;

        }

        public void GetDeptStaffs(Base_Department dept, List<Base_Staff> staffs)
        {
            IDingTalkClient client = new DefaultDingTalkClient("https://oapi.dingtalk.com/topapi/v2/user/list");
            OapiV2UserListRequest req = new OapiV2UserListRequest();
            req.DeptId = dept.DdDeptId;
            req.Cursor = 0L;
            req.Size = 20L;
            req.ContainAccessLimit = false;
            req.Language = ("zh_CN");
            //bool isHaveMore = true;

            while (true)
            {
                OapiV2UserListResponse rsp = client.Execute(req, _ddServices.AccessToken);
                if (rsp.Result != null && rsp.Result.List != null)
                {
                    rsp.Result.List.ForEach(p =>
                    {
                        staffs.Add(new Base_Staff
                        {
                            Id = p.Userid,
                            CreateTime = DateTime.Now,
                            CreatorId = "dd",
                            DDid = p.Userid,
                            StaffName = p.Name,
                            Tel = p.Mobile,
                            DepartmentList = ListLongToString(p.DeptIdList),
                            DepartmentId = p.DeptIdList[0].ToString(),
                            DeptName = dept.Name
                        });
                        ;
                    });
                    req.Cursor = rsp.Result.NextCursor;
                    if (rsp.Result.NextCursor == 0) break;
                }
                else break;
            }

        }
    }
}
