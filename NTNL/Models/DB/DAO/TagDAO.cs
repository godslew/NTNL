using NTNL.Models.DB.DTO;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using NTNL.NTNL_Config;
using NTNL.Models.DB.Entity;


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
        /*
        public int insertTag(TagDTO dto)
        {
            Dictionary<String, Object> values = toValues(dto);
            return this.insert(values);
        }
        */
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

        public void insertTag(TagDTO dto)
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
                        cmd.CommandText = "INSERT INTO "+ DBConstants.Tag_TABLE+"(" + DBConstants.TAG_TwitterID + "," + DBConstants.TAG_TagName + ") VALUES (@" + DBConstants.param_Tag_TwitterID + ",@" + DBConstants.param_Tag_TagName + ")";

                        // パラメータのセット
                        cmd.Parameters.Add(DBConstants.param_Tag_TwitterID, System.Data.DbType.String);
                        cmd.Parameters.Add(DBConstants.param_Tag_TagName, System.Data.DbType.String);

                        // データの追加
                        cmd.Parameters[DBConstants.param_Tag_TwitterID].Value = dto.TwitterID;
                        cmd.Parameters[DBConstants.param_Tag_TagName].Value = dto.TagName;
                        

                        cmd.ExecuteNonQuery();

                        // コミット
                        trans.Commit();
                        cn.Close();
                    }
                }
            }
            catch (Exception)
            {
                Console.WriteLine("same tag cannot insert.");
            }

        }

        public List<TagDTO> getTagALL()
        {
            try
            {
                using (var cn = new SQLiteConnection(DBConstants.DB_CONNECTION))
                {
                    cn.Open();
                    SQLiteCommand cmd = cn.CreateCommand();
                    cmd.CommandText = "SELECT * FROM " + DBConstants.Tag_TABLE;
                    SQLiteDataReader sr = cmd.ExecuteReader();

                    var list = new List<TagDTO>();
                    while (sr.Read())
                    {
                        var dto = new Tag(sr[DBConstants.TAG_TwitterID].ToString(), sr[DBConstants.TAG_TagName].ToString());
                        list.Add(dto.createDTO());
                    }


                    cn.Close();
                    return list;

                }
            }
            catch(Exception)
            {
                Console.WriteLine("Cannot return TagList");
                return null;
            }
        }

    }
}
