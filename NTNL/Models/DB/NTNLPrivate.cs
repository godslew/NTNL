using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Livet;

namespace NTNL.Models.DB
{
    public class NTNLPrivate : NotificationObject
    {
        /*
         * NotificationObjectはプロパティ変更通知の仕組みを実装したオブジェクトです。
         */

        public long ID { get; set; }
        public ObservableSynchronizedCollection<string> NGList { get; private set; }

        public NTNLPrivate()
        {

        }

        public NTNLPrivate(long id)
        {
            this.ID = id;
            this.NGList = new ObservableSynchronizedCollection<string>();
        }

        public void addNGWord(long id, string word)
        {
            if (ID == id)
            {
                NGList.Add(word);
            }
        }
    }
}
