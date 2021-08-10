using Coldairarrow.Business.Prod;
using Coldairarrow.Entity.Prod;
using Coldairarrow.Util;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coldairarrow.Api.Controllers.Prod
{
    [Route("/Prod/[controller]/[action]")]
    public class Prod_StyleController : BaseApiController
    {
        #region DI

        public Prod_StyleController(IProd_StyleBusiness prod_StyleBus)
        {
            _prod_StyleBus = prod_StyleBus;
        }

        IProd_StyleBusiness _prod_StyleBus { get; }

        #endregion

        #region 获取

        [HttpPost]
        public async Task<PageResult<Prod_Style>> GetDataList(PageInput<ConditionDTO> input)
        {
            return await _prod_StyleBus.GetDataListAsync(input);
        }

        [HttpPost]
        public async Task<Prod_Style> GetTheData(IdInputDTO input)
        {
            return await _prod_StyleBus.GetTheDataAsync(input.id);
        }

        #endregion

        #region 提交

        [HttpPost]
        public async Task<Prod_Style> SaveData(Prod_Style data)
        {
            if (data.Id.IsNullOrEmpty())
            {
                InitEntity(data);

                await _prod_StyleBus.AddDataAsync(data);
            }
            else
            {
                await _prod_StyleBus.UpdateDataAsync(data);
            }
            return data;
        }

        [HttpPost]
        public async Task DeleteData(List<string> ids)
        {
            await _prod_StyleBus.DeleteDataAsync(ids);
        }

        #endregion
    }
}