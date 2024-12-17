
using SqlSugar;
using System.Threading.Tasks; 
namespace Mir.ORM.SqlSugar.Repostitory
{
    public class Repostitory<T> : SqlSugarRepostitory<T>, IRepostitory<T> where T : class, new()
    {
        public Repostitory(DbType dbt) : base(dbt)
        {
        }

        public async Task<bool> DeleteTask(T deleteObj)
        {
            return await Context.Deleteable<T>().ExecuteCommandAsync() > 0 ? true : false; 
        }

        public async Task<bool> InsertTask(T insertObj)
        {
            
           return await Context.Insertable<T>(insertObj).ExecuteCommandAsync() > 0 ? true : false;

        }

        public async Task<bool> UpdateTask(T insertObj)
        {
            return  await Context.Updateable<T>().ExecuteCommandAsync() > 0 ? true : false;
        }

       
    }
}