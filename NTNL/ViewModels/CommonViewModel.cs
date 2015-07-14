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

namespace NTNL.ViewModels
{
    public class CommonViewModel : ViewModel
    {
        
        public void Initialize()
        {
        }

        #region singleton
        static CommonViewModel _instance;

        /// <summary>
        /// DBFacadeの唯一のインスタンスを取得します。
        /// </summary>
        public static CommonViewModel Instance
        {
            get
            {
                if (_instance == null) _instance = new CommonViewModel();
                return _instance;
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
                RaisePropertyChanged();
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


        #region isExpand変更通知プロパティ
        private bool _isExpand;

        public bool isExpand
        {
            get
            { return _isExpand; }
            set
            {
                if (_isExpand == value)
                    return;
                _isExpand = value;
                RaisePropertyChanged();
            }
        }
        #endregion


        #region isReply変更通知プロパティ
        private bool _isReply;

        public bool isReply
        {
            get
            { return _isReply; }
            set
            { 
                if (_isReply == value)
                    return;
                _isReply = value;
                RaisePropertyChanged();
            }
        }
        #endregion


        #region statusID変更通知プロパティ
        private long _statusID;

        public long statusID
        {
            get
            { return _statusID; }
            set
            { 
                if (_statusID == value)
                    return;
                _statusID = value;
                RaisePropertyChanged();
            }
        }
        #endregion



        public CommonViewModel()
        {

        }
    }
}
