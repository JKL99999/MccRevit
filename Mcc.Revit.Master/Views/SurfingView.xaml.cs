using Microsoft.Web.WebView2.Core;
using System.Text.RegularExpressions;
using System.Windows;
using System;
using System.Windows.Controls;
using System.Windows.Input;
using System.IO;
using Microsoft.Web.WebView2.Wpf;
using System.Threading.Tasks;

namespace Mcc.Revit.Master.Views
{
    /// <summary>
    /// SurfingView.xaml 的交互逻辑
    /// </summary>
    public partial class SurfingView : Window
    {
        private bool isWebView2Initialized = false;
        private CoreWebView2Environment webView2Environment;

        public SurfingView()
        {
            InitializeComponent();
            Loaded += BiliBiliViewer_Loaded;
        }

        private void BiliBiliViewer_Loaded(object sender, RoutedEventArgs e)
        {
            if (!isWebView2Initialized)
            {
                InitializeAsync();
            }
        }

        private async void InitializeAsync()
        {
            try
            {
                if (isWebView2Initialized || webView == null)
                {
                    return;
                }

                // 设置用户数据文件夹路径到用户的LocalApplicationData目录
                string userDataFolder = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                    "RevitWebView2",
                    "UserData"
                );

                // 确保目录存在
                Directory.CreateDirectory(userDataFolder);

                // 创建自定义环境配置
                var options = new CoreWebView2EnvironmentOptions();
                webView2Environment = await CoreWebView2Environment.CreateAsync(null, userDataFolder, options);

                // 使用自定义环境初始化WebView2
                await webView.EnsureCoreWebView2Async(webView2Environment);

                // 配置WebView2设置
                webView.CoreWebView2.NewWindowRequested += CoreWebView2_NewWindowRequested;
                webView.CoreWebView2.Navigate("https://www.bilibili.com/");

                isWebView2Initialized = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"初始化WebView2时发生错误: {ex.Message}",
                              "初始化错误",
                              MessageBoxButton.OK,
                              MessageBoxImage.Error);
            }
        }

        private void CoreWebView2_NewWindowRequested(object sender, CoreWebView2NewWindowRequestedEventArgs e)
        {
            if (webView?.CoreWebView2 != null)
            {
                webView.CoreWebView2.Navigate(e.Uri);
                e.Handled = true;
            }
        }

        private void AddressBar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && sender is TextBox addressBar && webView?.CoreWebView2 != null)
            {
                string pattern = @"^(http|https)://([\w-]+(\.[\w-]+)+)([\w.,@?^=%&:/~+#-]*[\w@?^=%&/~+#-])?$";
                string url = addressBar.Text;

                if (Regex.IsMatch(url, pattern))
                {
                    webView.CoreWebView2.Navigate(url);
                }
                else
                {
                    string searchUrl = $"https://www.baidu.com/s?wd={Uri.EscapeDataString(url)}";
                    webView.CoreWebView2.Navigate(searchUrl);
                }
            }

        }
    }
}
