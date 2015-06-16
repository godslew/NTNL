using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Livet;

namespace NTNL.Models
{
    public sealed class NTNLs : NotificationObject
    {
        /*
         * NotificationObjectはプロパティ変更通知の仕組みを実装したオブジェクトです。
         */

        private NTNLs()
        {

        }

        #region singleton
        static NTNLs _instance;

        /// <summary>
        /// NTNLの唯一のインスタンスを取得します。
        /// </summary>
        public static NTNLs Instance
        {
            get
            {
                if (_instance == null) _instance = new NTNLs();
                return _instance;
            }
        }
        #endregion
    }
}
