using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;

using Livet;
using NTNL.ViewModels;
using NTNL.Views;

namespace NTNL
{
    /// <summary>
    /// App.xaml の相互作用ロジック
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            DispatcherHelper.UIDispatcher = Dispatcher;
            //AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
        }

        //集約エラーハンドラ
        //private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        //{
        //    //TODO:ロギング処理など
        //    MessageBox.Show(
        //        "不明なエラーが発生しました。アプリケーションを終了します。",
        //        "エラー",
        //        MessageBoxButton.OK,
        //        MessageBoxImage.Error);
        //
        //    Environment.Exit(1);
        //}

        private Dictionary<Type, Type> ViewModels { get; set; }

        // コンストラクタ
        public App()
            : base()
        {
            // ViewModel と View の対応を設定する
            ViewModels = new Dictionary<Type, Type>();
            ViewModels.Add(typeof(SettingWindowViewModel), typeof(SettingWindow));
            ViewModels.Add(typeof(AccountManagerWindowViewModel), typeof(AccountManagerWindow));
        }

        // ViewModelからViewを生成する
        public Window CreateView<T>(T viewModel)
        {
            // ViewModel に対応する Viewが存在する？
            if (ViewModels.ContainsKey(viewModel.GetType()))
            {
                // View を生成し、DataContext に ViewModel を設定する
                Type viewType = ViewModels[viewModel.GetType()];
                Window wnd = Activator.CreateInstance(viewType) as Window;
                if (wnd != null)
                    wnd.DataContext = viewModel;
                return wnd;
            }
            else
                return null;
        }

        // ViewModelからモーダルでViewを表示する
        public bool ShowModalView<T>(T viewModel)
        {
            Window view = CreateView(viewModel);
            if (view != null)

                return (view.ShowDialog() == true);
            else
                return false;
        }

        // ViewModeからモードレスでViewを表示する
        public void ShowView<T>(T viewModel)
        {
            Window view = CreateView(viewModel);
            if (view != null)
                view.Show();
        }
    }
}
