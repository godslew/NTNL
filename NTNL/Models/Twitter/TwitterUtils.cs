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
        /// tweet
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
        public void retweetToStatus(Account account, String text, long statusID)
        {

        }

        /// <summary>
        /// follow
        /// </summary>
        /// <param name="account"></param>
        /// <param name="targetID"></param>
        public void createFollow(Account account, long targetID)
        {

        }

        /// <summary>
        /// unfollow
        /// </summary>
        /// <param name="account"></param>
        /// <param name="targetID"></param>
        public void createUnFollow(Account account, long targetID)
        {

        }

        /// <summary>
        /// get profile
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public Account getUserProfile(long userID)
        {

            return null;
        }

        /// <summary>
        /// tweet delete
        /// </summary>
        /// <param name="account"></param>
        /// <param name="targetID"></param>
        public void destroyStatus(Account account, long targetID)
        {

        }

        /// <summary>
        /// favorite delete
        /// </summary>
        /// <param name="account"></param>
        /// <param name="targetID"></param>
        public void destroyFavorite(Account account, long targetID)
        {

        }

        /// <summary>
        /// RT delete
        /// </summary>
        /// <param name="account"></param>
        /// <param name="targetID"></param>
        public void destroyRetweet(Account account, long targetID)
        {

        }

    }
}
