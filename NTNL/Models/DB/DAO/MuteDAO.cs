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

        public int insertMute(MuteDTO dto)
        {
            Dictionary<String, Object> values = toValues(dto);
            return this.insert(values);
        }


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
    }
}
