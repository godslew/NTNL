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
using CoreTweet;
using NTNL.Models.Analyzer;

namespace NTNL.ViewModels.items
{
    public class UserViewModel : ViewModel
    {

        public void Initialize()
        {
        }

        private User user;
        private MainWindowViewModel main;

        public UserViewModel(User user, MainWindowViewModel main)
        {

            this.user = user;
            this.main = main;
            this.Name = user.Name;
            this.ScreenName = user.ScreenName;
            this.ProfileImageUri = user.ProfileImageUrlHttps;
            this.SpamBanner = Spam.SpamTextCreate(user);
            this.ProfileBgImageUri = user.ProfileBannerUrl;
            this.Bio = user.Description;
            
        }

        public UserViewModel()
        {

        }

        public UserViewModel(User user)
        {
            this.user = user;
            this.Name = user.Name;
            this.ScreenName = user.ScreenName;
            this.ProfileImageUri = user.ProfileImageUrlHttps;
            this.ProfileBgImageUri = user.ProfileBannerUrl;
            this.SpamBanner = Spam.SpamTextCreate(user);
            this.Bio = user.Description;
        }

        #region Name変更通知プロパティ
        private string _Name = "Name";

        public string Name
        {
            get
            { return _Name; }
            set
            {
                if (_Name == value)
                    return;
                _Name = value;
                RaisePropertyChanged();
            }
        }
        #endregion


        #region ScreenName変更通知プロパティ
        private string _ScreenName = "ScreenName";

        public string ScreenName
        {
            get
            { return _ScreenName; }
            set
            {
                if (_ScreenName == value)
                    return;
                _ScreenName = "@" + value;
                RaisePropertyChanged();
            }
        }
        #endregion


        #region IdString変更通知プロパティ
        private string _IdString = "";

        public string IdString
        {
            get
            { return _IdString; }
            set
            {
                if (_IdString == value)
                    return;
                _IdString = value;
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


        #region ProfileBgImageUri変更通知プロパティ
        private Uri _ProfileBgImageUri;

        public Uri ProfileBgImageUri
        {
            get
            { return _ProfileBgImageUri; }
            set
            { 
                if (_ProfileBgImageUri == value)
                    return;
                _ProfileBgImageUri = value;
                RaisePropertyChanged();
            }
        }
        #endregion


        #region SpamBanner変更通知プロパティ
        private string _SpamBanner;

        public string SpamBanner
        {
            get
            { return _SpamBanner; }
            set
            { 
                if (_SpamBanner == value)
                    return;
                _SpamBanner = value;
                RaisePropertyChanged();
            }
        }
        #endregion


        #region Bio変更通知プロパティ
        private string _Bio;

        public string Bio
        {
            get
            { return _Bio; }
            set
            { 
                if (_Bio == value)
                    return;
                _Bio = value;
                RaisePropertyChanged();
            }
        }
        #endregion


    }
}
