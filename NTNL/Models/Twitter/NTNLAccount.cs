using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreTweet;

using Livet;
using System.Threading.Tasks;

namespace NTNL.Models.Twitter
{
    public class NTNLAccount : NotificationObject
    {
        /*
         * NotificationObjectはプロパティ変更通知の仕組みを実装したオブジェクトです。
         */
        public User user;
        /// <summary>
        /// DBから読み込んだ時に使う
        /// </summary>
        /// <param name="_ID"></param>
        /// <param name="CS"></param>
        /// <param name="CK"></param>
        /// <param name="AT"></param>
        /// <param name="AS"></param>
        public NTNLAccount(long _ID, String CS, String CK, String AT, String AS)
        {
            this.ID = _ID;
            this.Token = Tokens.Create(CS, CK, AT, AS);
            user = Token.Account.VerifyCredentials();
        }

        /// <summary>
        /// Tokenから生成
        /// </summary>
        /// <param name="_token"></param>
        public NTNLAccount(Tokens _token)
        {
            this.ID = _token.UserId;
            this.Token = _token;
            user = Token.Account.VerifyCredentials();
        }

        #region Token変更通知プロパティ
        private Tokens _Token;

        public Tokens Token
        {
            get
            { return _Token; }
            set
            {
                if (_Token == value)
                    return;
                _Token = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region UserId変更通知プロパティ
        private long _ID;

        public long ID
        {
            get
            { return _ID; }
            set
            {
                if (_ID == value)
                    return;
                _ID = value;
                RaisePropertyChanged();
            }
        }
        #endregion


    }
}
