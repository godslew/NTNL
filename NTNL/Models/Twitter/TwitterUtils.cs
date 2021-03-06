﻿using System;
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
        public void updateStatus(NTNLAccount account, string text)
        {
            try
            {
                account.Token.Statuses.UpdateAsync(status => text);
            }
            catch (Exception)
            {
                
            }
        }

        /// <summary>
        /// reply
        /// </summary>
        /// <param name="account"></param>
        /// <param name="text"></param>
        /// <param name="statusID"></param>
        public void replyToStatus(NTNLAccount account, String text, long statusID)
        {
            try
            {
                account.Token.Statuses.UpdateAsync(in_reply_to_status_id => statusID, status => text);
            }
            catch (Exception)
            {

            }
        }

        /// <summary>
        /// RT
        /// </summary>
        /// <param name="account"></param>
        /// <param name="text"></param>
        /// <param name="statusID"></param>
        public void retweetStatus(NTNLAccount account, long statusID)
        {
            try
            {
                account.Token.Statuses.RetweetAsync(id => statusID);
            }
            catch (Exception)
            {
                
                
            }
            
        }

        /// <summary>
        /// follow
        /// </summary>
        /// <param name="account"></param>
        /// <param name="targetID"></param>
        public void createFollow(NTNLAccount account, long targetID)
        {

        }

        /// <summary>
        /// unfollow
        /// </summary>
        /// <param name="account"></param>
        /// <param name="targetID"></param>
        public void createUnFollow(NTNLAccount account, long targetID)
        {

        }

        /// <summary>
        /// get profile
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public NTNLAccount getUserProfile(long userID)
        {

            return null;
        }

        /// <summary>
        /// tweet delete
        /// </summary>
        /// <param name="account"></param>
        /// <param name="targetID"></param>
        public void destroyStatus(NTNLAccount account, long targetID)
        {
            account.Token.Statuses.DestroyAsync(id => targetID);
        }

        /// <summary>
        /// favorite delete
        /// </summary>
        /// <param name="account"></param>
        /// <param name="targetID"></param>
        public void destroyFavorite(NTNLAccount account, long TargetID)
        {
            account.Token.Favorites.DestroyAsync(id => TargetID);
        }

        /// <summary>
        /// RT delete
        /// </summary>
        /// <param name="account"></param>
        /// <param name="targetID"></param>
        public void destroyRetweet(NTNLAccount account, long targetID)
        {

        }


        public void createFavorite(NTNLAccount account, long TargetID)
        {
            account.Token.Favorites.CreateAsync(id => TargetID);
        }
    }
}
