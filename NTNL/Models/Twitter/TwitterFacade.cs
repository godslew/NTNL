using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreTweet;
using NTNL.Models.DB;
using NTNL.Models.DB.DAO;
using NTNL.NTNL_Config;

namespace NTNL.Models.Twitter
{
    class TwitterFacade
    {

        private DBFacade db;
        private List<Account> accountList;
        private TwitterUtils twUtils;

        public TwitterFacade()
        {
            //this.db = new DBFacade();
            //this.accountList = db.getAccountList();
            this.twUtils = new TwitterUtils();

        }

        /// <summary>
        /// update status for all active account
        /// </summary>
        /// <param name="_list"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public Boolean UpdateStatus(List<Account> _list, String text)
        {
            foreach (Account account in _list)
            {
                twUtils.updateStatus(account, text);
            }
            return true;
        }

        /// <summary>
        /// 独自でないキーを使って認証を開始する
        /// </summary>
        /// <returns></returns>
        public  CoreTweet.OAuth.OAuthSession OAuthStart()
        {
           
            var session = OAuth.Authorize(TwitterConfig.CK, TwitterConfig.CS);
            System.Diagnostics.Process.Start(session.AuthorizeUri.ToString());
            return session;
        }
    }
}
