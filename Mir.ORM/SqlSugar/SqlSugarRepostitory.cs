
using SqlSugar;

namespace Mir.ORM.SqlSugar
{

    public class SqlSugarRepostitory<T> : SimpleClient<T> where T : class, new()
    {
        public SqlSugarRepostitory(DbType dbt) : base(new DbContext(dbt).Db)
        {

        }  
    } 
}

