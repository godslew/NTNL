using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTNL.Config
{
    /// <summary>
    /// queryの定義
    /// </summary>
    class NTNLQueryTemplate
    {
        public const string HomeQuery = "%ID%.HOME";
        public const string MentionQuery = "%ID%.MENTION";
        public const string ActivityQuery = "%ID%,FAVORITE.FOLLOW";

    }
}
