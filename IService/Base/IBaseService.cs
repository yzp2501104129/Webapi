using WebApi.Entity.Models;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.IService.Base
{
    public interface IBaseService<T> where T : class
    {
        Task<bool> InsertAsync(T insertObj);

        Task<bool> InsertRangeAsync(List<T> insertObjs);

        Task<bool> DeleteByIdAsync(dynamic id);

        Task<bool> DeleteByIdsAsync(dynamic[] ids);

        Task<bool> UpdateAsync(T updateObj);

        Task<T> GetByIdAsync(dynamic id);

        Task<List<T>> GetListAsync();

        Task<List<T>> GetListAsync(Expression<Func<T, bool>> whereExpression);

        PagingData<T> PageList(List<IConditionalModel> conditionalList, PageModel page, Expression<Func<T, object>> orderByExpression = null, OrderByType orderByType = OrderByType.Desc);
    }
}
