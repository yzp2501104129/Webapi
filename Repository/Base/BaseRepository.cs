using WebApi.Entity.Models;
using WebApi.Repository.Base.SqlSugar;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WebApi.IRepository.Base;

namespace WebApi.Repository.Base
{
    public class BaseRepository<T> : DefaultContext<T>, IBaseRepository<T> where T : class, new()
    {
        /// <summary>
        /// Orm数据分页
        /// </summary>
        /// <param name="conditionalList"></param>
        /// <param name="page"></param>
        /// <param name="orderByExpression"></param>
        /// <param name="orderByType"></param>
        /// <returns></returns>
        public virtual PagingData<T> BasePageList(List<IConditionalModel> conditionalList, PageModel page, Expression<Func<T, object>> orderByExpression = null, OrderByType orderByType = OrderByType.Desc)
        {
            int totalNumber = 0;
            List<T> result = Db.Queryable<T>().OrderByIF(orderByExpression != null, orderByExpression, orderByType).Where(conditionalList)
                .ToPageList(page.PageIndex, page.PageSize, ref totalNumber);
            page.TotalCount = totalNumber;
            return new PagingData<T>()
            {
                PageIndex = page.PageIndex,
                PageSize = page.PageSize,
                Data = result,
                Count = page.TotalCount
            };
        }

        /// <summary>
        /// 调用SQL语句进行分页
        /// </summary>
        /// <param name="SqlString"></param>
        /// <param name="conditionalList"></param>
        /// <param name="page"></param>
        /// <param name="orderByExpression"></param>
        /// <param name="orderByType"></param>
        /// <returns></returns>
        public virtual PagingData<T> BasePageList(string SqlString, List<IConditionalModel> conditionalList, PageModel page, Expression<Func<T, object>> orderByExpression = null, OrderByType orderByType = OrderByType.Desc)
        {
            int totalNumber = 0;
            List<T> result = Db.SqlQueryable<T>(SqlString).OrderByIF(orderByExpression != null, orderByExpression, orderByType).Where(conditionalList)
                .ToPageList(page.PageIndex, page.PageSize, ref totalNumber);
            page.TotalCount = totalNumber;
            return new PagingData<T>()
            {
                PageIndex = page.PageIndex,
                PageSize = page.PageSize,
                Data = result,
                Count = page.TotalCount
            };
        }


        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="insertObj">obj</param>
        /// <returns></returns>
        public virtual async Task<bool> BaseInsertAsync(T insertObj)
        {
            return await DbContext.InsertAsync(insertObj);
        }

        /// <summary>
        /// 批量新增
        /// </summary>
        /// <param name="insertObjs">obj List</param>
        /// <returns></returns>
        public virtual async Task<bool> BaseInsertRangeAsync(List<T> insertObjs)
        {
            return await DbContext.InsertRangeAsync(insertObjs);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id">id</param>
        /// <returns></returns>
        public virtual async Task<bool> BaseDeleteByIdAsync(dynamic id)
        {
            return await DbContext.DeleteByIdAsync(id);
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="ids">id list</param>
        /// <returns></returns>
        public virtual async Task<bool> BaseDeleteByIdsAsync(dynamic[] ids)
        {
            return await DbContext.DeleteByIdsAsync(ids);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="updateObj">obj</param>
        /// <returns></returns>
        public virtual async Task<bool> BaseUpdateAsync(T updateObj)
        {
            return await DbContext.UpdateAsync(updateObj);
        }

        /// <summary>
        /// 根据id查询
        /// </summary>
        /// <param name="id">id</param>
        /// <returns></returns>
        public virtual async Task<T> BaseGetByIdAsync(dynamic id)
        {
            return await DbContext.GetByIdAsync(id);
        }

        /// <summary>
        /// 查询List
        /// </summary>
        /// <returns></returns>
        public virtual async Task<List<T>> BaseGetListAsync()
        {
            return await DbContext.GetListAsync();
        }

        /// <summary>
        /// 条件查询·List
        /// </summary>
        /// <param name="whereExpression"></param>
        /// <returns></returns>
        public virtual async Task<List<T>> BaseGetListAsync(Expression<Func<T, bool>> whereExpression)
        {
            return await DbContext.GetListAsync(whereExpression);
        }
    }
}
