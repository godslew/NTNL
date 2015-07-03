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
        private int ID;
        private String NGword;

        public Private(int ID , String NGword)
        {
            this.ID = ID;
            this.NGword = NGword;
        }

        public Private(PrivateDTO dto)
        {
            this.ID = dto.ID;
            this.NGword = dto.NGword;
        }

        public PrivateDTO createDTO()
        {
            var dto = new PrivateDTO();
            dto.ID = ID;
            dto.NGword = NGword;
            return dto;
        }
    }
}
