using WebApi.Entity.Models;
using WebApi.IRepository.Base;
using WebApi.IService.Base;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Service.Base
{
    public class BaseService<T> : IBaseService<T> where T : class, new()
    {
        private readonly IBaseRepository<T> baseRepository;
        public BaseService(IBaseRepository<T> _baseRepository) { this.baseRepository = _baseRepository; }

        public async Task<bool> DeleteByIdAsync(dynamic id)
        {
            return await baseRepository.BaseDeleteByIdAsync(id);
        }

        public async Task<bool> DeleteByIdsAsync(dynamic[] ids)
        {
            return await baseRepository.BaseDeleteByIdsAsync(ids);
        }

        public async Task<T> GetByIdAsync(dynamic id)
        {
            return await baseRepository.BaseGetByIdAsync(id);
        }

        public async Task<List<T>> GetListAsync()
        {
            return await baseRepository.BaseGetListAsync();
        }

        public async Task<List<T>> GetListAsync(Expression<Func<T, bool>> whereExpression)
        {
            return await baseRepository.BaseGetListAsync(whereExpression);
        }

        public async Task<bool> InsertAsync(T insertObj)
        {
            return await baseRepository.BaseInsertAsync(insertObj);
        }

        public async Task<bool> InsertRangeAsync(List<T> insertObjs)
        {
            return await baseRepository.BaseInsertRangeAsync(insertObjs);
        }

        public PagingData<T> PageList(List<IConditionalModel> conditionalList, PageModel page, Expression<Func<T, object>> orderByExpression = null, OrderByType orderByType = OrderByType.Desc)
        {
            return baseRepository.BasePageList(conditionalList, page);
        }

        public async Task<bool> UpdateAsync(T updateObj)
        {
            return await baseRepository.BaseUpdateAsync(updateObj);
        }
    }
}
