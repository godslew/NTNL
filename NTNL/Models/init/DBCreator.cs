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
        /// <summary>
        /// DB作成
        /// </summary>
        public static void CREATE_DB()
        {
            String dbc = DBConstants.DB_CONNECTION;
            using (var cn = new SQLiteConnection(dbc))
            {
                try
                {
                    cn.Open();
                    SQLiteCommand cmd = cn.CreateCommand();
                    cmd.CommandText = DBConstants.CREATE_TABLE_ACCOUNT;
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = DBConstants.CREATE_TABLE_MUTE;
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = DBConstants.CREATE_TABLE_TAG;
                    cmd.ExecuteNonQuery();
                    Console.WriteLine(dbc + "を作成しました");
                    
                   
                    
                }
                catch(Exception)
                {

                    //cn.Open();
                    Console.WriteLine(dbc + "に接続しました");
                    
                    
                   
                }
            }
        }
    }
}
