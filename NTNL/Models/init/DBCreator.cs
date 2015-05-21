using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NTNL.NTNL_Config;
using System.Data.SQLite;
using System.Data.SQLite.Generic;


/*
 *ここにDBを初期化するものを書く
 */
namespace NTNL.Models.init
{
    class DBCreator
    {
        //DB作成
        public static void CREATE_DB()
        {
            String dbc = DBConstants.DB_CONNECTION;
            using (SQLiteConnection cn = new SQLiteConnection(dbc))
            {
                try
                {
                    cn.Open();
                    Console.WriteLine(dbc+"に接続しました");
                }
                catch(Exception exception)
                {
                    Console.WriteLine(dbc + "を作成しました");
                    SQLiteCommand cmd = cn.CreateCommand();
                    cmd.CommandText = DBConstants.CREATE_TABLE_ACCOUNT;
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = DBConstants.CREATE_TABLE_MUTE;
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = DBConstants.CREATE_TABLE_TAG;
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
