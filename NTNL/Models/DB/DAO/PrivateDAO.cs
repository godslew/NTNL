using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data.SQLite.Linq;
using NTNL.Models.DB.DTO;
using NTNL.Models.DB.Entity;
using NTNL.NTNL_Config;

namespace NTNL.Models.DB.DAO
{
    class PrivateDAO : EntityDAO
    {
        public PrivateDAO(SQLiteConnection dbConnectionString)
            : base(dbConnectionString, DBConstants.Private_ID, DBConstants.Private_TwitterID)
        {
        }
        public void insertPrivate(PrivateDTO dto)
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
                        cmd.CommandText = "INSERT INTO " + DBConstants.Private_TABLE + "(" + DBConstants.Private_ID + "," + DBConstants.Private_TwitterID +","+ DBConstants.Private_NGword +  ") VALUES (@" + DBConstants.param_Private_ID + ",@" + DBConstants.param_Private_TwitterID  +",@" + DBConstants.param_Private_NGword + ")";
                        // パラメータのセット
                        cmd.Parameters.Add(DBConstants.param_Private_ID, System.Data.DbType.Int32);
                        cmd.Parameters.Add(DBConstants.param_Private_TwitterID, System.Data.DbType.String);
                        cmd.Parameters.Add(DBConstants.param_Private_NGword, System.Data.DbType.String);
                        
                        // データの追加
                        cmd.Parameters[DBConstants.param_Private_ID].Value = dto.ID;
                        cmd.Parameters[DBConstants.param_Private_TwitterID].Value = dto.TwitterID;
                        cmd.Parameters[DBConstants.param_Private_NGword].Value = dto.NGword;
                       
                        cmd.ExecuteNonQuery();

                        // コミット
                        trans.Commit();
                        cn.Close();
                    }
                }
            }
            catch (Exception)
            {
                Console.WriteLine("same NGword cannot insert.");
            }

        }

        public List<PrivateDTO> getPrivateALL()
        {
            try
            {
                using (var cn = new SQLiteConnection(DBConstants.DB_CONNECTION))
                {
                    cn.Open();
                    SQLiteCommand cmd = cn.CreateCommand();
                    cmd.CommandText = "SELECT * FROM " + DBConstants.Private_TABLE;
                    SQLiteDataReader sr = cmd.ExecuteReader();

                    var list = new List<PrivateDTO>();
                    while (sr.Read())
                    {
                        var dto = new Private((int)sr[DBConstants.Private_ID], sr[DBConstants.param_Private_TwitterID].ToString() , sr[DBConstants.Private_NGword].ToString());
                        list.Add(dto.createDTO());
                    }
                    cn.Close();
                    return list;
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Cannot return Private List");
                return null;
            }

        }
        public void deletePrivate(PrivateDTO dto, int ID)
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
                        cmd.CommandText = "DELETE FROM " + DBConstants.Private_TABLE + " WHERE " + DBConstants.Private_ID + " = @" + DBConstants.param_Private_ID;
                        // パラメータのセット
                        cmd.Parameters.Add(DBConstants.param_Private_ID, System.Data.DbType.Int32);

                        // データの追加
                        cmd.Parameters[DBConstants.param_Private_ID].Value = ID;

                        cmd.ExecuteNonQuery();

                        // コミット
                        trans.Commit();
                        cn.Close();
                    }
                }
            }
            catch (Exception)
            {
                Console.WriteLine("private cannot delete.");
            }

        }
    }
}
