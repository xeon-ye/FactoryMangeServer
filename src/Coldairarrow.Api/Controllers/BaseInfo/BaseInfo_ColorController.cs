using Coldairarrow.Business.BaseInfo;
using Coldairarrow.Entity.BaseInfo;
using Coldairarrow.Util;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coldairarrow.Api.Controllers.BaseInfo
{
    [Route("/BaseInfo/[controller]/[action]")]
    public class BaseInfo_ColorController : BaseApiController
    {
        #region DI

        public BaseInfo_ColorController(IBaseInfo_ColorBusiness baseInfo_ColorBus)
        {
            _baseInfo_ColorBus = baseInfo_ColorBus;
        }

        IBaseInfo_ColorBusiness _baseInfo_ColorBus { get; }

        #endregion

        #region 获取

        [HttpPost]
        public async Task<PageResult<BaseInfo_Color>> GetDataList(PageInput<ConditionDTO> input)
        {
            return await _baseInfo_ColorBus.GetDataListAsync(input);
        }

        [HttpPost]
        public async Task<BaseInfo_Color> GetTheData(IdInputDTO input)
        {
            return await _baseInfo_ColorBus.GetTheDataAsync(input.id);
        }

        #endregion

        #region 提交

        [HttpPost]
        public async Task SaveData(BaseInfo_Color data)
        {
            if (data.Id.IsNullOrEmpty())
            {
                InitEntity(data);

                await _baseInfo_ColorBus.AddDataAsync(data);
            }
            else
            {
                await _baseInfo_ColorBus.UpdateDataAsync(data);
            }
        }

        [HttpPost]
        public async Task DeleteData(List<string> ids)
        {
            await _baseInfo_ColorBus.DeleteDataAsync(ids);
        }

        #endregion
    }
}