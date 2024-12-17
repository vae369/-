using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Mir.ORM.SqlSugar.Repostitory
{
    public interface IRepostitory<T> where T : class
    {
        int Count(Expression<Func<T, bool>> whereExpression);
        bool Delete(Expression<Func<T, bool>> whereExpression);
        bool Delete(T deleteObj);
        Task<bool> DeleteTask(T deleteObj);
        //bool DeleteById([Dynamic] dynamic id);
        //bool DeleteByIds([Dynamic(new[] { false, true })] dynamic[] ids);
        //T GetById([Dynamic] dynamic id);
        List<T> GetList();
        List<T> GetList(Expression<Func<T, bool>> whereExpression);
        List<T> GetPageList(List<IConditionalModel> conditionalList, PageModel page);
        List<T> GetPageList(List<IConditionalModel> conditionalList, PageModel page, Expression<Func<T, object>> orderByExpression = null, OrderByType orderByType = OrderByType.Asc);
        List<T> GetPageList(Expression<Func<T, bool>> whereExpression, PageModel page, Expression<Func<T, object>> orderByExpression = null, OrderByType orderByType = OrderByType.Asc);
        List<T> GetPageList(Expression<Func<T, bool>> whereExpression, PageModel page);
        T GetSingle(Expression<Func<T, bool>> whereExpression);
        bool Insert(T insertObj);
        Task<bool> InsertTask(T insertObj);
        bool InsertRange(T[] insertObjs);
        bool InsertRange(List<T> insertObjs);
        int InsertReturnIdentity(T insertObj);
        bool IsAny(Expression<Func<T, bool>> whereExpression);
        bool Update(T updateObj);
        Task<bool> UpdateTask(T insertObj);
        bool Update(Expression<Func<T, T>> columns, Expression<Func<T, bool>> whereExpression);
        bool UpdateRange(T[] updateObjs);
        bool UpdateRange(List<T> updateObjs);



    }
}
