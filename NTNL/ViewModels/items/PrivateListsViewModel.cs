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
using NTNL.Models.DB;

namespace NTNL.ViewModels.items
{
    public class PrivateListsViewModel : ViewModel
    {
        
        public NTNLPrivate Source { get; private set; }
        public MainWindowViewModel main;

        public void Initialize()
        {
        }

        public PrivateListsViewModel(MainWindowViewModel mw, NTNLPrivate source)
        {
            this.Source = source;
            this.ID = source.ID;
            this.main = mw;
            Privates = ViewModelHelper.CreateReadOnlyDispatcherCollection(
                source.NGList,
                (p) =>
                {
                    return new PrivateViewModel(main, p);
                },
                DispatcherHelper.UIDispatcher);


        }

        #region Privates変更通知プロパティ
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

        #region ID変更通知プロパティ
        private long _ID;

        public long ID
        {
            get
            { return _ID; }
            set
            { 
                if (_ID == value)
                    return;
                _ID = value;
                RaisePropertyChanged();
            }
        }
        #endregion

    }
}
