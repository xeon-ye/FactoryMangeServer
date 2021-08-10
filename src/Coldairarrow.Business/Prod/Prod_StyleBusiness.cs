using Coldairarrow.Business.Base_Manage;
using Coldairarrow.Entity.Prod;
using Coldairarrow.Util;
using EFCore.Sharding;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace Coldairarrow.Business.Prod
{
    public class Prod_StyleBusiness : BaseBusiness<Prod_Style>, IProd_StyleBusiness, ITransientDependency
    {
        IKMSBusiness _km;
        public Prod_StyleBusiness(IDbAccessor db, IKMSBusiness km)
            : base(db)
        {
            _km = km;
        }

        #region 外部接口

        public async Task<PageResult<Prod_Style>> GetDataListAsync(PageInput<ConditionDTO> input)
        {
            var q = GetIQueryable();
            var where = LinqHelper.True<Prod_Style>();
            var search = input.Search;

            //筛选
            if (!search.Condition.IsNullOrEmpty() && !search.Keyword.IsNullOrEmpty())
            {
                var newWhere = DynamicExpressionParser.ParseLambda<Prod_Style, bool>(
                    ParsingConfig.Default, false, $@"{search.Condition}.Contains(@0)", search.Keyword);
                where = where.And(newWhere);
            }

            return await q.Where(where).GetPageResultAsync(input);
        }

        public async Task<Prod_Style> GetTheDataAsync(string id)
        {
            return await GetEntityAsync(id);
        }

        public async Task AddDataAsync(Prod_Style data)
        {
            data.StyleId = _km.GetKey("Style");
            await InsertAsync(data);
        }

        public async Task UpdateDataAsync(Prod_Style data)
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