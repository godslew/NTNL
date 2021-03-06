﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using NTNL.Models.DB.DTO;

namespace NTNL.Models.DB.Entity
{
    class Column
    {
        private int NUM;
        private String NAME;
        private String TwitterID;
        private String QUERY;

        public Column(int NUM, String NAME, String TwitterID ,String QUERY)
        {
            this.NUM = NUM;
            this.NAME = NAME;
            this.TwitterID = TwitterID;
            this.QUERY = QUERY;
        }
        
        public Column(ColumnDTO dto)
        {
            this.NUM = dto.NUM;
            this.NAME = dto.NAME;
            this.TwitterID = dto.TwitterID;
            this.QUERY = dto.QUERY;
        }
        
        public ColumnDTO createDTO()
        {
            var dto = new ColumnDTO();
            dto.NUM = NUM;
            dto.NAME = NAME;
            dto.TwitterID = TwitterID;
            dto.QUERY = QUERY;
            return dto;
        }
    }
}
