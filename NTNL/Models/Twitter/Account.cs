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
    }
}
