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
