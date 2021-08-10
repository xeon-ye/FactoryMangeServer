using Coldairarrow.Business.BaseInfo;
using Coldairarrow.Entity.BaseInfo;
using Coldairarrow.Util;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coldairarrow.Api.Controllers.BaseInfo
{
    [Route("/BaseInfo/[controller]/[action]")]
    public class BaseInfo_SizeController : BaseApiController
    {
        #region DI

        public BaseInfo_SizeController(IBaseInfo_SizeBusiness baseInfo_SizeBus)
        {
            _baseInfo_SizeBus = baseInfo_SizeBus;
        }

        IBaseInfo_SizeBusiness _baseInfo_SizeBus { get; }

        #endregion

        #region 获取
        [HttpPost]
        public async Task<List<BaseInfo_Size>> GetAllDataList()
        {
            return await _baseInfo_SizeBus.GetAllDataListAsync();
        }


        [HttpPost]
        public async Task<PageResult<BaseInfo_Size>> GetDataList(PageInput<ConditionDTO> input)
        {
            return await _baseInfo_SizeBus.GetDataListAsync(input);
        }

        [HttpPost]
        public async Task<BaseInfo_Size> GetTheData(IdInputDTO input)
        {
            return await _baseInfo_SizeBus.GetTheDataAsync(input.id);
        }

        #endregion

        #region 提交

        [HttpPost]
        public async Task SaveData(BaseInfo_Size data)
        {
            if (data.Id.IsNullOrEmpty())
            {
                InitEntity(data);

                await _baseInfo_SizeBus.AddDataAsync(data);
            }
            else
            {
                await _baseInfo_SizeBus.UpdateDataAsync(data);
            }
        }

        [HttpPost]
        public async Task DeleteData(List<string> ids)
        {
            await _baseInfo_SizeBus.DeleteDataAsync(ids);
        }

        #endregion
    }
}