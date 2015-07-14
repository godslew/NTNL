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
    public class StatusTimeLineViewModel : ViewModel
    {
       
        public StatusTimeLine source { get; private set; }

        public StatusTimeLineViewModel(MainWindowViewModel main, StatusTimeLine _tl)
        {
            source = _tl;
            Statuses = ViewModelHelper.CreateReadOnlyDispatcherCollection(
                _tl.Statuses,
                (p) =>
                {
                    return new StatusViewModel(main, p);
                },
                DispatcherHelper.UIDispatcher);
            Name = source.Name;
            //QueryText = source.Query.QueryText;
            
        }


        public void Initialize()
        {
        }

        #region Statuses変更通知プロパティ
        private ReadOnlyDispatcherCollection<StatusViewModel> _Statuses;

        public ReadOnlyDispatcherCollection<StatusViewModel> Statuses
        {
            get
            { return _Statuses; }
            set
            {
                if (_Statuses == value)
                    return;
                _Statuses = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region Name変更通知プロパティ
        private string _Name;

        public string Name
        {
            get
            { return _Name; }
            set
            {
                if (_Name == value)
                    return;
                _Name = value;
                source.Name = value;
                RaisePropertyChanged();
            }
        }
        #endregion


        #region QueryText変更通知プロパティ
        private string _QueryText;

        public string QueryText
        {
            get
            { return _QueryText; }
            set
            {
                if (_QueryText == value)
                    return;
                _QueryText = value;
                //source.Query = new K.Query.Kbtter3Query(value);
                RaisePropertyChanged();
            }
        }
        #endregion
    }
}
