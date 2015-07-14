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
using NTNL.ViewModels.items;
using NTNL.Models.DB;
using System.Threading.Tasks;

namespace NTNL.ViewModels
{
    public class SettingWindowViewModel : ViewModel
    {

        NTNLs ntnls;

        public void Initialize()
        {
            ntnls = NTNLs.Instance;
        }

        public MainWindowViewModel main { get; set; }

        public SettingWindowViewModel(MainWindowViewModel mw)
        {
            this.main = mw;
        }

        public SettingWindowViewModel()
        {

        }

        #region PrivateLists変更通知プロパティ
        private ReadOnlyDispatcherCollection<PrivateListsViewModel> _PrivateLists;

        public ReadOnlyDispatcherCollection<PrivateListsViewModel> PrivateLists
        {
            get
            { return _PrivateLists; }
            set
            {
                if (_PrivateLists == value)
                    return;
                _PrivateLists = value;

                if (selectedAccount != null)
                {
                    foreach (var plist in PrivateLists)
                    {
                        if (plist.ID == selectedAccount.account.ID)
                        {
                            Privates = plist.Privates;
                            Console.WriteLine(plist.ID);
                        }
                    }
                }
                RaisePropertyChanged();
            }
        }
        #endregion

        #region SelectedPrivates変更通知プロパティ
        private ReadOnlyDispatcherCollection<PrivateViewModel> _Privates;

        public ReadOnlyDispatcherCollection<PrivateViewModel> Privates
        {
            get
            { return _Privates; }
            set
            {
                if (_Privates == value)
                    return;
                _Privates = value;
                
                RaisePropertyChanged();
            }
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
                
                foreach (var plist in PrivateLists)
                {
                    if (plist.ID == value.account.ID)
                    {
                        Privates = plist.Privates;
                        Console.WriteLine(plist.ID);
                    }
                }
                Console.WriteLine(value.ScreenName + "" + value.IsSelected.ToString());
                RaisePropertyChanged();
            }
        }
        #endregion


        #region Text変更通知プロパティ
        private string _Text = "";

        public string Text
        {
            get
            { return _Text; }
            set
            { 
                if (_Text == value)
                    return;
                _Text = value;
                if (Text != "")
                {
                    IsWrite = true;
                }else{

                    IsWrite = false;

                }
                RaisePropertyChanged();
            }
        }
        #endregion


        #region IsWrite変更通知プロパティ
        private bool _IsWrite;

        public bool IsWrite
        {
            get
            { return _IsWrite; }
            set
            { 
                if (_IsWrite == value)
                    return;
                _IsWrite = value;
                RaisePropertyChanged();
            }
        }
        #endregion


        #region RegisterNGWordCommand
        private ViewModelCommand _RegisterNGWordCommand;

        public ViewModelCommand RegisterNGWordCommand
        {
            get
            {
                if (_RegisterNGWordCommand == null)
                {
                    _RegisterNGWordCommand = new ViewModelCommand(RegisterNGWord);
                }
                return _RegisterNGWordCommand;
            }
        }

        public void RegisterNGWord()
        {
            Console.WriteLine(Text + "   "+ selectedAccount.account.ID);
            
                    try
                    {
                        DBFacade.Instance.insertPrivate(selectedAccount.account.ID.ToString(), Text);
                        
                        ntnls.installPrivate();
                       
                    }
                    catch (Exception)
                    {
                        
                       
                    }
                    
               
            
        }
        #endregion


    }
}
