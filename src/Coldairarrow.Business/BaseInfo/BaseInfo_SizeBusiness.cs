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
    public class BaseInfo_SizeBusiness : BaseBusiness<BaseInfo_Size>, IBaseInfo_SizeBusiness, ITransientDependency
    {
        public BaseInfo_SizeBusiness(IDbAccessor db)
            : base(db)
        {
        }

        #region 外部接口

        public async Task<PageResult<BaseInfo_Size>> GetDataListAsync(PageInput<ConditionDTO> input)
        {
            var q = GetIQueryable();
            var where = LinqHelper.True<BaseInfo_Size>();
            var search = input.Search;

            //筛选
            if (!search.Condition.IsNullOrEmpty() && !search.Keyword.IsNullOrEmpty())
            {
                var newWhere = DynamicExpressionParser.ParseLambda<BaseInfo_Size, bool>(
                    ParsingConfig.Default, false, $@"{search.Condition}.Contains(@0)", search.Keyword);
                where = where.And(newWhere);
            }

            return await q.Where(where).GetPageResultAsync(input);
        }

        public async Task<BaseInfo_Size> GetTheDataAsync(string id)
        {
            return await GetEntityAsync(id);
        }

        public async Task AddDataAsync(BaseInfo_Size data)
        {
            await InsertAsync(data);
        }

        public async Task UpdateDataAsync(BaseInfo_Size data)
        {
            await UpdateAsync(data);
        }

        public async Task DeleteDataAsync(List<string> ids)
        {
            await DeleteAsync(ids);
        }

        public async Task<List<BaseInfo_Size>> GetAllDataListAsync()
        {
            return await GetIQueryable().ToListAsync();
        }

        #endregion

        #region 私有成员

        #endregion
    }
}