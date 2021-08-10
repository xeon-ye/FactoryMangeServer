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
    public class BaseInfo_ProofTypeBusiness : BaseBusiness<BaseInfo_ProofType>, IBaseInfo_ProofTypeBusiness, ITransientDependency
    {
        public BaseInfo_ProofTypeBusiness(IDbAccessor db)
            : base(db)
        {
        }

        #region 外部接口

        public async Task<PageResult<BaseInfo_ProofType>> GetDataListAsync(PageInput<ConditionDTO> input)
        {
            var q = GetIQueryable();
            var where = LinqHelper.True<BaseInfo_ProofType>();
            var search = input.Search;

            //筛选
            if (!search.Condition.IsNullOrEmpty() && !search.Keyword.IsNullOrEmpty())
            {
                var newWhere = DynamicExpressionParser.ParseLambda<BaseInfo_ProofType, bool>(
                    ParsingConfig.Default, false, $@"{search.Condition}.Contains(@0)", search.Keyword);
                where = where.And(newWhere);
            }

            return await q.Where(where).GetPageResultAsync(input);
        }

        public async Task<BaseInfo_ProofType> GetTheDataAsync(string id)
        {
            return await GetEntityAsync(id);
        }

        public async Task AddDataAsync(BaseInfo_ProofType data)
        {
            await InsertAsync(data);
        }

        public async Task UpdateDataAsync(BaseInfo_ProofType data)
        {
            await UpdateAsync(data);
        }

        public async Task DeleteDataAsync(List<string> ids)
        {
            await DeleteAsync(ids);
        }

        public async Task<List<BaseInfo_ProofType>> GetAllDataListAsync()
        {
            return await GetIQueryable().ToListAsync();
        }

        #endregion

        #region 私有成员

        #endregion
    }
}