using System;

namespace DingDingApi
{
    public class DdOptions : IDdOptions
    {
        /// <summary>
        /// 应用程序ID
        /// </summary>
        public string AgentId { get; set; }
        /// <summary>
        /// 应用程序Key
        /// </summary>
        public string AppKey { get; set; }
        /// <summary>
        /// 安全密码
        /// </summary>
        public string AppSecret { get; set; }
        /// <summary>
        ///   回调地址
        /// </summary>
        public string CallBalkUrl { get; set; }

    }
}
