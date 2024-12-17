using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mir.Core.DBHelper
{
    public class SqliteHelper
    {
        //连接字符串
        public static string ConnectionString = "Data source=" + AppDomain.CurrentDomain.BaseDirectory + "\\DATA.db";

        /// <summary>
        /// 增删改
        /// 20180723
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="param">sql参数</param>
        /// <returns>受影响的行数</returns>
        public static int ExecuteNonQuery(string sql, params SQLiteParameter[] param)
        {
            try
            {
                using (SQLiteConnection con = new SQLiteConnection(ConnectionString))
                {
                    using (SQLiteCommand cmd = new SQLiteCommand(sql, con))
                    {
                        con.Open();
                        if (param != null)
                        {
                            cmd.Parameters.AddRange(param);
                        }

                        string sql2 = cmd.CommandText;
                        //con.Close();
                        return cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (SQLiteException se)
            {
                return 0;
            }
        }


        /// <summary>
        /// 增删改
        /// 20180723
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="param">sql参数</param>
        /// <returns>受影响的行数</returns>
        public static int ExecuteNonQuery(string sql)
        {
            try
            {
                using (SQLiteConnection con = new SQLiteConnection(ConnectionString))
                {
                    using (SQLiteCommand cmd = new SQLiteCommand(sql, con))
                    {
                        con.Open();
                        string sql2 = cmd.CommandText;
                        con.Close();
                        return cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (SQLiteException se)
            {
                return 0;
            }
        }

        /// <summary>
        /// 查询
        /// 20180723
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="param">sql参数</param>
        /// <returns>首行首列</returns>
        public static object ExecuteScalar(string sql)
        {
            try
            {
                using (SQLiteConnection con = new SQLiteConnection(ConnectionString))
                {
                    using (SQLiteCommand cmd = new SQLiteCommand(con))
                    {
                        con.Open();
                        cmd.CommandText = sql;
                        return cmd.ExecuteScalar();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }


        /// <summary>
        /// 查询
        /// 20180723
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="param">sql参数</param>
        /// <returns>首行首列</returns>
        public static object ExecuteScalar(string sql, params SQLiteParameter[] param)
        {
            using (SQLiteConnection con = new SQLiteConnection(ConnectionString))
            {
                using (SQLiteCommand cmd = new SQLiteCommand(sql, con))
                {
                    con.Open();
                    if (param != null)
                    {
                        cmd.Parameters.AddRange(param);
                    }

                    return cmd.ExecuteScalar();
                }
            }
        }

        /// <summary>
        /// 多行查询
        /// 20180723
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="param">sql参数</param>
        /// <returns>SQLiteDateReader</returns>
        public static SQLiteDataReader ExecuteReader(string sql, params SQLiteParameter[] param)
        {
            using (SQLiteConnection con = new SQLiteConnection(ConnectionString))
            {
                using (SQLiteCommand cmd = new SQLiteCommand(sql, con))
                {
                    if (param != null)
                    {
                        cmd.Parameters.AddRange(param);
                    }
                    try
                    {
                        con.Open();
                        return cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
                    }
                    catch (Exception ex)
                    {
                        con.Close();
                        con.Dispose();
                        throw ex;
                    }
                }
            }

        }

        /// <summary>
        /// 查询多行数据
        /// 20180723
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="param">sql参数</param>
        /// <returns>一个表</returns>
        public static DataTable ExecuteTable(string sql, params SQLiteParameter[] param)
        {
            DataTable dt = new DataTable();
            using (SQLiteDataAdapter sda = new SQLiteDataAdapter(sql, ConnectionString))
            {
                if (param != null)
                {
                    sda.SelectCommand.Parameters.AddRange(param);
                }
                sda.Fill(dt);
            }
            return dt;
        }


        /// <summary>
        /// 数据插入
        /// 20180725
        /// </summary>
        /// <param name="tbName">表名</param>
        /// <param name="insertData">需要插入的数据字典</param>
        /// <returns>受影响行数</returns>
        public static int ExecuteInsert(string tbName, Dictionary<String, String> insertData)
        {
            string point = "";//分隔符号(,)
            string keyStr = "";//字段名拼接字符串
            string valueStr = "";//值的拼接字符串

            List<SQLiteParameter> param = new List<SQLiteParameter>();
            foreach (string key in insertData.Keys)
            {
                keyStr += string.Format("{0} `{1}`", point, key);
                valueStr += string.Format("{0} @{1}", point, key);
                param.Add(new SQLiteParameter("@" + key, insertData[key]));
                point = ",";
            }
            string sql = string.Format("INSERT INTO `{0}`({1}) VALUES({2})", tbName, keyStr, valueStr);

            //return sql;
            return ExecuteNonQuery(sql, param.ToArray());

        }

        /// <summary>
        /// 执行Update语句
        /// 20180725
        /// </summary>
        /// <param name="tbName">表名</param>
        /// <param name="where">更新条件：id=1</param>
        /// <param name="insertData">需要更新的数据</param>
        /// <returns>受影响行数</returns>
        public static int ExecuteUpdate(string tbName, string where, Dictionary<String, String> insertData)
        {
            string point = "";//分隔符号(,)
            string kvStr = "";//键值对拼接字符串(Id=@Id)

            List<SQLiteParameter> param = new List<SQLiteParameter>();
            foreach (string key in insertData.Keys)
            {
                kvStr += string.Format("{0} {1}=@{2}", point, key, key);
                param.Add(new SQLiteParameter("@" + key, insertData[key]));
                point = ",";
            }
            string sql = string.Format("UPDATE `{0}` SET {1} WHERE {2}", tbName, kvStr, where);

            return ExecuteNonQuery(sql, param.ToArray());

        }






        /// <summary>
        /// 查询
        /// 20180723
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="param">sql参数</param>
        /// <returns>首行首列</returns>
        public static DataTable GetDataTable(string sql)
        {
            try
            {
                using (SQLiteConnection con = new SQLiteConnection(ConnectionString))
                {
                    using (SQLiteCommand cmd = new SQLiteCommand(sql, con))
                    {
                        con.Open();
                        SQLiteDataAdapter ad = new SQLiteDataAdapter(cmd);
                        DataTable tb = new DataTable();
                        ad.Fill(tb);
                        con.Close();
                        return tb;

                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }






    }
}