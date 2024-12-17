using Mir.Core.cache;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mir.ORM.SqlSugar
{

    public class DbContext
    {
        public DbContext(DbType dbt)
        {
            Db = new SqlSugarClient(new ConnectionConfig()
            {
                ConnectionString = PubConstant.ConnectionString,
                DbType = dbt,
                IsAutoCloseConnection = true,
                InitKeyType = InitKeyType.Attribute,
                ConfigureExternalServices = new ConfigureExternalServices()
                {
                    DataInfoCacheService = new HttpRuntimeCache() //配置的缓存类
                }
                //,MoreSettings =new ConnMoreSettings { IsWithNoLockQuery=true}
            });

            Db.Aop.OnLogExecuting = (sql, pars) =>
            {

            };
        }

        public SqlSugarClient Db;

    }


    public class PubConstant
    {
        public static string ConnectionString { get; set; }

        /// <summary>
        /// 获取连接字符串
        /// </summary>
        //public static string ConnectionString
        //{
        //    get
        //    {
        //        string _connectionString = ConfigurationManager.AppSettings["ConnectionString"];

        //        return _connectionString;
        //    }
        //}
    }
}
