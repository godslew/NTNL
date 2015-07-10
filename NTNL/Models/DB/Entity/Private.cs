using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using NTNL.Models.DB.DTO;

namespace NTNL.Models.DB.Entity
{
    class Private
    {
        
        private String TwitterID;
        private String NGword;

        public Private(String TwitterID , String NGword)
        {
            this.TwitterID = TwitterID;
            this.NGword = NGword;
        }

        public Private(PrivateDTO dto)
        {
            this.TwitterID = dto.TwitterID;
            this.NGword = dto.NGword;
        }

        public PrivateDTO createDTO()
        {
            var dto = new PrivateDTO();
            dto.TwitterID = TwitterID;
            dto.NGword = NGword;
            return dto;
        }
    }
}
