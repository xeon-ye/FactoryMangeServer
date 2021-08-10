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
    public class Att_DayController : BaseApiController
    {
        #region DI

        public Att_DayController(IAtt_DayBusiness att_DayBus)
        {
            _att_DayBus = att_DayBus;

        }

        IAtt_DayBusiness _att_DayBus { get; }

        #endregion

        #region 获取

        [HttpPost]
        public async Task<PageResult<Att_Day>> GetDataList(Att_DayPageInput input)
        {
            return await _att_DayBus.GetDataListAsync(input);
        }

        [HttpPost]
        public async Task<Att_Day> GetTheData(IdInputDTO input)
        {
            return await _att_DayBus.GetTheDataAsync(input.id);
        }


        [HttpPost]
        public async Task<List<Att_Day>> GetTheDayList(IdMonthDto input)
        {
            return await _att_DayBus.GetTheDayList(input);
        }

        [HttpPost]
        public ActionResult<List<AttDayReturnEntiry>> GetDeptAttList(IdDeptDateDto input)
        {
            return Ok(_att_DayBus.GetDeptAttDayList(input));


        }
        #endregion

        #region 提交
        [HttpPost]
        public async Task SaveDeclareOverTime(List<Att_Day> input)
        {
            await Task.Run(() =>
             {
                 input.ForEach(p =>
                           {
                               if (p.Id.IsNullOrEmpty())
                               {
                                   InitEntity(p);
                                   _att_DayBus.AddData(p);

                               }
                               else
                               {
                                   _att_DayBus.UpdateData(p);
                               }
                           });

             });


        }


        [HttpPost]
        public async Task SaveData(Att_Day data)
        {
            if (data.Id.IsNullOrEmpty())
            {
                InitEntity(data);

                await _att_DayBus.AddDataAsync(data);
            }
            else
            {
                await _att_DayBus.UpdateDataAsync(data);
            }
        }

        [HttpPost]
        public async Task DeleteData(List<string> ids)
        {
            await _att_DayBus.DeleteDataAsync(ids);
        }

        #endregion
    }
}