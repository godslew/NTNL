
using NTNL.Models.DB.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace NTNL.Models.DB.Entity
{
    class Account
    {
        private int ID;
        private String TwitterID;
        private String CK;
        private String CS;
        private String AT;
        private String ATS;

        public Account(int ID, String TwitterID, String CK, String CS, String AT, String ATS){
            this.ID=ID;
            this.TwitterID=TwitterID;
            this.CK=CK;
            this.CS=CS;
            this.AT = AT;
            this.ATS = ATS;
        }

        public Account(AccountDTO dto)
        {
            this.ID = dto.ID;
            this.TwitterID = dto.TwitterID;
            this.CK = dto.CK;
            
        }

        public AccountDTO createDTO()
        {
            AccountDTO dto = new AccountDTO();
            dto.setID(dto.ID);
            dto.setTwitterID(dto.TwitterID);
           
            return dto;
        }
        
    }
}
