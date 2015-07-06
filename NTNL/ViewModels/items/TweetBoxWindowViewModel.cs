﻿using System;
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

namespace NTNL.ViewModels.items
{
    public class TweetBoxWindowViewModel : ViewModel
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

        NTNLs ntnls;
        PropertyChangedEventListener listener;

        public void Initialize()
        {
            ntnls = NTNLs.Instance;
            listener = new PropertyChangedEventListener(ntnls);
            CompositeDisposable.Add(listener);
            Accounts = ViewModelHelper.CreateReadOnlyDispatcherCollection(
                ntnls.Accounts,
                p => new AccountViewModel(p),
                DispatcherHelper.UIDispatcher);
        }


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
                }
                else
                {
                    isWrite = false;
                }
                RaisePropertyChanged("Text");
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
                Console.WriteLine(value.First().ScreenName);
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
                Console.WriteLine(value.ScreenName);
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
                TwitterFacade.Instance.UpdateStatus(selectedAccount.account, parameter);
                Text = "";
            }
            
        }
        #endregion




        public TweetBoxWindowViewModel()
        {

        }
    }
}
