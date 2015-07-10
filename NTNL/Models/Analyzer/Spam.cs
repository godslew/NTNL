using CoreTweet;
using NTNL.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTNL.Models.Analyzer
{
    class Spam
    {
        public static String SpamTextCreate(User user)
        {
            int SpamPoint = 0;
            if (user.StatusesCount < SpamConfig.StatusesSpamCount)
            {
                SpamPoint += SpamConfig.StatusesSpamCountPoint;
            }
            if ((double)(user.FollowersCount / user.FriendsCount) < SpamConfig.FFSpamPercent)
            {
                SpamPoint += SpamConfig.FFSpamPercentPoint;
            }
            if (SpamPoint > 60)
            {
                return "このユーザはスパムの可能性があります";
            }
            return "このユーザはスパムではありません";


        }
    }
}
