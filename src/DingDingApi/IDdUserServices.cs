
using static DingTalk.Api.Response.OapiV2UserGetResponse;

namespace DingDingApi
{
    public interface IDdUserServices
    {
        string GetDdIdByAuthCode(string authCode);

        UserGetResponseDomain GetUserInfo(string ddId);

        string GetAvatar(string ddid);

    }
}