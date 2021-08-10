using Coldairarrow.Business.Att;
using Coldairarrow.Entity.Att;
using Coldairarrow.Util;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using AttApi;
using System;

namespace Coldairarrow.Api.Controllers.Att
{
    [Route("/Att/[controller]/[action]")]
    public class Att_MonthController : BaseApiController
    {
        #region DI

        public Att_MonthController(IAtt_MonthBusiness att_MonthBus, IAttDb attDb)
        {
            _att_MonthBus = att_MonthBus;
            _attDb = attDb;
        }

        IAtt_MonthBusiness _att_MonthBus { get; }
        IAttDb _attDb { get; }
        #endregion

        #region 获取

        [HttpPost]
        public async Task<PageResult<Att_Month>> GetDataList(PageInput<DeptMonthDTO> input)
        {

            return await _att_MonthBus.GetDataListAsync(input);
        }

        [HttpPost]
        public async Task<Att_Month> GetTheData(IdMonthDto input)
        {
            return await _att_MonthBus.GetTheDataAsync(input.id);
        }
        [HttpPost]
        public async Task<List<AttMonthInfo>> GetAttMonthList(IdMonthListDto input)
        {
            return await Task.Run(() =>
              {
                  var attMonthList = new List<AttMonthInfo>();
                  input.monthList.ForEach(m =>
                  {
                      attMonthList.Add(_attDb.GetMonthInfo(input.attId, DateTime.Parse(m + "-01")));
                  });
                  return attMonthList;
              });

        }

        #endregion

        #region 提交


        #endregion
    }
}