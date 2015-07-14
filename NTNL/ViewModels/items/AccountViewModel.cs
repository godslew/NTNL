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

namespace NTNL.ViewModels.items
{
    public class AccountViewModel : ViewModel
    {
        public NTNLAccount account { get; private set; }
        User user;

        public AccountViewModel(NTNLAccount ac)
        {
            this.account = ac;
            Token = account.Token;
            user = account.user;
            ScreenName = user.ScreenName;
            id = Token.UserId.ToString();
            ProfileImageUri = user.ProfileImageUrlHttps;
            Console.WriteLine(id.ToString());
        }
        
        public void Initialize()
        {
        }


        #region Token変更通知プロパティ
        private Tokens _Token;

        public Tokens Token
        {
            get
            { return _Token; }
            set
            { 
                if (_Token == value)
                    return;
                _Token = value;
                RaisePropertyChanged();
            }
        }
        #endregion
        
        #region ScreenName変更通知プロパティ
        private string _ScreenName;

        public string ScreenName
        {
            get
            { return _ScreenName; }
            set
            { 
                if (_ScreenName == value)
                    return;
                _ScreenName = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region id変更通知プロパティ
        private string _id;

        public string id
        {
            get
            { return _id; }
            set
            {
                if (_id == value)
                    return;
                _id = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region ProfileImageUri変更通知プロパティ
        private Uri _ProfileImageUri = null;

        public Uri ProfileImageUri
        {
            get
            { return _ProfileImageUri; }
            set
            {
                if (_ProfileImageUri == value)
                    return;
                _ProfileImageUri = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region IsSelected変更通知プロパティ
        private bool _IsSelected;

        public bool IsSelected
        {
            get
            { return _IsSelected; }
            set
            { 
                if (_IsSelected == value)
                    return;
                _IsSelected = value;
                RaisePropertyChanged();
            }
        }
        #endregion


    }
}
