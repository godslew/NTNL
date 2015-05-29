using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NTNL.Models.DB.DAO;
using NTNL.Models.DB.Entity;
using System.Data.SQLite;
using System.Data.SQLite.Linq;
using NTNL.Models.DB.DAO;
using NTNL.Models.DB.DTO;
using NTNL.Models.init;
using NTNL.NTNL_Config;

namespace NTNL.Models.DB
{
    class DBFacade
    {
        private AccountDAO accountDAO;
        private ColumnDAO columnDAO;
        private MuteDAO muteDAO;
        private PrivateDAO privateDAO;
        private TagDAO tagDAO;

        //private SQLiteConnection dbConnectionString ;
        SQLiteConnection dbConnectionString = new SQLiteConnection(DBConstants.DB_CONNECTION);

        public DBFacade()
        {
            DBCreator.CREATE_DB();
        }

        private void installAccountDAO()
        {
            if (this.accountDAO == null)
            {
                this.accountDAO = new AccountDAO(this.dbConnectionString);
            }
        }
        /*
        private void installColumnDAO()
        {
            if (this.columnDAO == null)
            {
                this.columnDAO = new ColumnDAO(this.dbConnectionString);
            }
        }
        */
        private void installMuteDAO()
        {
            if (this.muteDAO == null)
            {
                this.muteDAO = new MuteDAO(this.dbConnectionString);
            }
        }

        /*
        private void installPrivateDAO()
        {
            if (this.accountDAO == null)
            {
                this.privateDAO = new PrivateDAO(this.dbConnectionString);
            }
        }
        */
        //アカウントのデータをとってくる
        //
        
        

        private void installTagDAO()
        {
            if (this.tagDAO == null)
            {
                this.tagDAO = new TagDAO(this.dbConnectionString);
            }
        }

        public SuperDAO createSuperDAO()
        {
            return new SuperDAO(dbConnectionString, "");
        }

        //Account methods
        public int registerAccount(String twitterID)
        {
            installAccountDAO();
            return this.accountDAO.registAccount(twitterID);
        }
       
        /*
         *  DBの処理を全てしてくれる
         
        public Account getAccount(Account twitterAccount)
        {
            installAccountDAO();
        }
        */

        public void insertAccount(String TwitterID, String CK, String CS, String AT, String ATS)
        {
            var _entity = new Account(TwitterID, CK, CS, AT, ATS);
            var dto = _entity.createDTO();
            var dao = new AccountDAO(dbConnectionString);
            dao.insertAccount(dto);

        }
        
        
        public List<AccountDTO> getAccountList()
        {
            var dao = new AccountDAO(dbConnectionString);
            var reader = dao.getAccountALL();
            var list = dao.getAccountALL();
           
            return list;
        }

        public void insertMute(String TwitterID, String userID, String Media, String Tweet, String RT, String Favorite)
        {
            var _entity = new Mute(TwitterID, userID, Media, Tweet, RT, Favorite);
            var dto = _entity.createDTO();
            var dao = new MuteDAO(dbConnectionString);
            dao.insertMute(dto);
        }

        public List<MuteDTO> getMuteList()
        {
            var dao = new MuteDAO(dbConnectionString);
            //var reader = dao.getMuteALL();
            var list = dao.getMuteALL();

            return list;
        }

        public void insertTag(String TwitterID, String TagName)
        {
            var _entity = new Tag(TwitterID, TagName);
            var dto = _entity.createDTO();
            var dao = new TagDAO(dbConnectionString);
            dao.insertTag(dto);
        }

        public List<TagDTO> getTagList() 
        {
            var dao = new TagDAO(dbConnectionString);
            var reader = dao.getTagALL();
            var list = dao.getTagALL();

            return list;
        }
       

       /* 
        //get account list from DB
        public List<DTO.Account> getAccountList()  //List<DTO.Account>でok
        {
            var list = new List<Twitter.Account>();
            return list;
        }
        */
    }
}
