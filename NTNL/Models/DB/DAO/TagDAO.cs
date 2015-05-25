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
    class TagDAO : EntityDAO 
    {
        public TagDAO(SQLiteConnection dbConnectionString)
            : base(dbConnectionString, DBConstants.TAG_ID, DBConstants.TAG_TwitterID)
        {
        }

        public TagDTO findTag(int id)
        {
            TagDTO dto;

            SQLiteDataReader sr = this.find(id);
            dto = sr.NextResult() ? toDTO(sr) : null;

            return dto;
        }

        public TagDTO findTagFromTwitterID(String twitterID)
        {
            TagDTO dto;

            SQLiteDataReader sr = this.findIdentity(DBConstants.TAG_TwitterID, twitterID);
            dto = sr.NextResult() ? toDTO(sr) : null;

            return dto;
        }

        public TagDTO selectTag(Dictionary<String, Object> where)
        {
            TagDTO dto;

            SQLiteDataReader sr = this.select(where);
            dto = sr.NextResult() ? toDTO(sr) : null;

            return dto;
        }

        public List<TagDTO> selectTagAll(Dictionary<String, Object> where)
        {
            return toDTOAll(this.select(where));
        }

        public int insertTag(TagDTO dto)
        {
            Dictionary<String, Object> values = toValues(dto);
            return this.insert(values);
        }

        public int registTag(String TagName)
        {
            var values = new Dictionary<String, Object>();
            values.Add(DBConstants.TAG_TagName, TagName);
            return this.insert(values);

        }

        private static TagDTO toDTO(SQLiteDataReader sr)
        {
            var dto = new TagDTO();
            //dto.ID = sr.GetInt32(sr.StepCount);
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
            //values.Add(DBConstants.TAG_ID, dto.ID);
            values.Add(DBConstants.TAG_TwitterID, dto.TwitterID);
            values.Add(DBConstants.TAG_TagName, dto.TagName);

            return values;
        }

        private List<Dictionary<String, Object>> toValuesAll(List<TagDTO> dtos)
        {
            //? ArrayList
            var ValueList = new List<Dictionary<String, Object>>();
            foreach (TagDTO dto in dtos)
            {
                ValueList.Add(toValues(dto));
            }
            return ValueList;
        }
        
    }
}
