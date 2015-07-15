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
using System.Windows.Controls;
using CoreTweet;
using NTNL.ViewModels.items;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Windows;
using NTNL.Models.Twitter;
using NTNL.Models.Analyzer;

namespace NTNL.ViewModels
{
    public class StatusViewModel : ViewModel
    {
        
        public Status SourceStatus { get; private set; }

        public Status ReceivedStatus { get; private set; }

        public NTNLs ntnls;
        public MainWindowViewModel main;
        public PropertyChangedEventListener listener;
        public CommonViewModel common { get; set; }

        public void Initialize()
        {
        }

        public StatusViewModel()
        {

        }

        public StatusViewModel(MainWindowViewModel mw, Status _status){
            this.SourceStatus = _status;
            ReceivedStatus = SourceStatus;
            IsFavorite = Visibility.Hidden;
            if (SourceStatus.RetweetedStatus != null)
            {
                SourceStatus = SourceStatus.RetweetedStatus;
                IsRetweet = Visibility.Visible;
                RetweetingUser = new UserViewModel(ReceivedStatus.User, main);
                RetweetedBytext = RetweetingUser.Name;
            }
            this.main = mw;
            
            this.text = SourceStatus.Text;
            this.id = SourceStatus.Id;
            this.User = new UserViewModel(SourceStatus.User, main);
            ExtractVia();

            DispatcherHelper.UIDispatcher.BeginInvoke((Action)(() =>
            {
                if (SourceStatus.Entities != null)
                {
                    Medias = new ObservableSynchronizedCollection<StatusMediaViewModel>();

                    if (SourceStatus.Entities.Media != null)
                    {
                        foreach (var i in SourceStatus.Entities.Media)
                        {
                            Medias.Add(new StatusMediaViewModel { Uri = i.MediaUrlHttps });
                        }
                    }

                    HasMedia = Medias.Count != 0;

                }
            }));
        }

        public StatusViewModel(string _text)
        {
            this.text = _text;
        }

        #region id変更通知プロパティ
        private long _id;

        public long id
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

        #region text変更通知プロパティ
        private string _text;

        public string text
        {
            get
            { return _text; }
            set
            {
                if (_text == value)
                    return;
                _text = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region RetweetedBytext変更通知プロパティ
        private string _RetweetedBytext;

        public string RetweetedBytext
        {
            get
            { return _RetweetedBytext; }
            set
            {
                if (_RetweetedBytext == value)
                    return;
                _RetweetedBytext = value + " retweeted";
                RaisePropertyChanged();
            }
        }
        #endregion

        #region User変更通知プロパティ
        private UserViewModel _User;

        public UserViewModel User
        {
            get
            { return _User; }
            set
            {
                if (_User == value)
                    return;
                _User = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region IsRetweet変更通知プロパティ
        private Visibility _IsRetweet = Visibility.Hidden;

        public Visibility IsRetweet
        {
            get
            { return _IsRetweet; }
            set
            {
                if (_IsRetweet == value)
                    return;
                _IsRetweet = value;
                RaisePropertyChanged();
            }
        }
        #endregion


        #region IsFavorite変更通知プロパティ
        private Visibility _IsFavorite = Visibility.Hidden;

        public Visibility IsFavorite
        {
            get
            { return _IsFavorite; }
            set
            { 
                if (_IsFavorite == value)
                    return;
                _IsFavorite = value;
                RaisePropertyChanged();
            }
        }
        #endregion


        #region RetweetingUser変更通知プロパティ
        private UserViewModel _RetweetingUser;

        public UserViewModel RetweetingUser
        {
            get
            { return _RetweetingUser; }
            set
            {
                if (_RetweetingUser == value)
                    return;
                _RetweetingUser = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region Via変更通知プロパティ
        private string _Via = "";

        public string Via
        {
            get
            { return _Via; }
            set
            {
                if (_Via == value)
                    return;
                _Via = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region Via抽出
        static Regex reg = new Regex("<a href=\"(?<url>.+)\" rel=\"nofollow\">(?<client>.+)</a>");
        
        public void ExtractVia()
        {
            var m = reg.Match(SourceStatus.Source);
            if (!m.Success) return;

            Via = "via " + m.Groups["client"].Value;
            
        }
        #endregion

        #region HasMedia変更通知プロパティ
        private bool _HasMedia;

        public bool HasMedia
        {
            get
            { return _HasMedia; }
            set
            {
                if (_HasMedia == value)
                    return;
                _HasMedia = value;
                if (value) {
                    HasMediaVisibility = Visibility.Visible;
                }
                RaisePropertyChanged();
            }
        }
        #endregion

        #region HasMediaVisibility変更通知プロパティ
        private Visibility _HasMediaVisibility = Visibility.Hidden;

        public Visibility HasMediaVisibility
        {
            get
            { return _HasMediaVisibility; }
            set
            {
                if (_HasMediaVisibility == value)
                    return;
                _HasMediaVisibility = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region Medias変更通知プロパティ
        private ObservableSynchronizedCollection<StatusMediaViewModel> _Medias;

        public ObservableSynchronizedCollection<StatusMediaViewModel> Medias
        {
            get
            { return _Medias; }
            set
            {
                if (_Medias == value)
                    return;
                _Medias = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region ReplyCommand
        private ViewModelCommand _ReplyCommand;

        public ViewModelCommand ReplyCommand
        {
            get
            {
                if (_ReplyCommand == null)
                {
                    _ReplyCommand = new ViewModelCommand(Reply);
                }
                return _ReplyCommand;
            }
        }

        public void Reply()
        {
            main.SetReplyTo(this);
        }
        #endregion

        #region RetweetCommand
        private ViewModelCommand _RetweetCommand;

        public ViewModelCommand RetweetCommand
        {
            get
            {
                if (_RetweetCommand == null)
                {
                    _RetweetCommand = new ViewModelCommand(Retweet);
                }
                return _RetweetCommand;
            }
        }

        public void Retweet()
        {
            if (main.selectedAccount != null)
            {
                if (Spam.SpamTweetAnalyzer(SourceStatus))
                {
                    main.SpamRetweetWarning(this);
                }
                else
                {
                    TwitterFacade.Instance.RetweetStatus(main.selectedAccount.account, id);
                }
            }
        }
        #endregion

        #region OpenUserCommand
        private ViewModelCommand _OpenUserCommand;

        public ViewModelCommand OpenUserCommand
        {
            get
            {
                if (_OpenUserCommand == null)
                {
                    _OpenUserCommand = new ViewModelCommand(OpenUser);
                }
                return _OpenUserCommand;
            }
        }

        public  void OpenUser()
        {
            main.OpenUser(this.User);
        }
        #endregion


        #region deleteStatusCommand
        private ViewModelCommand _deleteStatusCommand;

        public ViewModelCommand deleteStatusCommand
        {
            get
            {
                if (_deleteStatusCommand == null)
                {
                    _deleteStatusCommand = new ViewModelCommand(deleteStatus);
                }
                return _deleteStatusCommand;
            }
        }

        public void deleteStatus()
        {
            try
            {
                TwitterFacade.Instance.DeleteStatus(main.selectedAccount.account, id);
               
            }
            catch (Exception)
            {
                
                
            }
        }
        #endregion


        #region RetweetOKCommand
        private ViewModelCommand _RetweetOKCommand;

        public ViewModelCommand RetweetOKCommand
        {
            get
            {
                if (_RetweetOKCommand == null)
                {
                    _RetweetOKCommand = new ViewModelCommand(RetweetOK);
                }
                return _RetweetOKCommand;
            }
        }

        public void RetweetOK()
        {
            TwitterFacade.Instance.RetweetStatus(main.selectedAccount.account, id);
        }
        #endregion


        #region FavoriteCommand
        private ViewModelCommand _FavoriteCommand;

        public ViewModelCommand FavoriteCommand
        {
            get
            {
                if (_FavoriteCommand == null)
                {
                    _FavoriteCommand = new ViewModelCommand(Favorite);
                }
                return _FavoriteCommand;
            }
        }

        public void Favorite()
        {
            if (main.selectedAccount != null)
            {
                try
                {
                    TwitterFacade.Instance.CreateFavorite(main.selectedAccount.account, id);
                    IsFavorite = Visibility.Visible;
                }
                catch (Exception)
                {
                    Console.WriteLine("Permission Error");

                }
            }
        }
        #endregion


        #region UnFavoriteCommand
        private ViewModelCommand _UnFavoriteCommand;

        public ViewModelCommand UnFavoriteCommand
        {
            get
            {
                if (_UnFavoriteCommand == null)
                {
                    _UnFavoriteCommand = new ViewModelCommand(UnFavorite);
                }
                return _UnFavoriteCommand;
            }
        }

        public void UnFavorite()
        {
            if (main.selectedAccount != null)
            {
                
                try
                {
                     TwitterFacade.Instance.DestroyFavorite(main.selectedAccount.account, id);
                     IsFavorite = Visibility.Hidden;
                 }
                 catch (Exception)
                 {

                     Console.WriteLine("Permission Error");
                 }

             }
        }
        #endregion




    }
}
