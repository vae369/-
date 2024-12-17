using ADOX;
using Mir.Core.DBHelper;
using Mir.Core.utils;
using Mir.IService;
using Mir.Models;
using Mir.Models.DTO;
using Mir.ORM.SqlSugar;
using Mir.Server;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Relational;
using Spire.Xls;
using Spire.Xls.Core;
using Sunny.UI;
using Sunny.UI.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Mir2.Helper
{
    public class DBHelper
    {
        
        public static void CreateTableByExcel(string path, List<tb_MirTable> list)
        {
            Workbook workbook = new Workbook();
            workbook.LoadFromFile(path);

            for (int i = 0; i < list.Count; i++)
            {
                var mir = list[i];
                Worksheet sheet = workbook.Worksheets[i];
                sheet.Name = mir.TableName;

                for (int j = 1; j < mir.Columns.Count; j++)
                {
                    // 插入列头 字段
                    sheet.Range[1, j].Text = mir.Columns[j].ColumnName;
                }
                sheet.InsertDataTable(mir.MirDataTable, true, 1, 1);
            }
            workbook.SaveToFile(path, ExcelVersion.Version2010);
        }

        public static List<tb_MirTable> GetDataByExcel(string path)
        {
            try
            {
                List<tb_MirTable> list = new List<tb_MirTable>();
                tb_MirTable mirTable = null;
                tb_MirColumn mirColumn = null;

                Workbook wb = new Workbook();
                wb.LoadFromFile(path);
                foreach (var item in wb.Worksheets)
                {
                    mirTable = new tb_MirTable
                    {
                        TableName = item.Name,
                    };
                }
                ////获取第一张工作表
                //Worksheet sheet = wb.Worksheets[0];

                ////获取包含数据的单元格区域
                //CellRange locatedRange = sheet.AllocatedRange;

                ////遍历其中的每一行
                //for (int i = 0; i < locatedRange.Rows.Length; i++)
                //{
                //    //遍历其中的每一列
                //    for (int j = 0; j < locatedRange.Rows[i].ColumnCount; j++)
                //    {
                //        //获取单元格数据
                //        Console.Write(locatedRange[i + 1, j + 1].Value + "  ");
                //    }
                //    Console.WriteLine();
                //}

                return list;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static void DataConvertExcel(DBInfo db)
        {
            IMonsterService MonsterService = null;
            try
            {
                string ConnectionString = "";
                List<tb_MirTable> list = null;
                #region 获取当前表，字段，类型
                switch (db.DataBaseType)
                {
                    case "Access":
                        ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + db.DataFilePath + ";Persist Security Info=False;";
                        PubConstant.ConnectionString = ConnectionString;
                        MonsterService = new MonsterServer(SqlSugar.DbType.Access);
                        break;
                    case "SQLite":
                        ConnectionString = "Data Source=" + db.DataFilePath;
                        PubConstant.ConnectionString = ConnectionString;
                        MonsterService = new MonsterServer(SqlSugar.DbType.Sqlite);
                        break;
                    case "MySQL":
                        ConnectionString = $"server={db.DataBaseAddr};Database={db.DataBaseName};Uid={db.DataBaseUserName};Pwd={db.DataBasePassWord}";
                        PubConstant.ConnectionString = ConnectionString;
                        MonsterService = new MonsterServer(SqlSugar.DbType.MySql);
                        break;
                    case "MSSQL":
                        if (db == null) throw new Exception("数据库对象为空，操作失败！");
                        ConnectionString = $"server={db.DataBaseAddr};Database={db.DataBaseName};Uid={db.DataBaseUserName};Pwd={db.DataBasePassWord}";
                        PubConstant.ConnectionString = ConnectionString;
                        MonsterService = new MonsterServer(SqlSugar.DbType.SqlServer);
                        break;
                    case "Excel":
                        list = GetDataByExcel(db.DataFilePath);
                        break;
                }
                #endregion

                if (db.DataBaseType != "Excel") list = MonsterService.GetMirTable();

                string dirPath = Path.GetDirectoryName(db.DataFilePath);
                string fileName = Path.GetFileNameWithoutExtension(db.DataFilePath);
                string path = dirPath + "\\" + fileName + ".xlsx";
                //创建Access数据库，表，字段
                CreateDBByExcel(path);
                CreateTableByExcel(path, list);

                db.DataFilePath = path;
                db.DataBaseType = "Excel(996引擎)";
                return;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static void DataConvertSQLite(DBInfo db)
        {
            IMonsterService MonsterService = null;
            try
            {
                string ConnectionString = "";

                switch (db.DataBaseType)
                {
                    case "Access":
                        ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + db.DataFilePath + ";Persist Security Info=False;";
                        PubConstant.ConnectionString = ConnectionString;
                        MonsterService = new MonsterServer(SqlSugar.DbType.Access);
                        break;
                    case "SQLite":
                        ConnectionString = "Data Source=" + db.DataFilePath;
                        PubConstant.ConnectionString = ConnectionString;
                        MonsterService = new MonsterServer(SqlSugar.DbType.Sqlite);
                        break;
                    case "MySQL":
                        ConnectionString = $"server={db.DataBaseAddr};Database={db.DataBaseName};Uid={db.DataBaseUserName};Pwd={db.DataBasePassWord}";
                        PubConstant.ConnectionString = ConnectionString;
                        MonsterService = new MonsterServer(SqlSugar.DbType.MySql);
                        break;
                    case "MSSQL":
                        if (db == null) throw new Exception("数据库对象为空，操作失败！");
                        ConnectionString = $"server={db.DataBaseAddr};Database={db.DataBaseName};Uid={db.DataBaseUserName};Pwd={db.DataBasePassWord}";
                        PubConstant.ConnectionString = ConnectionString;
                        MonsterService = new MonsterServer(SqlSugar.DbType.SqlServer);
                        break;
                }
                //获取表，字段，类型
                List<tb_MirTable> list = MonsterService.GetMirTable();
                string dirPath = Path.GetDirectoryName(db.DataFilePath);
                string fileName = Path.GetFileNameWithoutExtension(db.DataFilePath);
                string path = dirPath + "\\" + fileName + ".db";
                //创建Access数据库，表，字段
                CreateDBBySQLite(path);

                StringBuilder sb = new StringBuilder();
                foreach (var item in list)
                {
                    sb.Clear();
                    foreach (var col in item.Columns)
                    {
                        if (col.IsPrimarykey)
                        {
                            sb.Append($"{col.ColumnName} {col.DataType} PRIMARY KEY,");
                        }
                        else
                        {
                            sb.Append($"{col.ColumnName} {col.DataType},");
                        }
                    }
                    CreateTableBySQLite(path, item.TableName, sb.ToString().TrimEnd(','), item.MirData);
                }

                db.DataFilePath = path;
                db.DataBaseType = "SQLite";
                return;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static void DataConvertAccess(DBInfo db)
        {
            IMonsterService MonsterService = null;
            try
            {
                string ConnectionString = "";

                switch (db.DataBaseType)
                {
                    case "Access":
                        ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + db.DataFilePath + ";Persist Security Info=False;";
                        PubConstant.ConnectionString = ConnectionString;
                        MonsterService = new MonsterServer(SqlSugar.DbType.Access);
                        break;
                    case "SQLite":
                        ConnectionString = "Data Source=" + db.DataFilePath;
                        PubConstant.ConnectionString = ConnectionString;
                        MonsterService = new MonsterServer(SqlSugar.DbType.Sqlite);
                        break;
                    case "MySQL":
                        ConnectionString = $"server={db.DataBaseAddr};Database={db.DataBaseName};Uid={db.DataBaseUserName};Pwd={db.DataBasePassWord}";
                        PubConstant.ConnectionString = ConnectionString;
                        MonsterService = new MonsterServer(SqlSugar.DbType.MySql);
                        break;
                    case "MSSQL":
                        if (db == null) throw new Exception("数据库对象为空，操作失败！");
                        ConnectionString = $"server={db.DataBaseAddr};Database={db.DataBaseName};Uid={db.DataBaseUserName};Pwd={db.DataBasePassWord}";
                        PubConstant.ConnectionString = ConnectionString;
                        MonsterService = new MonsterServer(SqlSugar.DbType.SqlServer);
                        break;
                }
                //获取表，字段，类型
                List<tb_MirTable> list = MonsterService.GetMirTable();
                string dirPath = Path.GetDirectoryName(db.DataFilePath);
                string fileName = Path.GetFileNameWithoutExtension(db.DataFilePath);
                string path = dirPath + "\\" + fileName + ".mdb";
                //创建Access数据库，表，字段
                CreateDBByAccess(path);
                StringBuilder sb = new StringBuilder();
                foreach (var item in list)
                {
                    sb.Clear();
                    foreach (var col in item.Columns)
                    {
                        if (col.IsPrimarykey)
                        {
                            sb.Append($"{col.ColumnName} {col.DataType} PRIMARY KEY,");
                        }
                        else
                        {
                            sb.Append($"{col.ColumnName} {col.DataType},");
                        }
                    }
                    CreateTableByAccess(path, item.TableName, sb.ToString().TrimEnd(','), item.MirData);
                }

                db.DataFilePath = path;
                db.DataBaseType = "Access";
                return;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string DBBakOf(DBInfo db)
        {
            // 确保目标路径存在
            var directoryPath = Path.GetDirectoryName(db.DataFilePath);
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
            string fix = Path.GetExtension(db.DataFilePath);
            string path = $"{directoryPath}\\HeroDB_{DateTime.Now.ToString("yyyyMMddHHmmss")}_bak{fix}";
            switch (db.DataBaseType)
            {
                case "MySQL":
                    //DoBackupByMySql(db);
                    throw new Exception("MYSQL没有备份功能...");
                case "MSSQL":
                    throw new Exception("MSSQL没有备份功能...");
                default:
                    // 执行备份操作
                    File.Copy(db.DataFilePath, path, true); // true 用于覆盖可能存在的备份文件
                    break;
            }


            return path;
        }

        public static DataTable GetExcelDataBySQL(DBInfo db, string sql)
        {
            DataTable dt = new DataTable();
            try
            {
                string nSql = sql.ToLower();
                string where = "";
                Workbook workbook = new Workbook();

                workbook.LoadFromFile(db.DataFilePath);

                var magic = workbook.Worksheets["magic"].ExportDataTable();
                var monster = workbook.Worksheets["monster"].ExportDataTable();
                var stditems = workbook.Worksheets["stditems"].ExportDataTable();

                if (nSql.StartsWith("insert"))
                {

                }
                else if (nSql.StartsWith("delete"))
                {

                }
                else if (nSql.StartsWith("update"))
                {

                }
                else if (nSql.StartsWith("select"))
                {
                    if (nSql.Contains("magic"))
                    {
                        if (nSql.Contains("where"))
                        {
                            if (nSql.Contains("order"))
                            {
                                where = nSql.Substring(nSql.IndexOf("where") + 5, nSql.IndexOf("order") - nSql.IndexOf("where") - 5);
                                magic.DefaultView.RowFilter = where.Trim();

                                // 对 DataTable 进行排序
                                DataView view = new DataView(magic);
                                string orderby = nSql.Substring(nSql.IndexOf("order by") + 8);
                                // 按 Age 列升序排序
                                //view.Sort = orderby;
                                magic = view.ToTable();
                                magic.DefaultView.Sort = orderby;
                            }
                            else
                            {
                                where = nSql.Substring(nSql.IndexOf("where") + 5);
                                magic.DefaultView.RowFilter = where.Trim();
                            }
                        }

                        dt = magic.DefaultView.ToTable();
                    }
                    else if (nSql.Contains("monster"))
                    {
                        if (nSql.Contains("where"))
                        {
                            if (nSql.Contains("order"))
                            {
                                where = nSql.Substring(nSql.IndexOf("where") + 5, nSql.IndexOf("order") - nSql.IndexOf("where") - 5);
                                monster.DefaultView.RowFilter = where.Trim();

                                // 对 DataTable 进行排序
                                DataView view = new DataView(monster);
                                string orderby = nSql.Substring(nSql.IndexOf("order by") + 8);
                                // 按 Age 列升序排序
                                //view.Sort = orderby;
                                monster = view.ToTable();
                                monster.DefaultView.Sort = orderby;
                            }
                            else
                            {
                                where = nSql.Substring(nSql.IndexOf("where") + 5);
                                monster.DefaultView.RowFilter = where.Trim();
                            }
                        }
                        dt = monster.DefaultView.ToTable();
                    }
                    else if (nSql.Contains("stditems"))
                    {
                        if (nSql.Contains("where"))
                        {
                            if (nSql.Contains("order"))
                            {
                                where = nSql.Substring(nSql.IndexOf("where") + 5, nSql.IndexOf("order") - nSql.IndexOf("where") - 5);

                                // 对 DataTable 进行排序
                                DataView view = new DataView(stditems);
                                view.RowFilter = where.Trim();
                                string orderby = nSql.Substring(nSql.IndexOf("order by") + 8);
                                // 按 Age 列升序排序
                                //view.Sort = orderby;
                                stditems = view.ToTable();
                                stditems.DefaultView.Sort = orderby;
                            }
                            else
                            {
                                where = nSql.Substring(nSql.IndexOf("where") + 5);
                                stditems.DefaultView.RowFilter = where.Trim();
                            }
                        }

                        dt = stditems.DefaultView.ToTable();
                    }
                    else
                    {
                        throw new Exception("查询错误！");
                    }
                }
                else
                {
                    throw new Exception("参数错误！");
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return dt;
        }


        public static DataTable GetDataBySQL(DBInfo db, string sql)
        {
            DataTable dt = new DataTable();
            IMonsterService MonsterService = null;
            try
            {
                string ConnectionString = "";

                switch (db.DataBaseType)
                {
                    case "Access":
                        ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + db.DataFilePath + ";Persist Security Info=False;";
                        PubConstant.ConnectionString = ConnectionString;
                        MonsterService = new MonsterServer(SqlSugar.DbType.Access);
                        break;
                    case "SQLite":
                        ConnectionString = "Data Source=" + db.DataFilePath;
                        PubConstant.ConnectionString = ConnectionString;
                        MonsterService = new MonsterServer(SqlSugar.DbType.Sqlite);
                        break;
                    case "MySQL":
                        if (db == null) throw new Exception("数据库对象为空，操作失败！");

                        ConnectionString = $"server={db.DataBaseAddr};Database={db.DataBaseName};Uid={db.DataBaseUserName};Pwd={db.DataBasePassWord}";
                        PubConstant.ConnectionString = ConnectionString;
                        MonsterService = new MonsterServer(SqlSugar.DbType.MySql);
                        break;
                    case "MSSQL":
                        if (db == null) throw new Exception("数据库对象为空，操作失败！");
                        ConnectionString = $"server={db.DataBaseAddr};Database={db.DataBaseName};Uid={db.DataBaseUserName};Pwd={db.DataBasePassWord}";
                        PubConstant.ConnectionString = ConnectionString;
                        MonsterService = new MonsterServer(SqlSugar.DbType.SqlServer);
                        break;
                }

                if (sql.ToLower().StartsWith("insert"))
                {
                    dt.Columns.Add("Result");
                    DataRow dr = dt.NewRow();
                    dr["Result"] = MonsterService.ExecuteSQL(sql) > 0 ? "添加成功！" : "添加失败！";
                    dt.Rows.Add(dr);
                }
                else if (sql.ToLower().StartsWith("delete"))
                {
                    dt.Columns.Add("Result");
                    DataRow dr = dt.NewRow();
                    dr["Result"] = MonsterService.ExecuteSQL(sql) > 0 ? "删除成功！" : "删除失败！";
                    dt.Rows.Add(dr);
                }
                else if (sql.ToLower().StartsWith("update"))
                {
                    dt.Columns.Add("Result");
                    DataRow dr = dt.NewRow();
                    dr["Result"] = MonsterService.ExecuteSQL(sql) > 0 ? "修改成功！" : "修改失败！";
                    dt.Rows.Add(dr);
                }
                else if (sql.ToLower().StartsWith("select"))
                {
                    dt = MonsterService.QuerySQL(sql);
                }
                else
                {
                    throw new Exception("参数错误！");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }
        public static bool CreateDBByExcel(string path)
        {
            try
            {
                // 创建一个新的 Workbook 对象
                Workbook workbook = new Workbook();

                // 保存 Excel 文件
                workbook.SaveToFile(path, ExcelVersion.Version2010);
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return true;
        }
        public static bool CreateTableByMSSQL(string tableName, string fields, DBInfo db)
        {
            try
            {
                string ConnectoinString = $"Server={db.DataBaseAddr};Integrated security=SSPI;database=master;uid={db.DataBaseUserName};pwd={db.DataBasePassWord};";

                string createTableQuery = $@"
                                            CREATE TABLE {tableName} (
                                                {fields}
                                            );";

                using (SqlConnection connection = new SqlConnection(ConnectoinString))
                {
                    SqlCommand command = new SqlCommand(createTableQuery, connection);

                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                        Console.WriteLine("表创建成功！");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"表创建失败: {ex.Message}");
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return true;
        }
        public static bool CreateDBByMSSQL(DBInfo db)
        {
            try
            {
                string ConnectoinString = $"Server={db.DataBaseAddr};Integrated security=SSPI;database=master;uid={db.DataBaseUserName};pwd={db.DataBasePassWord};";
                using (SqlConnection myConn = new SqlConnection(ConnectoinString))
                {
                    string name = $"{db.DataBaseName}_Data";
                    string dataFile = $"{db.DataFilePath}\\{name}.mdf";
                    string log = $"{db.DataBaseName}_Log";
                    string logFile = $"{db.DataFilePath}\\{log}.ldf";

                    string str = "CREATE DATABASE MyDatabase ON PRIMARY " +
                                 "(NAME = " + name + ", " +
                                 "FILENAME = '" + dataFile + "', " +
                                 "SIZE = 2MB, MAXSIZE = 10MB, FILEGROWTH = 10%)" +
                                 "LOG ON (NAME = " + log + ", " +
                                 "FILENAME = '" + logFile + "', " +
                                 "SIZE = 1MB, " +
                                 "MAXSIZE = 5MB, " +
                                 "FILEGROWTH = 10%)";

                    SqlCommand myCommand = new SqlCommand(str, myConn);
                    myConn.Open();
                    myCommand.ExecuteNonQuery();
                }
                string tableName = "Magic";
                string fields = "MagID INT PRIMARY KEY IDENTITY, MagName VARCHAR(500),EffectType int,Effect int,Spell int,Power int,MaxPower int,DefSpell int,DefPower int,DefMaxPower int,Job int,NeedL1 int,L1Train int,NeedL2 int,L2Train int,NeedL3 int,L3Train int,Delay int,Descr int,NeedL4 int,L4Train int,NeedL5 int,L5Train int,NeedL6 int,L6Train int,NeedL7 int,L7Train int,NeedL8 int,L8Train int,NeedL9 int,L9Train int,NeedL10 int,L10Train int,NeedL11 int,L11Train int,NeedL12 int,L12Train int,NeedL13 int,L13Train int,NeedL14 int,L14Train int,NeedL15 int,L15Train int,MaxTrainLv int,CanUpgrade int,MaxUpgradeLv int";
                CreateTableByMSSQL(tableName, fields, db);

                tableName = "Monster";
                fields = "Name VARCHAR(500),Race int,RaceImg int,Appr int,Lvl int,Undead int,CoolEye int,HP int,MP int,AC int,MAC int,DC int,DCMAX int,MC int,SC int,SPEED int,HIT int,WALK_SPD int,WALKSTEP int,WALKWAIT int,ATTACK_SPD int,Exp int,ExploreItem int,InLevel int,IPExp int,AttackState int,AttackSource int,DisableSimpleActor int,MapType int";
                CreateTableByMSSQL(tableName, fields, db);

                tableName = "StdItems";
                fields = "Idx int PRIMARY KEY IDENTITY, Name VARCHAR(500),Stdmode int,Shape int,Weight int,Anicount int,Source int,Reserved int,Looks int,DuraMax int,Ac int,Ac2 int,Mac int,Mac2 int,Dc int,Dc2 int,Mc int,Mc2 int,Sc int,Sc2 int,Need int,NeedLevel int,Price int,Stock int,Color int,OverLap int,HP int,MP int,Light int,Horse int,Element int,Expand1 int,Expand2 int,Expand3 int,Expand4 int,Element1 int,Element2 int,Element3 int,Element4 int,Element5 int,Element6 int,Element7 int,Element8 int,Element9 int,Element10 int,Element11 int,Element12 int,Element13 int,Element14 int,Element15 int,Element16 int,Element17 int,Element18 int,Element19 int,Element20 int,Expand5 int,InsuranceCurrenc int,InsuranceGold int,Element21 int,Element22 int,Element23 int,Element24 int,Element25 int,WeaponType int";
                CreateTableByMSSQL(tableName, fields, db);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return true;
        }

        public static void DoBackupByMySql(DBInfo db)
        {
            string connectionString = $"SERVER={db.DataBaseAddr};USER={db.DataBaseUserName};PASSWORD={db.DataBasePassWord};";
            string backupDatabaseName = $"HeroDB_{DateTime.Now.ToString("yyyyMMddHHmmss")}_bak";
            string dataFilePath = Path.GetDirectoryName(db.DataFilePath);
            string backupFilePath = $"{dataFilePath}\\{backupDatabaseName}.sql";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // 创建备份命令
                    string sql = $"c --opt -u root -p {backupDatabaseName} > {backupFilePath}";

                    // 执行备份命令
                    MySqlCommand command = new MySqlCommand(sql, connection);
                    int num = command.ExecuteNonQuery();


                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public static bool CreateTableByMySQL(string tableName, string fields, DBInfo db)
        {
            string connectionString = $"SERVER={db.DataBaseAddr};USER={db.DataBaseUserName};PASSWORD={db.DataBasePassWord};";
            using (var connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    // 选择数据库
                    string useDatabaseQuery = $"USE {db.DataBaseName};";
                    var useDbCommand = new MySqlCommand(useDatabaseQuery, connection);
                    useDbCommand.ExecuteNonQuery();

                    // 创建表
                    string createTableQuery = $"CREATE TABLE IF NOT EXISTS {tableName} " +
                                              $"({fields});";
                    var createTableCommand = new MySqlCommand(createTableQuery, connection);
                    createTableCommand.ExecuteNonQuery();
                }
                catch (MySqlException ex)
                {
                    throw ex;
                }
            }
            return true;
        }
        public static bool CreateDBByMySQL(DBInfo db)
        {
            try
            {
                string connectionString = $"SERVER={db.DataBaseAddr};USER={db.DataBaseUserName};PASSWORD={db.DataBasePassWord};";
                using (var connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    // 创建数据库
                    string createDatabaseQuery = $"CREATE DATABASE IF NOT EXISTS {db.DataBaseName};";
                    var createDbCommand = new MySqlCommand(createDatabaseQuery, connection);
                    createDbCommand.ExecuteNonQuery();
                }
                string tableName = "Magic";
                string fields = "MagID INT PRIMARY KEY AUTO_INCREMENT, MagName VARCHAR(500),EffectType int,Effect int,Spell int,Power int,MaxPower int,DefSpell int,DefPower int,DefMaxPower int,Job int,NeedL1 int,L1Train int,NeedL2 int,L2Train int,NeedL3 int,L3Train int,Delay int,Descr int,NeedL4 int,L4Train int,NeedL5 int,L5Train int,NeedL6 int,L6Train int,NeedL7 int,L7Train int,NeedL8 int,L8Train int,NeedL9 int,L9Train int,NeedL10 int,L10Train int,NeedL11 int,L11Train int,NeedL12 int,L12Train int,NeedL13 int,L13Train int,NeedL14 int,L14Train int,NeedL15 int,L15Train int,MaxTrainLv int,CanUpgrade int,MaxUpgradeLv int";
                CreateTableByMySQL(tableName, fields, db);

                tableName = "Monster";
                fields = "Name VARCHAR(500),Race int,RaceImg int,Appr int,Lvl int,Undead int,CoolEye int,HP int,MP int,AC int,MAC int,DC int,DCMAX int,MC int,SC int,SPEED int,HIT int,WALK_SPD int,WALKSTEP int,WALKWAIT int,ATTACK_SPD int,Exp int,ExploreItem int,InLevel int,IPExp int,AttackState int,AttackSource int,DisableSimpleActor int,MapType int";
                CreateTableByMySQL(tableName, fields, db);

                tableName = "StdItems";
                fields = "Idx int PRIMARY KEY AUTO_INCREMENT, Name VARCHAR(500),Stdmode int,Shape int,Weight int,Anicount int,Source int,Reserved int,Looks int,DuraMax int,Ac int,Ac2 int,Mac int,Mac2 int,Dc int,Dc2 int,Mc int,Mc2 int,Sc int,Sc2 int,Need int,NeedLevel int,Price int,Stock int,Color int,OverLap int,HP int,MP int,Light int,Horse int,Element int,Expand1 int,Expand2 int,Expand3 int,Expand4 int,Element1 int,Element2 int,Element3 int,Element4 int,Element5 int,Element6 int,Element7 int,Element8 int,Element9 int,Element10 int,Element11 int,Element12 int,Element13 int,Element14 int,Element15 int,Element16 int,Element17 int,Element18 int,Element19 int,Element20 int,Expand5 int,InsuranceCurrenc int,InsuranceGold int,Element21 int,Element22 int,Element23 int,Element24 int,Element25 int,WeaponType int";
                CreateTableByMySQL(tableName, fields, db);

                DoBackupByMySql(db);
            }
            catch (MySqlException ex)
            {
                throw ex;
            }
            return true;
        }


        public static bool CreateDBBySQLite(string path)
        {
            try
            {
                string ConnectionString = path;
                SQLiteConnection.CreateFile(ConnectionString);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return true;
        }

        public static void CreateTableBySQLite(string dbPath, string tableName, string fields, List<string> listSql = null)
        {
            using (SQLiteConnection conn = new SQLiteConnection("data source=" + dbPath))
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                    SQLiteCommand cmd = new SQLiteCommand();
                    cmd.Connection = conn;
                    //cmd.CommandText = "CREATE TABLE " + tableName + "(Name varchar,Team varchar, Number varchar)";
                    cmd.CommandText = "CREATE TABLE " + tableName + "(" + fields + ")";
                    cmd.ExecuteNonQuery();

                    if (listSql != null)
                    {
                        foreach (string sql in listSql)
                        {
                            using (SQLiteCommand command = new SQLiteCommand(sql, conn))
                            {
                                command.ExecuteNonQuery();
                            }
                        }
                    }
                }
            }
        }

        public static void CreateTableByAccess(string path, string tableName, string fields, List<string> listSql = null)
        {
            string ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path + ";Persist Security Info=False;";

            using (OleDbConnection conn = new OleDbConnection(ConnectionString))
            {
                string query = "CREATE TABLE " + tableName + " (" + fields + ")";
                conn.Open();
                using (OleDbCommand cmd = new OleDbCommand(query, conn))
                {
                    cmd.ExecuteNonQuery();

                    if (listSql != null)
                    {
                        foreach (string sql in listSql)
                        {
                            using (OleDbCommand command = new OleDbCommand(sql, conn))
                            {
                                command.ExecuteNonQuery();
                            }
                        }
                    }

                }
            }
        }

        public static bool CreateDBByAccess(string path)
        {
            try
            {
                // 创建新的ADOX Catalog 对象
                Catalog catalog = new Catalog();
                // 创建新的Access数据库
                catalog.Create("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path + ";");
                // 关闭catalog
                catalog.ActiveConnection = null;
                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public static bool ConnectionAccess(string path)
        {
            try
            {
                string ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path + ";Persist Security Info=False;";
                //using (OleDbConnection connection = new OleDbConnection(ConnectionString))
                //{
                //    connection.Open(); // 打开连接
                //    string sql = "select count(1) from Monster";
                //    OleDbCommand command = new OleDbCommand(sql, connection);
                //    num = (int)command.ExecuteScalar();
                //    connection.Close(); // 关闭连接
                //}
                //if (num > 0)
                //    return true;
                //else
                //    return false;


                PubConstant.ConnectionString = ConnectionString;
                IMonsterService MonsterService = new MonsterServer(SqlSugar.DbType.Access);
                bool isExists = MonsterService.ConnectionDB();
                if (isExists)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public static bool ConnectionSqlite(string path)
        {
            bool isExists = false;
            try
            {
                string ConnectionString = "Data source=" + path;

                SqliteHelper.ConnectionString = ConnectionString;

                PubConstant.ConnectionString = ConnectionString;
                IMonsterService MonsterService = new MonsterServer(SqlSugar.DbType.Sqlite);
                isExists = MonsterService.ConnectionDB();

                //int num = MonsterService.GetList().Count();
                //if (num > 0) isExists = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return isExists;
        }
        public static bool ConnectionMySql(string addr, string dbname, string username, string password)
        {
            try
            {
                string ConnectionString = $"server={addr};Database={dbname};Uid={username};Pwd={password}";
                PubConstant.ConnectionString = ConnectionString;
                IMonsterService MonsterService = new MonsterServer(SqlSugar.DbType.MySql);
                int num = MonsterService.GetList().Count();
                if (num > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public static bool ConnectionMsSql(string addr, string dbname, string username, string password)
        {
            try
            {

                string ConnectionString = $"server={addr};Database={dbname};Uid={username};Pwd={password}";
                PubConstant.ConnectionString = ConnectionString;
                IMonsterService MonsterService = new MonsterServer(SqlSugar.DbType.SqlServer);
                int num = MonsterService.GetList().Count();
                if (num > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static bool ConnectionExcel(string path)
        {
            try
            {
                Workbook wb = new Workbook();
                wb.LoadFromFile(path);
                int num = wb.Worksheets.Count;
                if (num > 0)
                    return true;
                else
                    return false;

                ////获取第一张工作表
                //Worksheet sheet = wb.Worksheets[0];

                ////获取包含数据的单元格区域
                //CellRange locatedRange = sheet.AllocatedRange;

                ////遍历其中的每一行
                //for (int i = 0; i < locatedRange.Rows.Length; i++)
                //{
                //    //遍历其中的每一列
                //    for (int j = 0; j < locatedRange.Rows[i].ColumnCount; j++)
                //    {
                //        //获取单元格数据
                //        Console.Write(locatedRange[i + 1, j + 1].Value + "  ");
                //    }
                //    Console.WriteLine();
                //}

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        /// <summary>
        /// Access查询
        /// </summary>
        /// <param name="table">表名</param>
        /// <param name="path">文件地址</param>
        /// <returns></returns>
        public static DataTable GetDataByAccess(string table, string path)
        {
            try
            {
                DataTable dt = new DataTable();
                string ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path + ";Persist Security Info=False;";
                //using (OleDbConnection connection = new OleDbConnection(ConnectionString))
                //{
                //    connection.Open(); // 打开连接
                //    string sql = "select * from " + table;
                //    OleDbDataAdapter adapter = new OleDbDataAdapter(sql, connection);
                //    adapter.Fill(dt);
                //}

                PubConstant.ConnectionString = ConnectionString;
                switch (table)
                {
                    case "Monster":
                        IMonsterService MonsterService = new MonsterServer(SqlSugar.DbType.Access);
                        dt = MonsterService.GetMonsterByTable();
                        break;
                    case "StdItems":
                        IStdItemsService StdItemsService = new StdItemsServer(SqlSugar.DbType.Access);
                        dt = StdItemsService.GetStdItemsByTable("");
                        break;
                    case "Magic":
                        IMagicService MagicService = new MagicServer(SqlSugar.DbType.Access);
                        dt = MagicService.GetMagicByTable();
                        break;
                }

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Sqlite查询
        /// </summary>
        /// <param name="path">文件地址</param>
        /// <returns></returns>
        public static DataTable GetDataBySqlite(string table, string path)
        {
            try
            {
                string ConnectionString = "data source=" + path;
                PubConstant.ConnectionString = ConnectionString;
                DataTable dt = new DataTable();
                switch (table)
                {
                    case "Monster":
                        IMonsterService MonsterService = new MonsterServer(SqlSugar.DbType.Sqlite);
                        dt = MonsterService.GetMonsterByTable();
                        break;
                    case "StdItems":
                        IStdItemsService StdItemsService = new StdItemsServer(SqlSugar.DbType.Sqlite);
                        dt = StdItemsService.GetStdItemsByTable("");
                        break;
                }
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Mysql查询
        /// </summary>
        public static DataTable GetDataByMySql(DBInfo dBInfo, string table)
        {
            try
            {
                string addr = dBInfo.DataBaseAddr;
                string dbname = dBInfo.DataBaseName;
                string username = dBInfo.DataBaseUserName;
                string password = dBInfo.DataBasePassWord;
                string ConnectionString = $"server={addr};Database={dbname};Uid={username};Pwd={password}";
                PubConstant.ConnectionString = ConnectionString;

                DataTable dt = new DataTable();
                switch (table)
                {
                    case "Monster":
                        IMonsterService MonsterService = new MonsterServer(SqlSugar.DbType.MySql);
                        dt = MonsterService.GetList().ToDataTable();
                        break;
                    case "StdItems":
                        IStdItemsService StdItemsService = new StdItemsServer(SqlSugar.DbType.MySql);
                        dt = StdItemsService.GetList().ToDataTable();
                        break;
                }

                return dt;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static DataTable GetDataByMsSql(DBInfo dBInfo, string table)
        {
            try
            {
                string addr = dBInfo.DataBaseAddr;
                string dbname = dBInfo.DataBaseName;
                string username = dBInfo.DataBaseUserName;
                string password = dBInfo.DataBasePassWord;
                string ConnectionString = $"server={addr};Database={dbname};Uid={username};Pwd={password}";
                PubConstant.ConnectionString = ConnectionString;
                DataTable dt = new DataTable();
                switch (table)
                {
                    case "Monster":
                        IMonsterService MonsterService = new MonsterServer(SqlSugar.DbType.SqlServer);
                        dt = MonsterService.GetList().ToDataTable();
                        break;
                    case "StdItems":
                        IStdItemsService StdItemsService = new StdItemsServer(SqlSugar.DbType.SqlServer);
                        dt = StdItemsService.GetList().ToDataTable();
                        break;
                }

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



    }
}
