using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace NTNL.Models.DB.DTO
{
    class ColumnDTO
    {
        public int NUM {get; set; }
        public String NAME { get; set; }
        public String QUERY { get; set; }
        public String TwitterID { get; set; }
    }
}
