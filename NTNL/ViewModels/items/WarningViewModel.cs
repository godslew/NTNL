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

namespace NTNL.ViewModels.items
{
    public class WarningViewModel : ViewModel
    {
        public MainWindowViewModel main { get; set; }
        public string wordlist { get; set; }
        public void Initialize()
        {
        }

        public WarningViewModel(List<string> clist, MainWindowViewModel mw)
        {
            main = mw;
            foreach (string word in clist)
            {
                wordlist += word+" ";
            }
            WarningText += "アカウント@"+main.selectedAccount.ScreenName + "でのTweet内容に\nNG Word <" + wordlist + ">が含まれています。\nこのままTweetしますか？";

        }

        public WarningViewModel()
        {

        }

        #region WarningText変更通知プロパティ
        private string _WarningText;

        public string WarningText
        {
            get
            { return _WarningText; }
            set
            { 
                if (_WarningText == value)
                    return;
                _WarningText = value;
                RaisePropertyChanged();
            }
        }
        #endregion


        #region TweetCommand
        private ViewModelCommand _TweetCommand;

        public ViewModelCommand TweetCommand
        {
            get
            {
                if (_TweetCommand == null)
                {
                    _TweetCommand = new ViewModelCommand(Tweet);
                }
                return _TweetCommand;
            }
        }

        public void Tweet()
        {
            main.sendTweet(main.selectedAccount.account, main.Text);
        }
        #endregion


        #region CanselCommand
        private ViewModelCommand _CanselCommand;

        public ViewModelCommand CanselCommand
        {
            get
            {
                if (_CanselCommand == null)
                {
                    _CanselCommand = new ViewModelCommand(Cansel);
                }
                return _CanselCommand;
            }
        }

        public void Cansel()
        {
            main.Text = "";
            main.isReply = false;
        }
        #endregion



    }
}
