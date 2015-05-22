using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreTweet;
using NTNL.Models.DB;

namespace NTNL.Models.Twitter
{
    class TwitterFacade
    {

        private DBFacade db;
        private List<Account> accountList;
        private TwitterUtils twUtils;

        public TwitterFacade()
        {
            this.db = new DBFacade();
            this.accountList = db.getAccountList();
            this.twUtils = new TwitterUtils();

        }

        //update status for all active account 
        public Boolean UpdateStatus(List<Account> _list, String text)
        {
            foreach (Account account in _list)
            {
                twUtils.updateStatus(account, text);
            }
            return true;
        }

    }
}
