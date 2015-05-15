using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NTNL.Models.DB.DTO;
using System.Data.Linq;
using System.Data.Linq.Mapping;

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
            this.ID = dto.getID();
            this.TwitterID = dto.getTwitterID();
            this.TagName = dto.getTagName();
        }

        public TagDTO createDTO()
        {
            TagDTO dto = new TagDTO();
            dto.setID(dto.getID());
            dto.setTwitterID(dto.getTwitterID());
            dto.setTagName(dto.getTagName());

            return dto;
        }
    }
}
