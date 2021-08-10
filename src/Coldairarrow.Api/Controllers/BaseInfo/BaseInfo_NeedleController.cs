using Coldairarrow.Business.BaseInfo;
using Coldairarrow.Entity.BaseInfo;
using Coldairarrow.Util;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coldairarrow.Api.Controllers.BaseInfo
{
    [Route("/BaseInfo/[controller]/[action]")]
    public class BaseInfo_NeedleController : BaseApiController
    {
        #region DI

        public BaseInfo_NeedleController(IBaseInfo_NeedleBusiness baseInfo_NeedleBus)
        {
            _baseInfo_NeedleBus = baseInfo_NeedleBus;
        }

        IBaseInfo_NeedleBusiness _baseInfo_NeedleBus { get; }
      
        #endregion

        #region 获取
        [HttpPost]
        public async Task<List<BaseInfo_Needle>> GetAllDataList()
        {
            return await _baseInfo_NeedleBus.GetAllDataListAsync();
        }

        [HttpPost]
        public async Task<PageResult<BaseInfo_Needle>> GetDataList(PageInput<ConditionDTO> input)
        {
            return await _baseInfo_NeedleBus.GetDataListAsync(input);
        }

        [HttpPost]
        public async Task<BaseInfo_Needle> GetTheData(IdInputDTO input)
        {
            return await _baseInfo_NeedleBus.GetTheDataAsync(input.id);
        }

        #endregion

        #region 提交

        [HttpPost]
        public async Task SaveData(BaseInfo_Needle data)
        {
            if (data.Id.IsNullOrEmpty())
            {
                InitEntity(data);

                await _baseInfo_NeedleBus.AddDataAsync(data);
            }
            else
            {
                await _baseInfo_NeedleBus.UpdateDataAsync(data);
            }
        }

        [HttpPost]
        public async Task DeleteData(List<string> ids)
        {
            await _baseInfo_NeedleBus.DeleteDataAsync(ids);
        }

        #endregion
    }
}