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

namespace NTNL.ViewModels.items
{
    public class TweetBoxWindowViewModel : ViewModel
    {

       
       
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
