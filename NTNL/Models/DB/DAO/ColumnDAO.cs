using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data.SQLite.Linq;
using NTNL.Models.DB.Entity;
using NTNL.Models.DB.DTO;
using NTNL.NTNL_Config;

namespace NTNL.Models.DB.DAO
{
    class ColumnDAO : EntityDAO
    {
        public ColumnDAO(SQLiteConnection dbConnectionString)
            : base(dbConnectionString, DBConstants.COLUMN_NUM, DBConstants.COLUMN_NAME)
        {
        }
        public void insertColumn(ColumnDTO dto)
        {
            try
            {
                using (var cn = new SQLiteConnection(DBConstants.DB_CONNECTION))
                {
                    cn.Open();
                    using (SQLiteTransaction trans = cn.BeginTransaction())
                    {
                        SQLiteCommand cmd = cn.CreateCommand();

                        // インサート文
                        //cmd.CommandText = "INSERT INTO TAG(TwitterID, TagName) VALUES (@TwitterID_T, @TagName_T)";
                        cmd.CommandText = "INSERT INTO " + DBConstants.Column_TABLE + "(" + DBConstants.COLUMN_NUM+ "," + DBConstants.COLUMN_NAME +","+DBConstants.COLUMN_QUERY+ ") VALUES (@" + DBConstants.param_COLUMN_NUM + ",@" + DBConstants.param_COLUMN_NAME +",@"+ DBConstants.param_COLUMN_QUERY + ")";
                        // パラメータのセット
                        cmd.Parameters.Add(DBConstants.param_COLUMN_NUM, System.Data.DbType.Int32);
                        cmd.Parameters.Add(DBConstants.param_COLUMN_NAME, System.Data.DbType.String);
                        cmd.Parameters.Add(DBConstants.param_COLUMN_QUERY, System.Data.DbType.String);

                        // データの追加
                        cmd.Parameters[DBConstants.param_COLUMN_NUM].Value = dto.NUM;
                        cmd.Parameters[DBConstants.param_COLUMN_NAME].Value = dto.NAME;
                        cmd.Parameters[DBConstants.param_COLUMN_QUERY].Value = dto.QUERY;


                        cmd.ExecuteNonQuery();

                        // コミット
                        trans.Commit();
                        cn.Close();
                    }
                }
            }
            catch (Exception)
            {
                Console.WriteLine("same column cannot insert.");
            }

        }

        public List<ColumnDTO> getCoulumnALL()
        {
            try
            {
                using (var cn = new SQLiteConnection(DBConstants.DB_CONNECTION))
                {
                    cn.Open();
                    SQLiteCommand cmd = cn.CreateCommand();
                    cmd.CommandText = "SELECT * FROM " + DBConstants.Column_TABLE;
                    SQLiteDataReader sr = cmd.ExecuteReader();

                    var list = new List<ColumnDTO>();
                    while (sr.Read())
                    {
                        var dto = new Column((int)sr[DBConstants.COLUMN_NUM], sr[DBConstants.COLUMN_NAME].ToString(), sr[DBConstants.COLUMN_QUERY].ToString());
                         list.Add(dto.createDTO());
                    }
                    cn.Close();
                    return list;
                 }
            }
            catch (Exception)
            {
                Console.WriteLine("Cannot return Column List");
                return null;
            }

        }
        public void deleteColumn(ColumnDTO dto)
        {
            try
            {
                using (var cn = new SQLiteConnection(DBConstants.DB_CONNECTION))
                {
                    cn.Open();
                    using (SQLiteTransaction trans = cn.BeginTransaction())
                    {
                        SQLiteCommand cmd = cn.CreateCommand();

                        // デリート文
                        cmd.CommandText = "DELETE FROM " + DBConstants.Column_TABLE + " WHERE "  + DBConstants.COLUMN_NAME + " = @" + DBConstants.param_COLUMN_NUM ;
                        // パラメータのセット
                        //cmd.Parameters.Add(DBConstants.param_COLUMN_NUM, System.Data.DbType.Int32);
                        cmd.Parameters.Add(DBConstants.param_COLUMN_NAME, System.Data.DbType.String);
                        //cmd.Parameters.Add(DBConstants.param_COLUMN_QUERY, System.Data.DbType.String);

                        // データの追加
                        //cmd.Parameters[DBConstants.param_COLUMN_NUM].Value = dto.NUM;
                        cmd.Parameters[DBConstants.param_COLUMN_NAME].Value = dto.NAME;
                        //cmd.Parameters[DBConstants.param_COLUMN_QUERY].Value = dto.QUERY;


                        cmd.ExecuteNonQuery();

                        // コミット
                        trans.Commit();
                        cn.Close();
                    }
                }
            }
            catch (Exception)
            {
                Console.WriteLine("same column cannot insert.");
            }

        }


    }
}
