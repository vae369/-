using Mir.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mir.IService
{
    public interface IStdItemsService : IBaseServer<StdItems>
    {

        /// <summary>
        /// 获取物品数据
        /// </summary>
        /// <returns></returns>
        DataTable GetStdItemsByTable(string where);

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
