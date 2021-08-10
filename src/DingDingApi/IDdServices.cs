using Coldairarrow.Entity.Base_Manage;
using System.Collections.Generic;

namespace DingDingApi
{
    public interface IDdServices
    {
        string AccessToken { get; }

        IDdOptions Options { get; }
    }
}