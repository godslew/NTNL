
using NTNL.Models.DB.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite.Linq;
using System.Data.Linq;
using System.Data.Linq.Mapping;



namespace NTNL.Models.DB.Entity
{ 
    class Account
    {
        private String TwitterID;
        private String CK;
        private String CS;
        private String AT;
        private String ATS;

        public Account(String TwitterID, String CK, String CS, String AT, String ATS){
            this.TwitterID = TwitterID;
            this.CK = CK;
            this.CS = CS;
            this.AT = AT;
            this.ATS = ATS;
        }

        public Account(AccountDTO dto)
        {
            this.TwitterID = dto.TwitterID;
            this.CK = dto.CK;
            this.CS = dto.CS;
            this.AT = dto.AT;
            this.ATS = dto.ATS;
        }

        public AccountDTO createDTO()
        {
            var dto = new AccountDTO();           
            dto.TwitterID=TwitterID;
            dto.CK = CK;
            dto.CS = CS;
            dto.AT = AT;
            dto.ATS = ATS;      
            return dto;
        }
    }
}
