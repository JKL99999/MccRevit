using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Mcc.Revit.Entity;
using Microsoft.Web.WebView2.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Mcc.Revit.Master.ViewModels
{
    public class SurfingViewModel: ViewModelBase
    {
        public SurfingViewModel()
        {
            
        }

        //地址栏搜索框输入的内容
        public string InputUrl { get; set; }
        #region 导航command
        public RelayCommand<Object> BackCommand
        {
            get
            {
                return new RelayCommand<Object>((object parameter) =>
                {
                    WebView2 wb2 = parameter as WebView2;
                    if (wb2 != null && wb2.CanGoBack) wb2.GoBack();
                }, false);
            }
        }
        public RelayCommand<Object> ForwardCommand
        {
            get
            {
                return new RelayCommand<Object>((object parameter) =>
                {
                    WebView2 wb2 = parameter as WebView2;
                    if (wb2 != null && wb2.CanGoForward) wb2.GoForward();
                }, false);
            }
        }
        public RelayCommand<Object> RefreshCommand
        {
            get
            {
                return new RelayCommand<Object>((object parameter) =>
                {
                    WebView2 wb2 = parameter as WebView2;
                    if (wb2 != null) wb2.Reload();
                }, false);
            }
        }
        public RelayCommand<Object> HomeCommand
        {
            get
            {
                return new RelayCommand<Object>((object parameter) =>
                {
                    WebView2 wb2 = parameter as WebView2;
                    if (wb2 != null) wb2.CoreWebView2.Navigate("https://www.baidu.com/");
                }, false);
            }
        }
        #endregion


        //webview2控件跳转到指定URL
        public RelayCommand<Object> NavigateUrlCommand
        {
            get
            {
                return new RelayCommand<Object>((object parameter) =>
                {
                    var commandParams = parameter as CommandParameters;
                    if (commandParams != null)
                    {
                        string url = commandParams.UrlIdentify as string;
                        WebView2 webView = commandParams.WebView2Control;
                        string pattern = @"^(http|https)://([\w-]+(\.[\w-]+)+)([\w.,@?^=%&:/~+#-]*[\w@?^=%&/~+#-])?$";
                        if (Regex.IsMatch(url, pattern))
                        {
                            webView.CoreWebView2.Navigate(url);
                        }
                        else
                        {
                            string searchUrl = $"https://www.baidu.com/s?wd={Uri.EscapeDataString(url)}";
                            webView.CoreWebView2.Navigate(searchUrl); // 使用百度搜索
                        }
                        //webView.CoreWebView2.Navigate(url);
                    }
                }, false);
            }
        }

    }
}

