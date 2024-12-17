using Mir.IService;
using Mir.Models;
using Mir.ORM.SqlSugar.Repostitory;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Mir.Server
{
    public class MonsterServer : Repostitory<Monster>, IMonsterService
    {
        public MonsterServer(SqlSugar.DbType dbt) : base(dbt)
        {


        }
        public bool SaveByStorageable(DataTable dt)
        {
            string sql = "";
            string insertColumn = "";
            string insertRowData = "";
            foreach (DataRow dr in dt.Rows)
            {
                switch (dr.RowState)
                {
                    case DataRowState.Modified:
                        {
                            foreach (DataColumn dc in dt.Columns)
                            {
                                //自定义的最大生命值字段，所以不需要添加数据到数据库中
                                if (dc.ColumnName.ToLower() == "maxhp") continue;
                                if (dc.ColumnName.ToLower() == "m_id") continue;

                                // 使用TypeCode进行判断
                                TypeCode typeCode = Type.GetTypeCode(dc.DataType);
                                switch (typeCode)
                                {
                                    case TypeCode.Int32:
                                        if (string.IsNullOrEmpty(dr[dc.ColumnName].ToString()))
                                        {
                                            insertRowData += $"{dc.ColumnName}=0,";
                                        }
                                        else
                                        {
                                            insertRowData += $"{dc.ColumnName}={dr[dc.ColumnName]},";
                                        }
                                        break;
                                    case TypeCode.String: insertColumn = $"{dc.ColumnName}='{dr[dc.ColumnName]}',"; break;
                                    default:
                                        if (string.IsNullOrEmpty(dr[dc.ColumnName].ToString()))
                                        {
                                            insertRowData += $"{dc.ColumnName}=0,";
                                        }
                                        else
                                        {
                                            insertRowData += $"{dc.ColumnName}={dr[dc.ColumnName]},";
                                        }
                                        break;
                                }
                            }
                            insertColumn = insertColumn.TrimEnd(',');
                            insertRowData = insertRowData.TrimEnd(',');
                            sql = $"update Monster set {insertRowData} where {insertColumn}";
                            Context.Ado.ExecuteCommand(sql);
                        }
                        break;
                    case DataRowState.Deleted: break;
                    case DataRowState.Added:
                        {
                            foreach (DataColumn dc in dt.Columns)
                            {
                                //自定义的最大生命值字段，所以不需要添加数据到数据库中
                                if (dc.ColumnName.ToLower() == "maxhp") continue;
                                if (dc.ColumnName.ToLower() == "m_id") continue;
                                insertColumn += dc.ColumnName + ",";
                                TypeCode typeCode = Type.GetTypeCode(dc.DataType);
                                switch (typeCode)
                                {
                                    case TypeCode.Int32:
                                        if (string.IsNullOrEmpty(dr[dc.ColumnName].ToString()))
                                        {
                                            insertRowData += "0,";
                                        }
                                        else
                                        {
                                            insertRowData += dr[dc.ColumnName] + ",";
                                        }
                                        break;
                                    case TypeCode.String: insertRowData += $"'{dr[dc.ColumnName]}',"; break;
                                    default:
                                        if (string.IsNullOrEmpty(dr[dc.ColumnName].ToString()))
                                        {
                                            insertRowData += "0,";
                                        }
                                        else
                                        {
                                            insertRowData += dr[dc.ColumnName] + ",";
                                        }
                                        break;
                                }
                            }

                            insertColumn = insertColumn.TrimEnd(',');
                            insertRowData = insertRowData.TrimEnd(',');
                            sql = $"insert into Monster ({insertColumn}) values ({insertRowData})";
                            Context.Ado.ExecuteCommand(sql);
                        }
                        break;
                }

            }

            return true;
        }

        public bool ConnectionDB()
        {
            bool isExists = false;
            try
            {
                if (GetList().Count > 0) isExists = true;
            }
            catch (Exception)
            {
                var mList = Context.Queryable<object>().AS("Monster", "m").ToList();
                if (mList.Count > 0) isExists = true;
            }
            return isExists;
        }

        public System.Data.DataTable GetMonsterByTable()
        {
            DataTable dt = new DataTable();

            try
            {
                var list = Context.DbMaintenance.GetTableInfoList();
                var table = list.Where(s => s.Name.ToLower() == "monster").FirstOrDefault();
                if (table == null) return dt;

                string sql = "select * from Monster";
                dt = Context.Ado.GetDataTable(sql);
                dt.Columns.Add("MAXHP", typeof(long)).SetOrdinal(9);

                foreach (DataRow dr in dt.Rows)
                {
                    int.TryParse(dr["hp"].ToString(), out int hp);

                    dr["MAXHP"] = hp;
                    long maxhp = 0;
                    if (hp < 0)
                    {
                        hp = Math.Abs(int.MinValue - hp);
                        maxhp = (long)hp + int.MaxValue + 1;
                        dr["MAXHP"] = maxhp;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dt;
        }

        public bool SaveSingle(string tableName, Dictionary<string, string> SqlColmun)
        {
            bool flag = false;
            if (SqlColmun.Count >= 4)
            {
                string sql = $"update {tableName} set {SqlColmun["SaveKey"]}={SqlColmun["SaveValue"]} where {SqlColmun["PrimaryKey"]}={SqlColmun["PrimaryValue"]}";
                if (Context.Ado.ExecuteCommand(sql) > 0) flag = true;
            }
            return flag;
        }

        public void Delete(List<string> ids)
        {
            foreach (string id in ids)
            {
                string sql = "delete from Monster where name='" + id + "'";
                int num = Context.Ado.ExecuteCommand(sql);
            }
        }

        public DataTable QuerySQL(string sql)
        {
            try
            {
                return Context.Ado.GetDataTable(sql);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public bool ExecuteSQL(List<string> listSql)
        {
            var result = Context.Ado.UseTran(() =>
            {
                foreach (string sql in listSql)
                {
                    Context.Ado.ExecuteCommand(sql);
                }
                return true;
            });
            if (!result.Data)
            {
                throw new Exception(result.ErrorMessage);
            }
            return true;
        }
        public int ExecuteSQL(string sql)
        {
            try
            {
                return Context.Ado.ExecuteCommand(sql);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public List<tb_MirTable> GetMirTable()
        {
            List<tb_MirTable> list = new List<tb_MirTable>();
            tb_MirTable mirTable = null;
            tb_MirColumn column = null;
            string sql = "";
            string colString = "";
            string valString = "";
            var tables = Context.DbMaintenance.GetTableInfoList(false);//true 走缓存 false不走缓存
            string kv = "";
            foreach (var table in tables)
            {
                mirTable = new tb_MirTable
                {
                    TableName = table.Name
                };
                mirTable.Columns = new List<tb_MirColumn>();
                mirTable.MirData = new List<string>();

                //获取列信息
                var columns = Context.DbMaintenance.GetColumnInfosByTableName(table.Name);
                columns.ForEach(col =>
                {
                    kv = mirTable.TableName + "_" + col.DbColumnName;

                    column = new tb_MirColumn
                    {
                        IsPrimarykey = col.IsPrimarykey,
                        ColumnName = col.DbColumnName,
                        DataType = col.DataType,
                        ColumnLength = col.Length,
                        ColumnDescription = col.ColumnDescription,
                        SortCode = 0,
                    };
                    mirTable.Columns.Add(column);
                });

                int sortCode = 0;
                mirTable.Columns.ForEach(s =>
                {
                    // 如果表中的字段在表中就取到排序id进行排序，否则取最大值+1
                    kv = mirTable.TableName + "_" + s.ColumnName;
                    if (mirTable.TableKv.ContainsKey(kv.ToLower()))
                        sortCode = mirTable.TableKv[kv.ToLower()];
                    else
                    {
                        if (sortCode == 0) sortCode = 99;
                    }

                    s.SortCode = sortCode;
                    sortCode = 0;
                });
                mirTable.Columns = mirTable.Columns.OrderBy(s => s.SortCode).ToList();

                list.Add(mirTable);
                colString = string.Join(",", mirTable.Columns.Select(s => s.ColumnName));
                sql = $"select {colString} from {mirTable.TableName}";

                DataTable dt = Context.Ado.GetDataTable(sql);
                mirTable.MirDataTable = dt;
                foreach (DataRow dr in dt.Rows)
                {
                    valString = "";
                    foreach (var item in mirTable.Columns)
                    {
                        switch (item.DataType.ToLower())
                        {
                            case "string":
                            case "text": valString += "'" + dr[item.ColumnName].ToString() + "'"; break;
                            default: valString += dr[item.ColumnName].ToString(); break;
                        }
                        valString += ",";
                    }
                    sql = $"insert into {mirTable.TableName}({colString}) values({valString.TrimEnd(',')})";
                    mirTable.MirData.Add(sql);
                }
            }

            return list;
        }

        public bool SqlExcuteColumn(string tableName, tb_MirColumn col, string editType)
        {
            try
            {
                DbColumnInfo dbCol = new DbColumnInfo()
                {
                    TableName = tableName,
                    DbColumnName = col.ColumnName,
                    DataType = col.DataType,
                    Length = col.ColumnLength,
                    ColumnDescription = col.ColumnDescription,
                };
                bool flag = false;
                switch (editType)
                {
                    case "insert": flag = Context.DbMaintenance.AddColumn(tableName, dbCol); break;
                    case "delete": flag = Context.DbMaintenance.DropColumn(tableName, dbCol.DbColumnName); break;
                    case "update": flag = Context.DbMaintenance.UpdateColumn(tableName, dbCol); break;
                    case "rename":
                        var list = Context.DbMaintenance.GetColumnInfosByTableName(tableName);
                        flag = Context.DbMaintenance.DropColumn(tableName, dbCol.ColumnDescription);
                        dbCol.ColumnDescription = list.Where(s => s.DbColumnName == dbCol.ColumnDescription).First().ColumnDescription;
                        flag = Context.DbMaintenance.AddColumn(tableName, dbCol);
                        break;
                }

                return flag;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DropTable(string tableName)
        {
            return Context.DbMaintenance.DropTable(tableName);
        }
    }
}
