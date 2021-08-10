using Coldairarrow.Business.BaseInfo;
using Coldairarrow.Entity.BaseInfo;
using Coldairarrow.Util;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coldairarrow.Api.Controllers.BaseInfo
{
    [Route("/BaseInfo/[controller]/[action]")]
    public class BaseInfo_ClientController : BaseApiController
    {
        #region DI

        public BaseInfo_ClientController(IBaseInfo_ClientBusiness baseInfo_ClientBus)
        {
            _baseInfo_ClientBus = baseInfo_ClientBus;
        }

        IBaseInfo_ClientBusiness _baseInfo_ClientBus { get; }

        #endregion

        #region 获取

        [HttpPost]
        public async Task<PageResult<BaseInfo_Client>> GetDataList(PageInput<ConditionDTO> input)
        {
            return await _baseInfo_ClientBus.GetDataListAsync(input);
        }

        [HttpPost]
        public async Task<BaseInfo_Client> GetTheData(IdInputDTO input)
        {
            return await _baseInfo_ClientBus.GetTheDataAsync(input.id);
        }

        #endregion

        #region 提交

        [HttpPost]
        public async Task SaveData(BaseInfo_Client data)
        {
            if (data.Id.IsNullOrEmpty())
            {
                InitEntity(data);

                await _baseInfo_ClientBus.AddDataAsync(data);
            }
            else
            {
                await _baseInfo_ClientBus.UpdateDataAsync(data);
            }
        }

        [HttpPost]
        public async Task DeleteData(List<string> ids)
        {
            await _baseInfo_ClientBus.DeleteDataAsync(ids);
        }

        #endregion
    }
}