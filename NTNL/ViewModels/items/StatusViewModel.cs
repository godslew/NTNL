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

namespace NTNL.ViewModels
{
    public class StatusViewModel : ViewModel
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

        public Status SourceStatus { get; private set; }

        public Status ReceivedStatus { get; private set; }

        public NTNLs ntnls;
        public MainWindowViewModel main;
        public PropertyChangedEventListener listener;
        public CommonViewModel common { get; set; }

        public void Initialize()
        {
        }

        public StatusViewModel(MainWindowViewModel mw, Status _status){
            this.SourceStatus = _status;
            ReceivedStatus = SourceStatus;
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

        static Regex reg = new Regex("<a href=\"(?<url>.+)\" rel=\"nofollow\">(?<client>.+)</a>");
        
        public void ExtractVia()
        {
            var m = reg.Match(SourceStatus.Source);
            if (!m.Success) return;

            Via = "via" + m.Groups["client"].Value;
            
        }


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
                TwitterFacade.Instance.RetweetStatus(main.selectedAccount.account, id);
            }
        }
        #endregion


    }
}
