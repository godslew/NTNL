using System;
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
using NTNL.Models.DB.Entity;

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

        public void insertAccount(AccountDTO dto)
        {
            //string dbConnectionString = "Data Source=C:/sqlite3/"+DBname.Text+".sqlite3";
            try
            {
                using (var cn = new SQLiteConnection(DBConstants.DB_CONNECTION))
            {
                cn.Open();
                using (SQLiteTransaction trans = cn.BeginTransaction())
                {
                    SQLiteCommand cmd = cn.CreateCommand();

                    // インサート文
                    cmd.CommandText = "INSERT INTO ACCOUNT(TwitterID, CK, CS, AT, ATS ) VALUES (@TwitterID_T, @CK_T, @CS_T, @AT_T, @ATS_T)";

                    // パラメータのセット
                    cmd.Parameters.Add("TwitterID_T", System.Data.DbType.String);
                    cmd.Parameters.Add("CK_T", System.Data.DbType.String);
                    cmd.Parameters.Add("CS_T", System.Data.DbType.String);
                    cmd.Parameters.Add("AT_T", System.Data.DbType.String);
                    cmd.Parameters.Add("ATS_T", System.Data.DbType.String);

                    // データの追加
                    cmd.Parameters["TwitterID_T"].Value = dto.TwitterID;
                    cmd.Parameters["CK_T"].Value = dto.CK;
                    cmd.Parameters["CS_T"].Value = dto.CS;
                    cmd.Parameters["AT_T"].Value = dto.AT;
                    cmd.Parameters["ATS_T"].Value = dto.ATS;

                    cmd.ExecuteNonQuery();

                    // コミット
                    trans.Commit();
                    cn.Close();
                     }
                }
            }
            catch (Exception){
                Console.WriteLine("same account cannot insert.");
            }

        }

        public int registAccount(String twitterID)
        {
            var values = new Dictionary<String, Object>();
            values.Add(DBConstants.ACCOUNT_TwitterID, twitterID);
            return this.insert(values);
        }

        private static AccountDTO toDTO(SQLiteDataReader sr)
        {
            var dto = new AccountDTO();
            dto.TwitterID = sr.GetString(0);
            dto.CK = sr.GetString(1);
            dto.CS = sr.GetString(2);
            dto.AT = sr.GetString(3);
            dto.ATS = sr.GetString(4);

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
            var values = new Dictionary<String, Object>();

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

        public List<AccountDTO> getAccountALL()
        {
              using (var cn = new SQLiteConnection(DBConstants.DB_CONNECTION))
            {
                    cn.Open();
                    SQLiteCommand cmd = cn.CreateCommand();
                    cmd.CommandText = "SELECT * FROM ACCOUNT" ;
                    SQLiteDataReader sr = cmd.ExecuteReader();

                    var list = new List<AccountDTO>();
                    while (sr.Read())
                    {
                        var dto = new Account(sr[DBConstants.ACCOUNT_TwitterID].ToString(), sr[DBConstants.ACCOUNT_CK].ToString(), sr[DBConstants.ACCOUNT_CS].ToString(), sr[DBConstants.ACCOUNT_AT].ToString(), sr[DBConstants.ACCOUNT_ATS].ToString());
                        list.Add(dto.createDTO());
                    }

                        
                    cn.Close();
                    return list;
                  
           }
            
        }
}

}
