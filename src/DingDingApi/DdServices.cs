using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DingTalk.Api;
using DingTalk.Api.Request;
using DingTalk.Api.Response;
using Coldairarrow.Entity.Base_Manage;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;

namespace DingDingApi
{
    public class DdServices : IDdServices
    {
        public  IDdOptions Options { get; }
        private string Access_token;
        private DateTime ExpiresTime;
       private  ILogger<DdServices> _logger;
        public string AccessToken
        {
            get
            {
                if (ExpiresTime < DateTime.Now)
                {
                    GetAccessToken();
                }
                //_logger.LogInformation(DateTime.Now.ToString()+":返回token"+Access_token);
                return Access_token;
            }
        }
        public DdServices(IOptions<DdOptions> options, ILogger<DdServices> logger)
        {
            Options = options.Value;
            _logger = logger;
        }
        private void GetAccessToken()
        {
            IDingTalkClient client = new DefaultDingTalkClient("https://oapi.dingtalk.com/gettoken");
            OapiGettokenRequest request = new OapiGettokenRequest();
            request.Appkey = Options.AppKey;
            request.Appsecret = Options.AppSecret;
            request.SetHttpMethod("GET");
            OapiGettokenResponse response = client.Execute(request);
            Access_token = response.AccessToken;
            ExpiresTime = DateTime.Now.AddSeconds(response.ExpiresIn - 60);
            //_logger.LogInformation(DateTime.Now.ToString()+":从服务器生成token:"+Access_token );
        }
    
    }
}
