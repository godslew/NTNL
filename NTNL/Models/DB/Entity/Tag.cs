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
        private String TwitterID;
        private String TagName;

        public Tag(String TwitterID, String TagName)
        {
            this.TwitterID = TwitterID;
            this.TagName = TagName;
        }

        public Tag(TagDTO dto)
        {
            this.TwitterID = dto.TwitterID;
            this.TagName = dto.TagName;
        }

        public TagDTO createDTO()
        {
            var dto = new TagDTO();
            dto.TwitterID = TwitterID;
            dto.TagName = TagName;
            return dto;
        }
    }
}
