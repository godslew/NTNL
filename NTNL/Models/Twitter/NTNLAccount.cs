using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreTweet;

using Livet;

namespace NTNL.Models.Twitter
{
    public class NTNLAccount : NotificationObject
    {
        /*
         * NotificationObjectはプロパティ変更通知の仕組みを実装したオブジェクトです。
         */
        public long ID { get; private set; }
        public Tokens token { get; private set; }

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
            this.token = Tokens.Create(CS, CK, AT, AS);
        }

        /// <summary>
        /// Tokenから生成
        /// </summary>
        /// <param name="_token"></param>
        public NTNLAccount(Tokens _token)
        {
            this.ID = _token.UserId;
            this.token = _token;
        }

    }
}
