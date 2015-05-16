using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data.SQLite.Linq;
using NTNL.Helper;
using NTNL;
using NTNL.NTNL_Config;

namespace NTNL.Models.DB.DAO
{
    class SuperDAO
    {
        protected String dbConnectionString ; 
        protected String tableName ;
        
        //コンストラクタ
        public SuperDAO(String dbConnectionString, String tableName)
        {
            this.dbConnectionString = dbConnectionString;
            this.tableName = tableName;
        }

        
        //SQLのテンプレート
        protected static String getSqlBaseInsert()
        {
            return DBConstants.SQLBASE_INSERT;
        }
        protected static String getSqlBaseSelect()
        {
            return DBConstants.SQLBASE_SELECT;
        }
        protected static String getSqlBaseUpdate()
        {
            return DBConstants.SQLBASE_UPDATE;
        }
        protected static String getSqlBaseDelete()
        {
            return DBConstants.SQLBASE_DELETE;
        }

        protected static String generateSqlPartsColumns(ICollection<String> columnNameList)
        {
            if (columnNameList == null || columnNameList.Empty)
            {
                 return "*";
            }
                 return helper.join(columnNameList.ToArray(new String[0]),",");
        }
        protected static String generateSqlPartsWhere(Dictionary<String, Object> where)
        {
            return generateSqlPartsWhere(where.Keys, null);
        }
        protected static String generateSqlPartsWhere(Dictionary<String, Object> where, List<String> whereLimit)
        {
            return generateSqlPartsWhere(where.Keys, whereLimit);
        }
        protected static String generateSqlPartsSet(Dictionary<String, Object> values)
        {
            return generateSqlPartsSet(values.Keys);
        }
        protected static String generateSqlPartsSet(ICollection<String> values)
        {
            return helper.join(values.ToArray(new String[0]), ",","=?");
        }
        protected static String generateSqlPartsValues(Dictionary<String, Object> values)
        {
            String[] signs = (String[])helper.filledArray("?", values.Count.ToArray(new String[0]));
            return helper.join(signs,",");
        }
        protected static String generateSqlPartsWhere(ICollection<String> whereColumns, List<String> whereLimit)
        {
            String[] whereColumnsKey = (String[])whereColumns.ToArray(new String[0]);
            String sql = "";
            for (int i = 0; i < whereColumnsKey.Length; i++)
            {
                sql += whereColumnsKey[i] + " " + (whereLimit != null && whereLimit[i] != null ? whereLimit[i] : "=") + " ? ";
                if (i != whereColumnsKey.Length - 1)
                {
                    sql += " and ";
                }
            }
            return sql;
        }

 
        //insert
        protected int insert(Dictionary<String, Object> values, String option, String tableName)
        {
            String sqlBase = getSqlBaseInsert();
            sqlBase = sqlBase.Replace(DBConstants.PIECE_TABLE_NAME, tableName);
            sqlBase = sqlBase.Replace(DBConstants.PIECE_COLUMNS,generateSqlPartsColumns(values.Keys));
            sqlBase = sqlBase.Replace(DBConstants.PIECE_VALUES, generateSqlPartsValues(values));
            SQLiteCommand prepStmt;
            String sqlPrep = sqlBase + (option != null ? option : "") + ";";
            

        }

        //must add "SQLException"
        protected int insert(Dictionary<String, Object> values, String option)
        {
            return this.insert(values, option, this.tableName);
        }

        protected int insert(Dictionary<String, Object> values)
        {
            return this.insert(values, "");
        }

        //select
        


        //update
        protected void update(Dictionary<String, Object> set, Dictionary<String, Object> where, String option, String tableName)
        {
            String sqlBase = getSqlBaseUpdate();
            sqlBase = sqlBase.Replace(DBConstants.PIECE_TABLE_NAME, tableName);
            sqlBase = sqlBase.Replace(DBConstants.PIECE_SET, generateSqlPartsSet(set));
            sqlBase = sqlBase.Replace(DBConstants.PIECE_WHERE, generateSqlPartsWhere(where));
            SQLiteCommand prepStmt;
            
        }


        //delete


        
        
         
         
 
        //Account追加
        protected void AddAccount(int ID, String TwitterID, String CK, String CS, String AT, String ATS)
        {
           // using (SQLiteConnection cn = new SQLiteConnection(dbConnectionString))
            using (var cn = new SQLiteConnection(dbConnectionString))
            {
                cn.Open();
                using (SQLiteTransaction trans = cn.BeginTransaction())
                {
                    SQLiteCommand cmd = cn.CreateCommand();

                    // インサート文
                    cmd.CommandText = "INSERT INTO ACCOUNT(ID, TwitterID, CK, CS, AT, ATS ) VALUES (@ID_T, @TwitterID_T, @CK_T, @CS_T, @AT_T, @ATS_T)";
                    
                    // パラメータのセット
                    cmd.Parameters.Add("ID_T", System.Data.DbType.Int16);
                    cmd.Parameters.Add("TwitterID_T", System.Data.DbType.String);
                    cmd.Parameters.Add("CK_T", System.Data.DbType.String);
                    cmd.Parameters.Add("CS_T", System.Data.DbType.String);
                    cmd.Parameters.Add("AT_T", System.Data.DbType.String);
                    cmd.Parameters.Add("ATS_T", System.Data.DbType.String);

                    // データの追加
                    cmd.Parameters["ID_T"].Value = ID;
                    cmd.Parameters["TwitterID_T"].Value = TwitterID;
                    cmd.Parameters["CK_T"].Value = CK;
                    cmd.Parameters["CS_T"].Value = CS;
                    cmd.Parameters["AT_T"].Value = AT;
                    cmd.Parameters["ATS_T"].Value = ATS;

                    cmd.ExecuteNonQuery();

                    // コミット
                    trans.Commit();
                }
            }
        }

        //Account参照
        protected void getID()
        {
            using (SQLiteConnection cn = new SQLiteConnection(dbConnectionString))
            {
                cn.Open();
                SQLiteCommand cmd = cn.CreateCommand();
                cmd.CommandText = "SELECT * FROM ACCOUNT";
                
                using (SQLiteDataReader reader = cmd.ExecuteReader())
                {
                   while (reader.Read())
                    {
                        //テキストの追加
                        Console.WriteLine("ID: " + reader["ID"].ToString() + "\t");
                        Console.WriteLine("TwitterID: " + reader["TwitterID"].ToString() + "\t");
                        Console.WriteLine("CK: " + reader["CK"].ToString() + "\t");
                        Console.WriteLine("CS: " + reader["CS"].ToString() + "\t");
                        Console.WriteLine("AT: " + reader["AT"].ToString() + "\t");
                        Console.WriteLine("ATS: " + reader["ATS"].ToString() + "\n");
                    }
                }
                cn.Close();
            }
        }

        //Mute追加
        public void AddMute(int ID, String TwitterID, String userID, String Media, String Tweet, String RT, String Favorite)
        {
            using (SQLiteConnection cn = new SQLiteConnection(dbConnectionString))
            {
                cn.Open();
                using (SQLiteTransaction trans = cn.BeginTransaction())
                {
                    SQLiteCommand cmd = cn.CreateCommand();

                    //インサート文
                    cmd.CommandText = "INSERT INTO (ID, TwitterID, userID, Media, Tweet, RT, Favorite)";

                    //パラメータのセット
                    cmd.Parameters.Add("ID", System.Data.DbType.Int16);
                    cmd.Parameters.Add("TwitterID", System.Data.DbType.String);
                    cmd.Parameters.Add("userID",System.Data.DbType.String);
                    cmd.Parameters.Add("Media", System.Data.DbType.String);
                    cmd.Parameters.Add("Tweet", System.Data.DbType.String);
                    cmd.Parameters.Add("RT", System.Data.DbType.String);
                    cmd.Parameters.Add("Favorite", System.Data.DbType.String);

                    // データの追加
                    cmd.Parameters["ID"].Value = ID;
                    cmd.Parameters["TwitterID"].Value = TwitterID;
                    cmd.Parameters["userID"].Value = userID;
                    cmd.Parameters["Media"].Value = Media;
                    cmd.Parameters["Tweet"].Value = Tweet;
                    cmd.Parameters["RT"].Value = RT;
                    cmd.Parameters["Favorite"].Value = Favorite;

                    cmd.ExecuteNonQuery();

                    // コミット
                    trans.Commit();

                }
            }
        }
        //Mute参照
        protected void getMute()
        {
            using (SQLiteConnection cn = new SQLiteConnection(dbConnectionString))
            {
                cn.Open();
                SQLiteCommand cmd = cn.CreateCommand();
                cmd.CommandText = "SELECT * FROM Mute";
                using (SQLiteDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        //テキストの追加
                        Console.WriteLine("ID: " + reader["ID"].ToString() + "\t");
                        Console.WriteLine("TwitterID: " + reader["TwitterID"].ToString() + "\t");
                        Console.WriteLine("userID: " + reader["userID"].ToString()+ "\t");
                        Console.WriteLine("Media: " + reader["Media"].ToString() + "\t");
                        Console.WriteLine("Tweet: " + reader["Tweet"].ToString() + "\t");
                        Console.WriteLine("RT: " + reader["RT"].ToString() + "\t");
                        Console.WriteLine("Favorite: " + reader["Favorite"].ToString() + "\n");
                    }
                }
                cn.Close();
            }
        }


        //Mute追加
        public void AddTag(int ID, String TwitterID, String Tagname ){
            using (SQLiteConnection cn = new SQLiteConnection(dbConnectionString))
            {
                cn.Open();
                using (SQLiteTransaction trans = cn.BeginTransaction())
                {
                    SQLiteCommand cmd = cn.CreateCommand();

                    // インサート文
                    cmd.CommandText = "INSERT INTO TAG (ID, TwitterID, TagName ) VALUES (@ID, @TwitterID, @TAG)";

                    // パラメータのセット
                    cmd.Parameters.Add("ID", System.Data.DbType.Int16);
                    cmd.Parameters.Add("TwitterID", System.Data.DbType.String);
                    cmd.Parameters.Add("TAG", System.Data.DbType.String);

                    // データの追加
                    cmd.Parameters["ID"].Value = ID;
                    cmd.Parameters["TwitterID"].Value = TwitterID;
                    cmd.Parameters["TAG"].Value = Tagname;
                    cmd.ExecuteNonQuery();

                    // コミット
                    trans.Commit();
                }
            }
        }
        
        //Tag一覧の取得
        private void gatTag(){
            using (SQLiteConnection cn = new SQLiteConnection(dbConnectionString))
            {
                cn.Open();
                SQLiteCommand cmd = cn.CreateCommand();
                cmd.CommandText = "SELECT * FROM TAG";
                using (SQLiteDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine("ID: " + reader["ID"].ToString() + "\t");
                        Console.WriteLine("TwitterID: " + reader["TwitterID"].ToString() + "\t");
                        Console.WriteLine("TagName: " + reader["TagName"].ToString() + "\n");
                    }
                }
                cn.Close();
            }
        }
        
    }
}
