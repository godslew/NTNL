﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace NTNL.Models.DB.DTO
{
    class TagDTO
    {
        //public int ID { get; set; }
        public String TwitterID { get; set; }
        public String TagName { get; set; }

    }
}
