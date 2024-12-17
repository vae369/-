using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Mir.IService
{
    public interface IBaseServer<T>
    {
        int Count(Expression<Func<T, bool>> whereExpression);
        bool Delete(Expression<Func<T, bool>> whereExpression);
        bool Delete(T deleteObj);
        List<T> GetList();
        List<T> GetList(Expression<Func<T, bool>> whereExpression);
        List<T> GetPageList(List<IConditionalModel> conditionalList, PageModel page);
        List<T> GetPageList(List<IConditionalModel> conditionalList, PageModel page, Expression<Func<T, object>> orderByExpression = null, OrderByType orderByType = OrderByType.Asc);
        List<T> GetPageList(Expression<Func<T, bool>> whereExpression, PageModel page, Expression<Func<T, object>> orderByExpression = null, OrderByType orderByType = OrderByType.Asc);
        List<T> GetPageList(Expression<Func<T, bool>> whereExpression, PageModel page);
        T GetSingle(Expression<Func<T, bool>> whereExpression);
        bool Insert(T insertObj);
        bool InsertRange(T[] insertObjs);
        bool InsertRange(List<T> insertObjs);
        int InsertReturnIdentity(T insertObj);

        bool IsAny(Expression<Func<T, bool>> whereExpression);
        bool Update(T updateObj);
        bool Update(Expression<Func<T, T>> columns, Expression<Func<T, bool>> whereExpression);
        bool UpdateRange(T[] updateObjs);
        bool UpdateRange(List<T> updateObjs);
    }
}
