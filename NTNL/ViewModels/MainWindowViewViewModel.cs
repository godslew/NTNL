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
using System.Collections.ObjectModel;

namespace NTNL.ViewModels
{
    public class MainWindowViewViewModel : ViewModel
    {
       
        public void Initialize()
        {
        }

        #region StatusTimeline変更通知プロパティ
        private ReadOnlyDispatcherCollection<StatusTimeLineViewModel> _StatusTimeline;

        public ReadOnlyDispatcherCollection<StatusTimeLineViewModel> StatusTimeline
        {
            get
            { return _StatusTimeline; }
            set
            {
                if (_StatusTimeline == value)
                    return;
                _StatusTimeline = value;
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
    }
}
