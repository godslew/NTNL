﻿using System;
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
using NTNL.Models.DB;

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
        DBFacade db;
        private List<IDisposable> StreamManager { get; set; }
        public ObservableSynchronizedCollection<NTNLAccount> Accounts { get; private set; }
        public ObservableSynchronizedCollection<StatusTimeLine> StatusTimeLines { get; private set; }
        public ObservableSynchronizedCollection<NTNLPrivate> PrivateList { get; private set; }
        private IConnectableObservable<StreamingMessage> Streaming { get; set; }
        public bool hasAccounts { get; set; }
        public bool hasPrivateList { get; set; }

        #region construct
        private NTNLs()
        {
            StreamManager = new List<IDisposable>();
            tw = TwitterFacade.Instance;
            db = DBFacade.Instance;
            StatusTimeLines = new ObservableSynchronizedCollection<StatusTimeLine>();
            StatusTimeLines.Add(new StatusTimeLine("HOME","HOME"));
            StatusTimeLines.Add(new StatusTimeLine("MENTION", "MENTION"));
            Accounts = new ObservableSynchronizedCollection<NTNLAccount>();
            PrivateList = new ObservableSynchronizedCollection<NTNLPrivate>();
            installAccounts();
            installPrivate();
        }
        #endregion

        /// <summary>
        /// Account読み込み
        /// </summary>
        public void installAccounts()
        {
            
            //await Task.Run(() =>
                // {
                     var list = tw.getAccounts();
                     if (list == null) { 
                         Console.WriteLine("Accountsは登録されていません");
                         hasAccounts = false;
                     }
                     else
                     {
                         Accounts.Clear();
                         hasAccounts = true;
                         foreach (NTNLAccount ac in list)
                         {
                             Accounts.Add(ac);
                         }

                     }
               //  });
                      
        }

        public void installPrivate()
        {
            try
            {
                var list = db.getPrivateList();
                if (list == null)
                {
                    hasPrivateList = false;
                }
                else
                {
                    if (hasAccounts)
                    {
                        hasPrivateList = true;
                        PrivateList.Clear();
                        foreach (var ac in Accounts)
                        {
                            PrivateList.Add(new NTNLPrivate(ac.ID));
                        }
                        foreach (var plist in PrivateList)
                        {
                            foreach (var dto in list)
                            {
                                plist.addNGWord(Helper.helper.StringToLong(dto.TwitterID), dto.NGword);
                            }
                        }

                    }

                }
            }
            catch (Exception)
            {
                
                
            }
            
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
        public void StartStreaming(NTNLAccount account)
        {
            
            Streaming = account.Token.Streaming.StartObservableStream(
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
                                NTNL_OnStatus(this, new NTNLMessageReceivedEventArgs<StatusMessage>(p as StatusMessage), account);
                                break;
                            case MessageType.Event:
                                NTNL_OnEvent(this, new NTNLMessageReceivedEventArgs<EventMessage>(p as EventMessage), account);
                                break;
                            case MessageType.DirectMesssage:
                                //NTNL_OnDirectMessage(this, new NTNLMessageReceivedEventArgs<DirectMessageMessage>(p as DirectMessageMessage));
                                break;
                            case MessageType.DeleteStatus:
                                
                                break;
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
                        if (t.Exception != null && !t.IsCanceled) RestartStreaming(account);
                    });
                },
                (ex) =>
                {
                    //LogInformation("エラーが発生したため再接続しました : " + ex.Message);
                   // SaveLog();
                    RestartStreaming(account);
                },
                () =>
                {
                   // LogInformation("UserStreamが切断されたため再接続しました");
                   // SaveLog();
                    RestartStreaming(account);
                }
            ));
            StreamManager.Add(Streaming.Connect());
        }

        private void NTNL_OnStatus(object sender, NTNLMessageReceivedEventArgs<StatusMessage> e, NTNLAccount account)
        {

            var status = e.Message.Status;
                   
           Console.WriteLine(string.Format("{0}:{1}", status.User.ScreenName, status.Text));
           foreach (var tl in StatusTimeLines)
           {
               tl.addStatus(status, account, "HOME");
               if (status.Entities.UserMentions.Any(p => p.Id == account.user.Id) && status.RetweetedStatus == null)
               {
                   tl.addStatus(status, account, "MENTION");
               }
           }
           
        }

        private void NTNL_OnEvent(object sender, NTNLMessageReceivedEventArgs<EventMessage> e, NTNLAccount ac)
        {
            var s = e.Message;
        }

        public void RestartStreaming(NTNLAccount account)
        {
            StopStreaming();
            StartStreaming(account);
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
