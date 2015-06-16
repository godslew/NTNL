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

        NTNLs ntnls;


        public void Initialize()
        {
            ntnls = NTNLs.Instance;
            listener = new PropertyChangedEventListener(ntnls);
            CompositeDisposable.Add(listener);
        }

        public MainWindowViewModel()
        {
            this.columnList = new ObservableCollection<ColumnItemViewModel>();
            this.columnList.Add(new ColumnItemViewModel(this,"Home", 0));
            this.columnList.Add(new ColumnItemViewModel(this,"Mention", 1));
            this.columnList.Add(new ColumnItemViewModel(this, "Activity", 2));
            columnList.First().statusList.Add(new StatusViewModel("testtest"));
            columnList.First().statusList.Add(new StatusViewModel("test"));
            columnList.First().statusList.Add(new StatusViewModel("testtest"));
            columnList.First().statusList.Add(new StatusViewModel("test"));
            columnList.First().statusList.Add(new StatusViewModel("testtest"));
            columnList.First().statusList.Add(new StatusViewModel("test"));
            columnList.First().statusList.Add(new StatusViewModel("testtest"));
            columnList.First().statusList.Add(new StatusViewModel("test"));
            columnList.First().statusList.Add(new StatusViewModel("testtest"));
            columnList.First().statusList.Add(new StatusViewModel("testtest"));
            columnList.First().statusList.Add(new StatusViewModel("testtest"));
            columnList.First().statusList.Add(new StatusViewModel("testtest"));
            columnList.First().statusList.Add(new StatusViewModel("testtest"));
            columnList.First().statusList.Add(new StatusViewModel("testtest"));
            columnList.First().statusList.Add(new StatusViewModel("testtest"));

           

            BindingOperations.EnableCollectionSynchronization(this.columnList, new object());

        }

        #region OpenTextBoxCommand
        private ViewModelCommand _OpenTextBoxCommand;

        public ViewModelCommand OpenTextBoxCommand
        {
            get
            {
                if (_OpenTextBoxCommand == null)
                {
                    _OpenTextBoxCommand = new ViewModelCommand(OpenTextBox);
                }
                return _OpenTextBoxCommand;
            }
        }

        public  void OpenTextBox()
        {
            Console.WriteLine("test");            
       
            //messageを使ってみた,非常につよい
            var message = new TransitionMessage(typeof(Views.AccountManagerWindow), new AccountManagerViewModel(), TransitionMode.Modal);
            Messenger.Raise(message);
            
        }
        #endregion

         #region columnList変更通知プロパティ
        private ObservableCollection<ColumnItemViewModel> _columnList;

         public ObservableCollection<ColumnItemViewModel> columnList
        {
            get
            { return _columnList; }
            set
            { 
                if (_columnList == value)
                    return;
                _columnList = value;
                RaisePropertyChanged();
            }
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
            this.columnList.Add(new ColumnItemViewModel(this, "test", columnList.Count()));
            //Console.WriteLine("test" + columnList.Count);
        }
        #endregion
        
    }
}
