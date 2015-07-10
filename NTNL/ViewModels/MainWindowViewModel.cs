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

namespace NTNL.ViewModels
{
    public class MainWindowViewModel : ViewModel
    {
        /* コマンド、プロパティの定義にはそれぞれ 
         * 
         *  lvcom   : ViewModelCommand
         *  lvcomn  : ViewModelCommand(CanExecute無)
         *  llcom   : ListenerCommand(パラメータ有のコマンド)
         *  llcomn  : ListenerCommand(パラメータ有のコマンド・CanExecute無)
         *  lprop   : 変更通知プロパティ(.NET4.5ではlpropn)
         *  
         * を使用してください。
         * 
         * Modelが十分にリッチであるならコマンドにこだわる必要はありません。
         * View側のコードビハインドを使用しないMVVMパターンの実装を行う場合でも、ViewModelにメソッドを定義し、
         * LivetCallMethodActionなどから直接メソッドを呼び出してください。
         * 
         * ViewModelのコマンドを呼び出せるLivetのすべてのビヘイビア・トリガー・アクションは
         * 同様に直接ViewModelのメソッドを呼び出し可能です。
         */

        /* ViewModelからViewを操作したい場合は、View側のコードビハインド無で処理を行いたい場合は
         * Messengerプロパティからメッセージ(各種InteractionMessage)を発信する事を検討してください。
         */

        /* Modelからの変更通知などの各種イベントを受け取る場合は、PropertyChangedEventListenerや
         * CollectionChangedEventListenerを使うと便利です。各種ListenerはViewModelに定義されている
         * CompositeDisposableプロパティ(LivetCompositeDisposable型)に格納しておく事でイベント解放を容易に行えます。
         * 
         * ReactiveExtensionsなどを併用する場合は、ReactiveExtensionsのCompositeDisposableを
         * ViewModelのCompositeDisposableプロパティに格納しておくのを推奨します。
         * 
         * LivetのWindowテンプレートではViewのウィンドウが閉じる際にDataContextDisposeActionが動作するようになっており、
         * ViewModelのDisposeが呼ばれCompositeDisposableプロパティに格納されたすべてのIDisposable型のインスタンスが解放されます。
         * 
         * ViewModelを使いまわしたい時などは、ViewからDataContextDisposeActionを取り除くか、発動のタイミングをずらす事で対応可能です。
         */

        /* UIDispatcherを操作する場合は、DispatcherHelperのメソッドを操作してください。
         * UIDispatcher自体はApp.xaml.csでインスタンスを確保してあります。
         * 
         * LivetのViewModelではプロパティ変更通知(RaisePropertyChanged)やDispatcherCollectionを使ったコレクション変更通知は
         * 自動的にUIDispatcher上での通知に変換されます。変更通知に際してUIDispatcherを操作する必要はありません。
         */


        PropertyChangedEventListener listener;
        public MainWindowViewViewModel View { get; private set; }
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
            common.isExpand = false;

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
            common = CommonViewModel.Instance;
          
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
            Console.WriteLine(parameter);
            if (selectedAccount != null)
            {
                if (!isReply)
                {
                    TwitterFacade.Instance.UpdateStatus(selectedAccount.account, parameter);
                    Text = "";
                }
                else if (isReply)
                {
                    TwitterFacade.Instance.ReplyToStatus(selectedAccount.account, parameter, ReplyingStatus.id);
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
            var t = new StringBuilder();
            ru.ForEach(p => t.Append(String.Format("@{0} ", p)));
            Text = t.ToString();
        }
        #endregion

        #region
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
