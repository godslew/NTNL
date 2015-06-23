using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Livet;
using CoreTweet;
using NTNL.Models.Analyzer;

namespace NTNL.Models.Twitter
{
    public class StatusTimeLine : NotificationObject
    {
        /*
         * NotificationObjectはプロパティ変更通知の仕組みを実装したオブジェクトです。
         */


        public ObservableSynchronizedCollection<Status> Statuses { get; private set; }
        public bool HasQuery { get; private set; }
        public string Name {get; set; }
        public ColumnQuery Query { get; set; }

        /// <summary>
        /// 名前のみのカラム
        /// </summary>
        /// <param name="_name"></param>
        public StatusTimeLine(string _name)
        {
            this.Name = _name;
            Statuses = new ObservableSynchronizedCollection<Status>();
        }

        /// <summary>
        /// 名前とQueryなカラム
        /// </summary>
        /// <param name="_query"></param>
        /// <param name="_name"></param>
        public StatusTimeLine(string _query, string _name)
        {
            this.Name = _name;
            Statuses = new ObservableSynchronizedCollection<Status>();
            this.HasQuery = true;
            this.Query = new ColumnQuery(_query);
        }

        public void addStatus(Status _status)
        {
            /*
             * collectionにstatusを格納
             * 失敗した時のstatus追加漏れをどうするか？？
             * */
            try
            {
                //queryが定義されていたら
                if (this.HasQuery)
                {

                }
                else
                {

                }

            }
            catch (Exception)
            {

                Console.WriteLine("add status failed");
                /*
                 * ここにlogを保存する機構を搭載
                 * NTNLsにsavelogのメソッドを使う
                 */
            }
        }


    }
}
