using Mir.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mir.Core.DBHelper
{
    public interface IDBHepler
    {
        /// <summary>
        /// 数据库是否连接成功
        /// </summary>
        /// <param name="dBInfo">数据库对象</param>
        /// <returns></returns>
        bool IsConnected(DBInfo dBInfo);
    }
}
