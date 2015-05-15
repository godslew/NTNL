using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data.SQLite.Linq;

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

        
        //insert
        
      
        //update


        //select
        

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
