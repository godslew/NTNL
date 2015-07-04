using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Livet;

using CoreTweet;
using CoreTweet.Core;
using CoreTweet.Rest;
using CoreTweet.Streaming;
using CoreTweet.Streaming.Reactive;

using NTNL.Models.Twitter;
using System.Threading.Tasks;
using System.Reactive.Subjects;
using System.Reactive.Linq;

namespace NTNL.Models
{
    public sealed partial class NTNLs : NotificationObject
    {
        /*
         * NotificationObjectはプロパティ変更通知の仕組みを実装したオブジェクトです。
         */

        public Tokens Token { get; set; }
        private OAuth.OAuthSession OAuthSession { get; set; }
        TwitterFacade tw;
        private List<IDisposable> StreamManager { get; set; }
        public ObservableSynchronizedCollection<NTNLAccount> Accounts { get; private set; }
        public ObservableSynchronizedCollection<StatusTimeLine> StatusTimeLines { get; private set; }
        private IConnectableObservable<StreamingMessage> Streaming { get; set; }

        #region construct
        private NTNLs()
        {
            StreamManager = new List<IDisposable>();
            tw = TwitterFacade.Instance;
            StatusTimeLines = new ObservableSynchronizedCollection<StatusTimeLine>();
            StatusTimeLines.Add(new StatusTimeLine("HOME"));
            Accounts = new ObservableSynchronizedCollection<NTNLAccount>();
            installAccounts();
        }
        #endregion

        /// <summary>
        /// Account読み込み
        /// </summary>
        private  void installAccounts()
        {
            
            //await Task.Run(() =>
                // {
                     var list = tw.getAccounts();
                     if (list == null) { 
                         this.Accounts = null;
                         Console.WriteLine("Accountsはnullです");
                     }
                     else
                     {

                         foreach (NTNLAccount ac in list)
                         {
                             
                             this.Accounts.Add(ac);
                         }

                     }
               //  });
                      
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

        #region Streaming接続
        public void StartStreaming(Tokens token)
        {
            
            Streaming = token.Streaming.StartObservableStream(
                StreamingType.User,
                new StreamingParameters(include_entities => "true", include_followings_activity => "true"))
                .Publish();

            StreamManager.Add(Streaming.Subscribe(
                (p) =>
                {
                    Task.Factory.StartNew(() =>
                    {
                        switch (p.Type)
                        {
              
                            case MessageType.Create:
                                NTNL_OnStatus(this, new NTNLMessageReceivedEventArgs<StatusMessage>(p as StatusMessage),token);
                                break;
                            case MessageType.Event:
                                //NTNL_OnEvent(this, new NTNLMessageReceivedEventArgs<EventMessage>(p as EventMessage));
                                break;
                            case MessageType.DirectMesssage:
                                //NTNL_OnDirectMessage(this, new NTNLMessageReceivedEventArgs<DirectMessageMessage>(p as DirectMessageMessage));
                                break;
                            case MessageType.DeleteStatus:
                            case MessageType.DeleteDirectMessage:
                                //NTNL_OnId(this, new NTNLMessageReceivedEventArgs<DeleteMessage>(p as DeleteMessage));
                                break;
                            case MessageType.Disconnect:
                                //LogInformation("Disconnected");
                                //SaveLog();
                                break;
                            default:
                                break;

                        }
                    }, TaskCreationOptions.LongRunning)
                    .ContinueWith(t =>
                    {
                        if (t.Exception != null && !t.IsCanceled) RestartStreaming(token);
                    });
                },
                (ex) =>
                {
                    //LogInformation("エラーが発生したため再接続しました : " + ex.Message);
                   // SaveLog();
                    RestartStreaming(token);
                },
                () =>
                {
                   // LogInformation("UserStreamが切断されたため再接続しました");
                   // SaveLog();
                    RestartStreaming(token);
                }
            ));
            StreamManager.Add(Streaming.Connect());
        }

        private void NTNL_OnStatus(object sender, NTNLMessageReceivedEventArgs<StatusMessage> e, Tokens token)
        {

            var status = e.Message.Status;
                   
           Console.WriteLine(string.Format("{0}:{1}", status.User.ScreenName, status.Text));
           foreach (var tl in StatusTimeLines)
           {
               tl.addStatus(status, token);
           }
           
        }

        public void RestartStreaming(Tokens token)
        {
            StopStreaming();
            StartStreaming(token);
        }

        public void StopStreaming()
        {
            foreach (var i in StreamManager) i.Dispose();
            StreamManager.Clear();
        }
        #endregion


    }
    public class NTNLMessageReceivedEventArgs<T> : EventArgs
    {
        public T Message { get; set; }

        public NTNLMessageReceivedEventArgs(T obj)
        {
            Message = obj;
        }
    }


}
