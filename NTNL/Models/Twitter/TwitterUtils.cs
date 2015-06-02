using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreTweet;

using Livet;

namespace NTNL.Models.Twitter
{
    public class TwitterUtils : NotificationObject
    {
        /*
         * NotificationObjectはプロパティ変更通知の仕組みを実装したオブジェクトです。
         */
        /// <summary>
        /// status update
        /// </summary>
        /// <param name="account"></param>
        /// <param name="text"></param>
        public void updateStatus(Account account, string text)
        {
            
        }

        /// <summary>
        /// reply
        /// </summary>
        /// <param name="account"></param>
        /// <param name="text"></param>
        /// <param name="statusID"></param>
        public void replyToStatus(Account account, String text, long statusID)
        {

        }

        /// <summary>
        /// RT
        /// </summary>
        /// <param name="account"></param>
        /// <param name="text"></param>
        /// <param name="statusID"></param>
        public void retweetStatus(Account account, String text, long statusID)
        {

        }
    }
}
