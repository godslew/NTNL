using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreTweet;
using NTNL.Models.DB;
using NTNL.Models.DB.DAO;
using NTNL.NTNL_Config;
using NTNL.Models.DB.DTO;
using NTNL.Helper;

namespace NTNL.Models.Twitter
{
    class TwitterFacade
    {

        private DBFacade db;
        private TwitterUtils twUtils;

        public TwitterFacade()
        {
            this.db = new DBFacade();
            //this.accountList = db.getAccountList();
            this.twUtils = new TwitterUtils();

        }

        #region singleton
        static TwitterFacade _instance;

        /// <summary>
        /// TwitterFacadeの唯一のインスタンスを取得します。
        /// </summary>
        public static TwitterFacade Instance
        {
            get
            {
                if (_instance == null) _instance = new TwitterFacade();
                return _instance;
            }
        }
        #endregion

        /// <summary>
        /// update status for all active account
        /// </summary>
        /// <param name="_list"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public Boolean UpdateStatus(List<NTNLAccount> _list, String text)
        {
            foreach (NTNLAccount account in _list)
            {
                UpdateStatus(account, text);
            }
            return true;
        }

        public List<NTNLAccount> getAccounts()
        {
            var list = new List<NTNLAccount>();
            try
            {
                var dtoList = db.getAccountList();
                foreach (AccountDTO dto in dtoList)
                {
                    list.Add(new NTNLAccount(helper.StringToLong(dto.TwitterID), dto.CK, dto.CS, dto.AT, dto.ATS));
                }

                return list;
            }
            catch (Exception)
            {
                return null;
            }
            
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

        public void UpdateStatus(NTNLAccount account, string text)
        {
            twUtils.updateStatus(account, text);
        }
    }
}
