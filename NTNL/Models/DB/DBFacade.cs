﻿using System;
using System.Collections.Generic;
using NTNL.Models.DB.DAO;
using NTNL.Models.DB.Entity;
using System.Data.SQLite;
using NTNL.Models.DB.DTO;
using NTNL.Models.init;
using NTNL.NTNL_Config;
using System.Threading.Tasks;

namespace NTNL.Models.DB
{
    class DBFacade
    {
        private AccountDAO accountDAO;
        private ColumnDAO columnDAO;
        private MuteDAO muteDAO;
        private PrivateDAO privateDAO;
        private TagDAO tagDAO;
        SQLiteConnection dbConnectionString = new SQLiteConnection(DBConstants.DB_CONNECTION);

        public DBFacade()
        {
            DBCreator.CREATE_DB();
        }

        #region singleton
        static DBFacade _instance;

        /// <summary>
        /// DBFacadeの唯一のインスタンスを取得します。
        /// </summary>
        public static DBFacade Instance
        {
            get
            {
                if (_instance == null) _instance = new DBFacade();
                return _instance;
            }
        }
        #endregion

        private void installAccountDAO()
        {
            if (this.accountDAO == null)
            {
                this.accountDAO = new AccountDAO(this.dbConnectionString);
            }
        }
        
        private void installColumnDAO()
        {
            if (this.columnDAO == null)
            {
                this.columnDAO = new ColumnDAO(this.dbConnectionString);
            }
        }
        
        private void installMuteDAO()
        {
            if (this.muteDAO == null)
            {
                this.muteDAO = new MuteDAO(this.dbConnectionString);
            }
        }

        private void installPrivateDAO()
        {
            if (this.accountDAO == null)
            {
                this.privateDAO = new PrivateDAO(this.dbConnectionString);
            }
        }
            

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

       
        //Mute methods
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
            var list = dao.getMuteALL();
            return list;
        }

        
        //Tag Methods
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
            var list = dao.getTagALL();
            return list;
        }


        //Column Methods
        public void insertColumn(int NUM, String NAME, String TwitterID ,String Query)
        {
            var _entity = new Column(NUM, NAME, TwitterID,Query);
            var dto = _entity.createDTO();
            var dao = new ColumnDAO(dbConnectionString);
            dao.insertColumn(dto);
        }

        public List<ColumnDTO> getColumnList()
        {
            var dao = new ColumnDAO(dbConnectionString);
            var list = dao.getCoulumnALL();
            return list;
        }

        public void deleteColumn(int NUM)
        {
            var dao = new ColumnDAO(dbConnectionString);
            dao.deleteColumn(NUM);

        }

        public void updateColumn(ColumnDTO dto, int NUM)
        {
            var dao = new ColumnDAO(dbConnectionString);
            dao.updateColumn(dto, NUM);
        }
        
        //Private Methods
        public void insertPrivate(String TwitterID, String NGword)
        {
            var _entity = new Private(TwitterID, NGword);
            var dto = _entity.createDTO();
            var dao = new PrivateDAO(dbConnectionString);
            dao.insertPrivate(dto);
        }

        public List<PrivateDTO> getPrivateList()
        {
            var dao = new PrivateDAO(dbConnectionString);
            var list = dao.getPrivateALL();
            return list;
        }

        public void deletePrivate(String TwitterID, String NGword)
        {
            var dao = new PrivateDAO(dbConnectionString);
            dao.deletePrivate(TwitterID, NGword);
        }
    }
}
