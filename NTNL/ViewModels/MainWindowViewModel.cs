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
using System.Collections.ObjectModel;
using System.Windows.Data;
using NTNL.Models.Twitter;
using System.Threading.Tasks;
using NTNL.ViewModels.items;
using System.Windows.Controls;
using NTNL.Models.Analyzer;

namespace NTNL.ViewModels
{
    public class MainWindowViewModel : ViewModel
    {
        

        PropertyChangedEventListener listener;
        public MainWindowViewViewModel View { get; private set; }
        public SettingWindowViewModel setView { get; private set; }
        NTNLs ntnls;
        public CommonViewModel common { get; private set; }


        public void Initialize()
        {
            ntnls = NTNLs.Instance;
            View.StatusTimeline = ViewModelHelper.CreateReadOnlyDispatcherCollection(
                ntnls.StatusTimeLines,
                p => new StatusTimeLineViewModel(this, p),
                DispatcherHelper.UIDispatcher);
            Accounts = ViewModelHelper.CreateReadOnlyDispatcherCollection(
                ntnls.Accounts,
                p => new AccountViewModel(p),
                DispatcherHelper.UIDispatcher);
            
            setView.PrivateLists = ViewModelHelper.CreateReadOnlyDispatcherCollection(
                ntnls.PrivateList,
                p => new PrivateListsViewModel(this, p),
                DispatcherHelper.UIDispatcher);

            setView.Accounts = this.Accounts;

            listener = new PropertyChangedEventListener(ntnls);
            CompositeDisposable.Add(listener);
            //SelectedAccount = new List<AccountViewModel>();
            if (ntnls.hasAccounts)
            {
                StartStream();
            }
        }

        public MainWindowViewModel()
        {
            View = new MainWindowViewViewModel();
            setView = new SettingWindowViewModel(this);
            //common = CommonViewModel.Instance;
          
        }


        private void StartStream()
        {

            var list = ntnls.Accounts;
            if (list != null)
            {
                var ac = list.First();
                Console.WriteLine(ac.Token.UserId+""+ac.Token.AccessToken);
                ntnls.StartStreaming(ac);
            }
        }

        #region AccountManagerCommand
        private ViewModelCommand _AccountManagerCommand;

        public ViewModelCommand AccountManagerCommand
        {
            get
            {
                if (_AccountManagerCommand == null)
                {
                    _AccountManagerCommand = new ViewModelCommand(AccountManager);
                }
                return _AccountManagerCommand;
            }
        }
        public void AccountManager()
        {
            
            //messageを使ってみた,非常につよい
            var message =  new TransitionMessage(typeof(Views.AccountManagerWindow), new AccountManagerViewModel(), TransitionMode.NewOrActive);
            Messenger.Raise(message);
            
        }
        #endregion

        #region OpenSettingCommand
        private ViewModelCommand _OpenSettingCommand;

        public ViewModelCommand OpenSettingCommand
        {
            get
            {
                if (_OpenSettingCommand == null)
                {
                    _OpenSettingCommand = new ViewModelCommand(OpenSetting);
                }
                return _OpenSettingCommand;
            }
        }

        public void OpenSetting()
        {

            //messageを使ってみた,非常につよい
            var message = new TransitionMessage(typeof(Views.SettingWindow), this.setView, TransitionMode.Modal);
            Messenger.Raise(message);

        }
        #endregion


        #region addColumnCommand
        private ViewModelCommand _addColumnCommand;

        public ViewModelCommand addColumnCommand
        {
            get
            {
                if (_addColumnCommand == null)
                {
                    _addColumnCommand = new ViewModelCommand(addColumn, CanaddColumn);
                }
                return _addColumnCommand;
            }
        }

        public bool CanaddColumn()
        {
            return true;
        }

        public void addColumn()
        {
            ntnls.StatusTimeLines.Add(new StatusTimeLine("test"));
            //Console.WriteLine("test" + columnList.Count);
        }
        #endregion


        #region Accounts変更通知プロパティ
        private ReadOnlyDispatcherCollection<AccountViewModel> _Accounts;

        public ReadOnlyDispatcherCollection<AccountViewModel> Accounts
        {
            get
            { return _Accounts; }
            set
            {
                if (_Accounts == value)
                    return;
                _Accounts = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        
        #region SelectedAccount変更通知プロパティ
        private List<AccountViewModel> _SelectedAccount;

        public List<AccountViewModel> SelectedAccount
        {
            get
            { return _SelectedAccount; }
            set
            {
                if (_SelectedAccount == value)
                    return;
                _SelectedAccount = value;

                foreach (var item in value)
                {
                    Console.WriteLine(item.ScreenName);
                }
                RaisePropertyChanged();
            }
        }
        #endregion


        #region selectedAccount変更通知プロパティ
        private AccountViewModel _selectedAccount;

        public AccountViewModel selectedAccount
        {
            get
            { return _selectedAccount; }
            set
            {
                if (_selectedAccount == value)
                    return;
                _selectedAccount = value;
                foreach (var plist in setView.PrivateLists)
                {
                    if (plist.ID == value.account.ID)
                    {
                        setView.Privates = plist.Privates;
                        Console.WriteLine(plist.ID);
                    }
                }
                Console.WriteLine(value.ScreenName+""+value.IsSelected.ToString());
                RaisePropertyChanged();
            }
        }
        #endregion

        #region Text変更通知プロパティ
        private string _Text;

        public string Text
        {
            get
            { return _Text; }
            set
            {
                if (_Text == value)
                    return;
                _Text = value;
                if (_Text != "")
                {
                    isWrite = true;

                }else if(_Text == ""){
                    isReply = false;
                    isWrite = false;
                }
                    
                RaisePropertyChanged();
            }
        }
        #endregion


        #region isWrite変更通知プロパティ
        private bool _isWrite;

        public bool isWrite
        {
            get
            { return _isWrite; }
            set
            {
                if (_isWrite == value)
                    return;
                _isWrite = value;
                RaisePropertyChanged();
            }
        }
        #endregion


        #region isExpand変更通知プロパティ
        private bool _isExpand;

        public bool isExpand
        {
            get
            { return _isExpand; }
            set
            {
                if (_isExpand == value)
                    return;
                _isExpand = value;
                RaisePropertyChanged();
            }
        }
        #endregion


        #region isReply変更通知プロパティ
        private bool _isReply;

        public bool isReply
        {
            get
            { return _isReply; }
            set
            {
                if (_isReply == value)
                    return;
                _isReply = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region ReplyingStatus変更通知プロパティ
        private StatusViewModel _ReplyingStatus;

        public StatusViewModel ReplyingStatus
        {
            get
            { return _ReplyingStatus; }
            set
            {
                if (_ReplyingStatus == value)
                    return;
                _ReplyingStatus = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region statusID変更通知プロパティ
        private long _statusID;

        public long statusID
        {
            get
            { return _statusID; }
            set
            {
                if (_statusID == value)
                    return;
                _statusID = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region TweetCommand
        private ListenerCommand<string> _TweetCommand;

        public ListenerCommand<string> TweetCommand
        {
            get
            {
                if (_TweetCommand == null)
                {
                    _TweetCommand = new ListenerCommand<string>(Tweet);
                }
                return _TweetCommand;
            }
        }

        public void Tweet(string parameter)
        {
            var analyze = new PrivateAnalyzer();
            Console.WriteLine(parameter);
            if(ntnls.hasPrivateList){
            foreach(var list in ntnls.PrivateList){
                if (list.ID == selectedAccount.account.ID)
                    {
                        analyze.setWordList(list.NGList.ToList());
                        analyze.setText(parameter);
                        break;
                     }
                }
            }
            if (analyze.hasWordlist)
            {
                var clist = analyze.getContainNGwordList();
                if (analyze.hasNGword)
                {
                    if (clist != null)
                    {
                        var Warning = new WarningViewModel(clist, this);
                        var message = new TransitionMessage(typeof(Views.items.Warning), Warning, TransitionMode.Modal);
                        Messenger.Raise(message);
                    }
                    else
                    {
                        sendTweet(selectedAccount.account, parameter);
                    }
                }
                else if (!analyze.hasNGword)
                {
                    sendTweet(selectedAccount.account, parameter);
                }
            }
            else
            {
                sendTweet(selectedAccount.account, parameter);
            }
        }
        #endregion

        #region sendTweet
        public void sendTweet(NTNLAccount ac, string text)
        {
            if (selectedAccount != null)
            {
                if (!isReply)
                {
                    TwitterFacade.Instance.UpdateStatus(selectedAccount.account, text);
                    Text = "";
                }
                else if (isReply)
                {
                    TwitterFacade.Instance.ReplyToStatus(selectedAccount.account, text, ReplyingStatus.id);
                    isReply = false;
                    Text = "";
                }
            }
        }
        #endregion

        #region setReply
        public void SetReplyTo(StatusViewModel st)
        {
            ReplyingStatus = st;
            isReply = true;
            isExpand = true;

            var s = st.SourceStatus;
            List<string> ru = s.Entities.UserMentions.Select(p => p.ScreenName).ToList();
            if (!ru.Contains(s.User.ScreenName)) ru.Insert(0, s.User.ScreenName);
            if (ru.Count != 1 && ru.Contains(selectedAccount.ScreenName)) ru.Remove(selectedAccount.ScreenName);
            var t = new StringBuilder();
            ru.ForEach(p => t.Append(String.Format("@{0} ", p)));
            Text = t.ToString();
        }
        #endregion

        #region OpenUser
        public async void OpenUser(UserViewModel user)
        {
            await Task.Run(() =>
            {
                var message = new TransitionMessage(typeof(Views.items.UserWindow), user, TransitionMode.Modal);
                Messenger.Raise(message);
            });
        }
        #endregion
    }
}
