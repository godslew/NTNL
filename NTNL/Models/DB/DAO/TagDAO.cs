using NTNL.Models.DB.DTO;
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
    class TagDAO : SuperDAO 
    {
        public TagDAO(SQLiteConnection dbConnectionString, String tableName)
            : base(dbConnectionString, tableName)
        {
        }

        private static TagDTO toDTO(SQLiteDataReader sr)
        {
            TagDTO dto = new TagDTO();
            dto.ID = sr.GetInt32(sr.StepCount);
            dto.TwitterID = sr.GetString(sr.StepCount);

            return dto;
        }

        private static List<TagDTO> toDTOAll(SQLiteDataReader sr)
        {
            var dtos = new List<TagDTO>();
            while (sr.Read())
            {
                dtos.Add(toDTO(sr));
            }
            sr.Close();
            return dtos;
        }

        private static Dictionary<String, Object> toValues(TagDTO dto)
        {
            var values = new Dictionary<String, Object>();
            values.Add(DBConstants.TAG_ID, dto.ID);
            values.Add(DBConstants.TAG_TwitterID, dto.TwitterID);
            values.Add(DBConstants.TAG_TagName, dto.TagName);

            return values;
        }    

        
    }
}
