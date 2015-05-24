using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NTNL.Models.DB.DTO;

namespace NTNL.Models.DB.Entity
{
    class Tag
    {
        private int ID;
        private String TwitterID;
        private String TagName;

        public Tag(int ID, String TwitterID, String TagName)
        {
            this.ID = ID;
            this.TwitterID = TwitterID;
            this.TagName = TagName;
        }

        public Tag(TagDTO dto)
        {
            this.ID = dto.ID;
            this.TwitterID = dto.TwitterID;
            this.TagName = dto.TagName;
        }

        public TagDTO createDTO()
        {
            var dto = new TagDTO();
            dto.ID = ID;
            dto.TwitterID = TwitterID;
            dto.TagName = TagName;

            return dto;
        }
    }
}
