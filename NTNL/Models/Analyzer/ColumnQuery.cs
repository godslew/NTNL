using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Livet;

namespace NTNL.Models.Analyzer
{
    public class ColumnQuery : NotificationObject
    {
        /*
         * NotificationObjectはプロパティ変更通知の仕組みを実装したオブジェクトです。
         */

        public string query { get; private set; }

        public ColumnQuery(string _query)
        {
            this.query = _query;
        }

    }
}
