using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTNL.Models.DB.DTO
{
    class MuteDTO
    {
        public int ID { get; set; }
        public String TwitterID { get; set; }
        public String userID { get; set; }
        public String Media { get; set; }
        public String Tweet { get; set; }
        public String RT { get; set; }
        public String Favorite { get; set; }

    }
}
