using Coldairarrow.Entity.BaseInfo;
using Coldairarrow.Util;
using EFCore.Sharding;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace Coldairarrow.Business.BaseInfo
{
    public class BaseInfo_ProcessBusiness : BaseBusiness<BaseInfo_Process>, IBaseInfo_ProcessBusiness, ITransientDependency
    {
        public BaseInfo_ProcessBusiness(IDbAccessor db)
            : base(db)
        {
        }

        #region 外部接口

        public async Task<List<BaseInfo_Process>> GetAllDataListAsync()
        {
            return await GetIQueryable().ToListAsync();
        }

        public async Task<PageResult<BaseInfo_Process>> GetDataListAsync(PageInput<ConditionDTO> input)
        {
            var q = GetIQueryable();
            var where = LinqHelper.True<BaseInfo_Process>();
            var search = input.Search;

            //筛选
            if (!search.Condition.IsNullOrEmpty() && !search.Keyword.IsNullOrEmpty())
            {
                var newWhere = DynamicExpressionParser.ParseLambda<BaseInfo_Process, bool>(
                    ParsingConfig.Default, false, $@"{search.Condition}.Contains(@0)", search.Keyword);
                where = where.And(newWhere);
            }

            return await q.Where(where).GetPageResultAsync(input);
        }

        public async Task<BaseInfo_Process> GetTheDataAsync(string id)
        {
            return await GetEntityAsync(id);
        }

        public async Task AddDataAsync(BaseInfo_Process data)
        {
            await InsertAsync(data);
        }

        public async Task UpdateDataAsync(BaseInfo_Process data)
        {
            await UpdateAsync(data);
        }

        public async Task DeleteDataAsync(List<string> ids)
        {
            await DeleteAsync(ids);
        }



        #endregion

        #region 私有成员

        #endregion
    }
}