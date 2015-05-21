using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NTNL.Models.DB.DAO;
using NTNL.Models.DB.Entity;

namespace NTNL.Models.DB
{
    class DBFacade
    {
        private AccountDAO accountDAO;
        private ColumnDAO columnDAO;
        private MuteDAO muteDAO;
        private PrivateDAO privateDAO;
        private TagDAO tagDAO;

        private String dbConnectionString;

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
*/
        public SuperDAO createSuperDAO()
        {
            return new SuperDAO(dbConnectionString, "");
        }

        //Account methods
        


        

        //get account list from DB
        public List<Twitter.Account> getAccountList()
        {
            var list = new List<Twitter.Account>();
            return list;
        }
    }
}
