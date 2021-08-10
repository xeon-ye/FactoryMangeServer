using Coldairarrow.Business.BaseInfo;
using Coldairarrow.Entity.BaseInfo;
using Coldairarrow.Util;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coldairarrow.Api.Controllers.BaseInfo
{
    [Route("/BaseInfo/[controller]/[action]")]
    public class BaseInfo_ProcessController : BaseApiController
    {
        #region DI

        public BaseInfo_ProcessController(IBaseInfo_ProcessBusiness baseInfo_ProcessBus)
        {
            _baseInfo_ProcessBus = baseInfo_ProcessBus;
        }

        IBaseInfo_ProcessBusiness _baseInfo_ProcessBus { get; }

        #endregion

        #region 获取


        [HttpPost]
        public async Task<List<BaseInfo_Process>> GetAllDataList()
        {
            return await _baseInfo_ProcessBus.GetAllDataListAsync();
        }


        [HttpPost]
        public async Task<PageResult<BaseInfo_Process>> GetDataList(PageInput<ConditionDTO> input)
        {
            return await _baseInfo_ProcessBus.GetDataListAsync(input);
        }

        [HttpPost]
        public async Task<BaseInfo_Process> GetTheData(IdInputDTO input)
        {
            return await _baseInfo_ProcessBus.GetTheDataAsync(input.id);
        }

        #endregion

        #region 提交

        [HttpPost]
        public async Task SaveData(BaseInfo_Process data)
        {
            if (data.Id.IsNullOrEmpty())
            {
                InitEntity(data);

                await _baseInfo_ProcessBus.AddDataAsync(data);
            }
            else
            {
                await _baseInfo_ProcessBus.UpdateDataAsync(data);
            }
        }

        [HttpPost]
        public async Task DeleteData(List<string> ids)
        {
            await _baseInfo_ProcessBus.DeleteDataAsync(ids);
        }

        #endregion
    }
}