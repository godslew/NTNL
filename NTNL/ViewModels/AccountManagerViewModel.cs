using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

using Livet;
using Livet.Commands;
using Livet.Messaging;
using Livet.Messaging.IO;
using Livet.EventListeners;
using Livet.Messaging.Windows;

using NTNL.Models;
using NTNL.Models.Twitter;
using CoreTweet;
using System.Threading.Tasks;
using NTNL.Models.DB;

namespace NTNL.ViewModels
{
    public class AccountManagerViewModel : ViewModel
    {
        
        private TwitterFacade tw;
        private CoreTweet.OAuth.OAuthSession session;
        NTNLs ntnls;

        public void Initialize()
        {
            tw = TwitterFacade.Instance;
            this.session = tw.OAuthStart();
            ntnls = NTNLs.Instance;
            
        }

        public AccountManagerViewModel()
        {
            
            
        }


        #region OAuthCommand
        private ListenerCommand<string> _OAuthCommand;

        public ListenerCommand<string> OAuthCommand
        {
            get
            {
                if (_OAuthCommand == null)
                {
                    _OAuthCommand = new ListenerCommand<string>(OAuth);
                }
                return _OAuthCommand;
            }
        }
        #region OAuth認証
        public async void OAuth(string parameter)
        {
            Console.WriteLine(parameter);
            if (parameter == null)
            {
                
            }
            else
            {
                await Task.Run(() =>
                {
                    Tokens tokens = this.session.GetTokens(parameter);
                    DBFacade db = DBFacade.Instance;
                    try
                    {
                        db.insertAccount(tokens.UserId.ToString(), tokens.ConsumerKey, tokens.ConsumerSecret, tokens.AccessToken, tokens.AccessTokenSecret);
                        var list = db.getAccountList();
                        foreach(var account in list){
                            Console.WriteLine(account.TwitterID);
                        }
                        ntnls.installAccounts();
                        ntnls.StartStreaming(new NTNLAccount(tokens));
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("error");
                    }
                    
                });
            }
        }
        #endregion
        #endregion

    }
}
