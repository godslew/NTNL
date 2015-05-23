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
using System.Collections;

namespace NTNL.Models.DB.DAO
{
    class SuperDAO
    {
        //protected SQLiteConnection dbConnectionString ;
        protected String tableName; 
        SQLiteConnection dbConnectionString = new SQLiteConnection(DBConstants.DB_CONNECTION);

        
        //コンストラクタ
        public SuperDAO(SQLiteConnection dbConnectionString, String tableName)
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


        protected static String generateSqlPartsColumns(List<String> columnNameList)
        {
            if (columnNameList == null || columnNameList.Count == 0)
            {
                 return "*";
            }
                 String[] str = columnNameList.ToArray();
                 return helper.join( str,",");
        }

        protected static String generateSqlPartsWhere(Dictionary<String, Object> where)
        {
            List<String> str = where.Keys.ToList();
            return generateSqlPartsWhere(str, null);
        }
        protected static String generateSqlPartsWhere(Dictionary<String, Object> where, List<String> whereLimit)
        {
            List<String> str = where.Keys.ToList();
            return generateSqlPartsWhere(str, whereLimit);
        }
        protected static String generateSqlPartsSet(Dictionary<String, Object> values)
        {
            List<String> keys = values.Keys.ToList();
            return generateSqlPartsSet(keys);
        }

        protected static String generateSqlPartsSet(List<String> values)
        {
            String[] str = values.ToArray();
            return helper.join(str, "," , "=?");
        }
 
        protected static String generateSqlPartsValues(Dictionary<String, Object> values)
        {
            String[] signs = (String[])helper.filledArray("?", values.Count).ToArray();
            return helper.join(signs,",");
        }
        protected static String generateSqlPartsWhere(List<String> whereColumns, List<String> whereLimit)
        {
            String[] str = whereColumns.ToArray();
            String[] whereColumnsKey = str;
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
        //not yet
        protected int insert(Dictionary<String, Object> values, String option, String tableName)
        {
            String sqlBase = getSqlBaseInsert();
            sqlBase = sqlBase.Replace(DBConstants.PIECE_TABLE_NAME, tableName);
            List<String>  keyList = values.Keys.ToList();
            sqlBase = sqlBase.Replace(DBConstants.PIECE_COLUMNS,generateSqlPartsColumns(keyList));
            sqlBase = sqlBase.Replace(DBConstants.PIECE_VALUES, generateSqlPartsValues(values));
            using (SQLiteConnection cn = new SQLiteConnection(dbConnectionString))
            {
              cn.Open();
              using (SQLiteTransaction trans = cn.BeginTransaction())
              {
                  SQLiteCommand cmd = cn.CreateCommand();
                  String sqlPrep = sqlBase + (option != null ? option : "") + ";";
                  cmd.CommandText = sqlPrep;
                  setObjects(cmd, values);
                  cmd.ExecuteNonQuery();
                  SQLiteDataReader sr = this.select(values);
                  trans.Commit();
                  return sr.NextResult() ? sr.GetInt32(1) : -1;
              }
            }
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
        protected SQLiteDataReader select(Dictionary<String, Object> where, List<String>columnNameList, String option, String tableName, List<String>whereLimit )
        {
            String sqlBase = getSqlBaseSelect();
            sqlBase = sqlBase.Replace(DBConstants.PIECE_COLUMNS, generateSqlPartsColumns(columnNameList));
            sqlBase = sqlBase.Replace(DBConstants.PIECE_TABLE_NAME, tableName);
            sqlBase = sqlBase.Replace(DBConstants.PIECE_WHERE, (where != null && where.Count !=0 ) ? generateSqlPartsWhere(where, whereLimit): "TRUE");
            using (SQLiteConnection cn = new SQLiteConnection(dbConnectionString))
            {
                cn.Open();
                using (SQLiteTransaction trans = cn.BeginTransaction())
                {
                    SQLiteCommand cmd = cn.CreateCommand();
                    String sqlPrep = sqlBase + (option != null ? option : "") + ";";
                    cmd.CommandText = sqlPrep;
                    setObjects(cmd, where);
                    //?↓1行必要ないかも
                    cmd.ExecuteNonQuery();
                    //
                    SQLiteDataReader sr = cmd.ExecuteReader();
                    trans.Commit();
                    return sr;
                }
            }
        }
        protected SQLiteDataReader select(Dictionary<String, Object>where, List<String> columnNameList, String option, String tableName)
        {
            return this.select(where, columnNameList, option, this.tableName, null);
        }
        protected SQLiteDataReader select(Dictionary<String, Object>where, List<String> columnNameList, String option)
        {
            return this.select(where, columnNameList, option, this.tableName);
        }
        protected SQLiteDataReader select(Dictionary<String, Object> where, List<String> columnNameList)
        {
            return this.select(where, columnNameList, "");
        }
        protected SQLiteDataReader select(Dictionary<String, Object> where)
        {
            return this.select(where, null);
        }
        protected SQLiteDataReader selectTableAll()
        {
            return this.select(null);
        }

        //update
        //not yet
        protected void update(Dictionary<String, Object> set, Dictionary<String, Object> where, String option, String tableName)
        {
            String sqlBase = getSqlBaseUpdate();
            sqlBase = sqlBase.Replace(DBConstants.PIECE_TABLE_NAME, tableName);
            sqlBase = sqlBase.Replace(DBConstants.PIECE_SET, generateSqlPartsSet(set));
            sqlBase = sqlBase.Replace(DBConstants.PIECE_WHERE, generateSqlPartsWhere(where));
            using (SQLiteConnection cn = new SQLiteConnection(dbConnectionString))
            {
                cn.Open();
                using (SQLiteTransaction trans = cn.BeginTransaction())
                {
                    SQLiteCommand cmd = cn.CreateCommand();
                    String sqlPrep = sqlBase + (option != null ? option : "") + ";" ;
                    setObjects(cmd, set);
                    setObjects(cmd, where, set.Count +1);

                    cmd.ExecuteNonQuery();
                    trans.Commit();
                }
            }
            
           
            
        }
        protected void update(Dictionary<String, Object>set, Dictionary<String, Object>where, String option)
        {
            this.update(set,where,option,this.tableName);
        }
        protected void update(Dictionary<String, Object>set, Dictionary<String, Object>where )
        {
            this.update(set,where,"");
        }

        //delete
        //not yet
        protected void delete(Dictionary<String, Object>where, String option, String tableName)
        {
            String sqlBase = getSqlBaseDelete();
            sqlBase = sqlBase.Replace(DBConstants.PIECE_TABLE_NAME, tableName);
            sqlBase = sqlBase.Replace(DBConstants.PIECE_WHERE, generateSqlPartsWhere(where));
            using (SQLiteConnection cn = new SQLiteConnection(dbConnectionString))
            {
                cn.Open();
                using (SQLiteTransaction trans = cn.BeginTransaction())
                {
                    SQLiteCommand cmd = cn.CreateCommand();
                    String sqlPrep = sqlBase + (option != null ? option : "") + ";";
                    cmd.CommandText = sqlPrep;
                    setObjects(cmd, where);

                    cmd.ExecuteNonQuery();
                    trans.Commit();
                }
            }


        }
        protected void delete(Dictionary<String, Object>where , String option)
        {
            this.delete(where, option, this.tableName);
        }
        protected void delete(Dictionary<String, Object>where)
        {
            this.delete(where, "");
        }
        
/*
        //count
        protected int count(Dictionary<String, Object> where, String option)
        {
            int n = -1;
            
            SQLiteDataReader sr = this.select(where,)
        }
    
        //trancate
        public void trancate(String tableName)
        {
         
        }
 */


        //setObjects
        protected static void setObjects(SQLiteCommand cmd, Dictionary<String, Object>where, int start)
        {
            int i = start;
            if (where == null || where.Count == 0)
            {
                return;
            }
            foreach(String key in where.Keys){
                setObject(i++, cmd, where[key]);
            }
        }

        protected static void setObjects(SQLiteCommand cmd, List<Object>where, int start)
        {
            int i = start;
            if (where == null || where.Count == 0)
            {
                return;
            }
            foreach(Object value in where){
                setObject(i++ , cmd, value);
            }
        }

        protected static void setObjects(SQLiteCommand cmd, List<Object>where)
        {
            setObjects(cmd, where, 1);
        }

        protected static void setObjects(SQLiteCommand cmd, Dictionary<String, Object>where)
        {
            setObjects(cmd, where, 1);
        }
 
        //not yet
        protected static void setObject(int index, SQLiteCommand cmd, Object obj)
        {
            if (obj is String)
            {
               
            }
        }
    }
}
