using Coldairarrow.Business.Prod;
using Coldairarrow.Entity.Prod;
using Coldairarrow.Util;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coldairarrow.Api.Controllers.Prod
{
    [Route("/Prod/[controller]/[action]")]
    public class Prod_OrderController : BaseApiController
    {
        #region DI

        public Prod_OrderController(IProd_OrderBusiness prod_OrderBus)
        {
            _prod_OrderBus = prod_OrderBus;
        }

        IProd_OrderBusiness _prod_OrderBus { get; }

        #endregion

        #region 获取

        [HttpPost]
        public async Task<PageResult<Prod_Order>> GetDataList(PageInput<ConditionDTO> input)
        {
            return await _prod_OrderBus.GetDataListAsync(input);
        }

        [HttpPost]
        public async Task<Prod_Order> GetTheData(IdInputDTO input)
        {
            return await _prod_OrderBus.GetTheDataAsync(input.id);
        }

        #endregion

        #region 提交

        [HttpPost]
        public async Task SaveData(Prod_Order data)
        {
            if (data.Id.IsNullOrEmpty())
            {
                InitEntity(data);

                await _prod_OrderBus.AddDataAsync(data);
            }
            else
            {
                await _prod_OrderBus.UpdateDataAsync(data);
            }
        }

        [HttpPost]
        public async Task DeleteData(List<string> ids)
        {
            await _prod_OrderBus.DeleteDataAsync(ids);
        }

        #endregion
    }
}