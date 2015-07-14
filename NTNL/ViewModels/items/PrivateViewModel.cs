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

namespace NTNL.ViewModels.items
{
    public class PrivateViewModel : ViewModel
    {
        

        public void Initialize()
        {
        }

        public PrivateViewModel(MainWindowViewModel main, string word)
        {
            this.NGword = word;

        }


        #region NGword変更通知プロパティ
        private string _NGword;

        public string NGword
        {
            get
            { return _NGword; }
            set
            { 
                if (_NGword == value)
                    return;
                _NGword = value;
                RaisePropertyChanged();
            }
        }
        #endregion

    }
}
