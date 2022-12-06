using WebApi.Entity.Models;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.IRepository.Base
{
    public interface IBaseRepository<T> where T : class, new()
    {
        Task<bool> BaseInsertAsync(T insertObj);

        Task<bool> BaseInsertRangeAsync(List<T> insertObjs);

        Task<bool> BaseDeleteByIdAsync(dynamic id);

        Task<bool> BaseDeleteByIdsAsync(dynamic[] ids);

        Task<bool> BaseUpdateAsync(T updateObj);

        Task<T> BaseGetByIdAsync(dynamic id);

        Task<List<T>> BaseGetListAsync();

        Task<List<T>> BaseGetListAsync(Expression<Func<T, bool>> whereExpression);

        PagingData<T> BasePageList(List<IConditionalModel> conditionalList, PageModel page, Expression<Func<T, object>> orderByExpression = null, OrderByType orderByType = OrderByType.Desc);
    }
}