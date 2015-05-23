﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data.SQLite.Linq;
using NTNL.Models.DB.DTO;
using NTNL.NTNL_Config;
using NTNL.Models;
using System.Collections;

namespace NTNL.Models.DB.DAO
{
    class AccountDAO : EntityDAO
    {
        public AccountDAO(SQLiteConnection dbConnectionString)
            : base(dbConnectionString, DBConstants.ACCOUNT_ID, DBConstants.ACCOUNT_TwitterID)
        {
        }


        public AccountDTO findUser(int id)
        {
            AccountDTO dto;
            //?try文必要かも
            SQLiteDataReader sr = this.find(id);
            dto = sr.NextResult() ? toDTO(sr) : null;

            return dto;
        }

        public AccountDTO findUserFromTwitterID(String twitterID)
        {
            AccountDTO dto;
            
            SQLiteDataReader sr = this.findIdentity(DBConstants.ACCOUNT_TwitterID, twitterID);
            dto = sr.NextResult() ? toDTO(sr) : null;

            return dto;
        }

        public AccountDTO selectAccount(Dictionary<String, Object> where)
        {
            AccountDTO dto;
            SQLiteDataReader sr = this.select(where);
            dto = sr.NextResult() ? toDTO(sr) : null;

            return dto;
        }

        public List<AccountDTO> selectAccountAll(Dictionary<String, Object> where)
        {
            return toDTOAll(this.select(where));
        }

        public int insertAccount(AccountDTO dto)
        {
            Dictionary<String, Object> values = toValues(dto);
            return this.insert(values);
        }

        public int registAccount(String twitterID)
        {
            var values = new Dictionary<String, Object>();
            values.Add(DBConstants.ACCOUNT_TwitterID, twitterID);
            return this.insert(values);
        }

        private static AccountDTO toDTO(SQLiteDataReader sr)
        {
            AccountDTO dto = new AccountDTO();
            dto.ID = sr.GetInt32(sr.StepCount);
            dto.TwitterID = sr.GetString(sr.StepCount);
            return dto;
        }

        private static List<AccountDTO> toDTOAll(SQLiteDataReader sr)
        {
            var dtos = new List<AccountDTO>();
            while (sr.Read())
            {
                dtos.Add(toDTO(sr));
            }
            sr.Close();
            return dtos;
        }

        private static Dictionary<String, Object> toValues(AccountDTO dto)
        {
            Dictionary<String, Object> values = new Dictionary<String, Object>();
            values.Add(DBConstants.ACCOUNT_ID, dto.ID);
            values.Add(DBConstants.ACCOUNT_TwitterID, dto.TwitterID);
            
            values.Add(DBConstants.ACCOUNT_CK, dto.CK);
            values.Add(DBConstants.ACCOUNT_CS, dto.CS);
            values.Add(DBConstants.ACCOUNT_AT, dto.AT);
            values.Add(DBConstants.ACCOUNT_ATS, dto.ATS);
            
            return values;
        }
        
        private static List<Dictionary<String, Object>> toValuesAll(List<AccountDTO> dtos)
        {
            //? ArrayListかも
            var valueList = new List<Dictionary<String, Object>>(); 
            foreach(AccountDTO dto in dtos){
                valueList.Add(toValues(dto));
            }
            return valueList;
        }
        
}

}
