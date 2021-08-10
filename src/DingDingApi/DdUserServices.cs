using DingTalk.Api;
using DingTalk.Api.Request;
using DingTalk.Api.Response;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DingTalk.Api.Response.OapiV2UserGetResponse;

namespace DingDingApi
{
    public class DdUserServices : IDdUserServices
    {
        private IDdServices _ddServices;
        ILogger<DdUserServices> _logger;
        public DdUserServices(IDdServices ddServices, ILogger<DdUserServices> logger)
        {
            _ddServices = ddServices;
            _logger = logger;
        }

        public string GetAvatar(string ddid)
        {
            var user = GetUserInfo(ddid);
            if (user != null) return user.Avatar;
            return "";
        }

        public string GetDdIdByAuthCode(string authCode)
        {
            IDingTalkClient client = new DefaultDingTalkClient("https://oapi.dingtalk.com/user/getuserinfo");
            OapiUserGetuserinfoRequest req = new OapiUserGetuserinfoRequest();
            req.Code = authCode;
            req.SetHttpMethod("GET");
            OapiUserGetuserinfoResponse rsp = client.Execute(req, _ddServices.AccessToken);

            fastJSON.JSON.ToJSON(rsp);

            if (rsp != null) {  return rsp.Userid; }
            return "";

        }

        public UserGetResponseDomain GetUserInfo(string ddId)
        {
            IDingTalkClient client = new DefaultDingTalkClient("https://oapi.dingtalk.com/topapi/v2/user/get");
            OapiV2UserGetRequest req = new OapiV2UserGetRequest();
            req.Userid = ddId;
            req.Language = ("zh_CN");
            OapiV2UserGetResponse rsp = client.Execute(req, _ddServices.AccessToken);
            if (rsp.Result != null) return rsp.Result;
            throw new NotImplementedException();
        }


    }
}
