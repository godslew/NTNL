using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreTweet;

using Livet;

namespace NTNL.Models.Twitter
{
    public class Account : NotificationObject
    {
        /*
         * NotificationObjectはプロパティ変更通知の仕組みを実装したオブジェクトです。
         */
        public long ID { get; private set; }
        public Tokens token { get; private set; }

        public Account(long _ID, String CS, String CK, String AT, String AS)
        {
            this.ID = _ID;
            this.token = Tokens.Create(CS, CK, AT, AS);
        }
    }
}
