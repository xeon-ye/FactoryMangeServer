using Coldairarrow.Business.BaseInfo;
using Coldairarrow.Entity.BaseInfo;
using Coldairarrow.Util;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coldairarrow.Api.Controllers.BaseInfo
{
    [Route("/BaseInfo/[controller]/[action]")]
    public class BaseInfo_ProofTypeController : BaseApiController
    {
        #region DI

        public BaseInfo_ProofTypeController(IBaseInfo_ProofTypeBusiness baseInfo_ProofTypeBus)
        {
            _baseInfo_ProofTypeBus = baseInfo_ProofTypeBus;
        }

        IBaseInfo_ProofTypeBusiness _baseInfo_ProofTypeBus { get; }

        #endregion

        #region 获取
        [HttpPost]
        public async Task<List<BaseInfo_ProofType>> GetAllDataList()
        {
            return await _baseInfo_ProofTypeBus.GetAllDataListAsync();
        }


        [HttpPost]
        public async Task<PageResult<BaseInfo_ProofType>> GetDataList(PageInput<ConditionDTO> input)
        {
            return await _baseInfo_ProofTypeBus.GetDataListAsync(input);
        }

        [HttpPost]
        public async Task<BaseInfo_ProofType> GetTheData(IdInputDTO input)
        {
            return await _baseInfo_ProofTypeBus.GetTheDataAsync(input.id);
        }

        #endregion

        #region 提交

        [HttpPost]
        public async Task SaveData(BaseInfo_ProofType data)
        {
            if (data.Id.IsNullOrEmpty())
            {
                InitEntity(data);

                await _baseInfo_ProofTypeBus.AddDataAsync(data);
            }
            else
            {
                await _baseInfo_ProofTypeBus.UpdateDataAsync(data);
            }
        }

        [HttpPost]
        public async Task DeleteData(List<string> ids)
        {
            await _baseInfo_ProofTypeBus.DeleteDataAsync(ids);
        }

        #endregion
    }
}