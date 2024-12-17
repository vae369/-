using Mir.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mir.IService
{
    public interface IMonsterService : IBaseServer<Monster>
    {
        /// <summary>
        /// 删除表
        /// </summary>
        bool DropTable(string tableName);

        /// <summary>
        /// 保存，更新，删除字段
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        bool SqlExcuteColumn(string tableName, tb_MirColumn col, string editType);


        /// <summary>
        /// 动态获取数据库表，字段，类型
        /// </summary>
        /// <returns></returns>
        List<tb_MirTable> GetMirTable();

        /// <summary>
        /// 连接数据库
        /// </summary>
        /// <returns></returns>
        bool ConnectionDB();

        /// <summary>
        /// 查询数据库
        /// </summary>
        DataTable QuerySQL(string sql);

        /// <summary>
        /// 增删改数据库(事务)
        /// </summary>
        bool ExecuteSQL(List<string> listSql);


        /// <summary>
        /// 增删改数据库
        /// </summary>
        int ExecuteSQL(string sql);

        /// <summary>
        /// 获取怪物数据
        /// </summary>
        /// <returns></returns>
        DataTable GetMonsterByTable();

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <returns></returns>
        bool SaveByStorageable(DataTable dt);

        /// <summary>
        /// 保存一条数据
        /// </summary>
        bool SaveSingle(string tableName, Dictionary<string, string> SqlColmun);

        /// <summary>
        /// 删除多条数据
        /// </summary>
        void Delete(List<string> ids);
    }
}
