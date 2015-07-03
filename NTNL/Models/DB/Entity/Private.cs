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
        private int NUM;
        private String NGword;

        public Private(int NUM , String NGword)
        {
            this.NUM = NUM;
            this.NGword = NGword;
        }

        public Private(PrivateDTO dto)
        {
            this.NUM = dto.NUM;
            this.NGword = dto.NGword;
        }

        public PrivateDTO createDTO()
        {
            var dto = new PrivateDTO();
            dto.NUM = NUM;
            dto.NGword = NGword;
            return dto;
        }
    }
}
