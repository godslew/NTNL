using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data.SQLite.Linq;
using NTNL.Models.DB.DTO;
using NTNL.NTNL_Config;
using NTNL.Models;
using NTNL.Models.DB.Entity;


namespace NTNL.Models.DB.DAO
{
    class MuteDAO :EntityDAO
    {
        public MuteDAO(SQLiteConnection dbConnectionString)
           : base(dbConnectionString, DBConstants.MUTE_ID, DBConstants.MUTE_TwitterID)
        {
        }
        
        public MuteDTO findMute(int id)
        {
            MuteDTO dto;
            SQLiteDataReader sr = this.find(id);
            dto = sr.NextResult() ? toDTO(sr) : null;
            
            return dto ;
         }

        public MuteDTO findMuteFromTwitterID(String twitterID)
        {
            MuteDTO dto;

            SQLiteDataReader sr = this.findIdentity(DBConstants.MUTE_TwitterID, twitterID);
            dto = sr.NextResult() ? toDTO(sr) : null;

            return dto;
        }

        public MuteDTO selectMute(Dictionary<String, Object> where)
        {
            MuteDTO dto;
            SQLiteDataReader sr = this.select(where);
            dto = sr.NextResult() ? toDTO(sr) : null ;

            return dto;
        }

        public List<MuteDTO> selectMuteAll(Dictionary<String, Object> where)
        {
            return toDTOAll(this.select(where));
        }
        /*
        public int insertMute(MuteDTO dto)
        {
            Dictionary<String, Object> values = toValues(dto);
            return this.insert(values);
        }
        */

        public static MuteDTO toDTO(SQLiteDataReader sr)
        {
            var dto = new MuteDTO();
            //dto.ID = sr.GetInt32(sr.StepCount);
            dto.TwitterID = sr.GetString(sr.StepCount);
            return dto;
        }

        public static List<MuteDTO> toDTOAll(SQLiteDataReader sr)
        {
            var dtos = new List<MuteDTO>();
            while (sr.Read())
            {
                dtos.Add(toDTO(sr));
            }
            sr.Close();
            return dtos;
        }

        private static Dictionary<String, Object> toValues(MuteDTO dto)
        {
            var values = new Dictionary<String, Object>();
           // values.Add(DBConstants.MUTE_ID, dto.ID);
            values.Add(DBConstants.MUTE_TwitterID, dto.TwitterID);
            values.Add(DBConstants.MUTE_UserID, dto.userID);
            values.Add(DBConstants.MUTE_Media, dto.Media);
            values.Add(DBConstants.MUTE_Tweet, dto.Tweet);
            values.Add(DBConstants.MUTE_RT, dto.RT);
            values.Add(DBConstants.MUTE_Favorite, dto.Favorite);

            return values;
        }

        private static List<Dictionary<String, Object>> toValuesAll(List<MuteDTO> dtos)
        {
            //? ArrayListかも
            var valueList = new List<Dictionary<String, Object>>();
            foreach (MuteDTO dto in dtos)
            {
                valueList.Add(toValues(dto));
            }
            return valueList;
        }

        public void insertMute(MuteDTO dto)
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
                       // cmd.CommandText = "INSERT INTO Mute(TwitterID, UserID, Media, Tweet, RT, Favorite ) VALUES (@TwitterID_T, @UserID_T, @Media_T, @Tweert_T, @RT_T, @Favorite)";
                        cmd.CommandText = "INSERT INTO "+ DBConstants.Mute_TABLE +"(" + DBConstants.MUTE_TwitterID + "," + DBConstants.MUTE_UserID + "," + DBConstants.MUTE_Media + "," + DBConstants.MUTE_Tweet + "," + DBConstants.MUTE_RT + "," + DBConstants.MUTE_Favorite + ") VALUE (@" + DBConstants.param_Mute_TwitterID + ",@" + DBConstants.param_Mute_UserID + ",@" + DBConstants.param_Mute_Media + ",@" + DBConstants.param_Mute_Tweet + ",@" + DBConstants.param_Mute_RT + ",@" + DBConstants.param_Mute_Favorite + ")";

                        // パラメータのセット
                        cmd.Parameters.Add(DBConstants.param_Mute_TwitterID, System.Data.DbType.String);
                        cmd.Parameters.Add(DBConstants.param_Mute_UserID, System.Data.DbType.String);
                        cmd.Parameters.Add(DBConstants.param_Mute_Media, System.Data.DbType.String);
                        cmd.Parameters.Add(DBConstants.param_Mute_Tweet, System.Data.DbType.String);
                        cmd.Parameters.Add(DBConstants.param_Mute_RT, System.Data.DbType.String);
                        cmd.Parameters.Add(DBConstants.param_Mute_Favorite, System.Data.DbType.String);

                        // データの追加
                        cmd.Parameters[DBConstants.param_Mute_TwitterID].Value = dto.TwitterID;
                        cmd.Parameters[DBConstants.param_Mute_UserID].Value = dto.userID;
                        cmd.Parameters[DBConstants.param_Mute_Media].Value = dto.Media;
                        cmd.Parameters[DBConstants.param_ACCOUNT_TwitterID].Value = dto.Tweet;
                        cmd.Parameters[DBConstants.param_Mute_RT].Value = dto.RT;
                        cmd.Parameters[DBConstants.param_Mute_Favorite].Value = dto.Favorite;


                        cmd.ExecuteNonQuery();

                        // コミット
                        trans.Commit();
                        cn.Close();
                    }
                }
            }
            catch (Exception)
            {
                Console.WriteLine("same mute cannot insert.");
            }

        }

        public List<MuteDTO> getMuteALL()
        {
            using (var cn = new SQLiteConnection(DBConstants.DB_CONNECTION))
            {
                cn.Open();
                SQLiteCommand cmd = cn.CreateCommand();
                cmd.CommandText = "SELECT * FROM " +DBConstants.Mute_TABLE;
                SQLiteDataReader sr = cmd.ExecuteReader();

                var list = new List<MuteDTO>();
                while (sr.Read())
                {
                    var dto = new Mute(sr[DBConstants.MUTE_TwitterID].ToString(), sr[DBConstants.MUTE_UserID].ToString(), sr[DBConstants.MUTE_Media].ToString(), sr[DBConstants.MUTE_Tweet].ToString(), sr[DBConstants.MUTE_RT].ToString(), sr[DBConstants.MUTE_Favorite].ToString());
                    list.Add(dto.createDTO());
                }


                cn.Close();
                return list;

            }

        }

    }
}
