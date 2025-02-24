using Autodesk.Revit.DB;
using GalaSoft.MvvmLight.Messaging;
using Mcc.Revit.Entity;
using Mcc.Revit.Master.Contacts;
using Mcc.Revit.Master.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Mcc.Revit.Master.Views
{
    /// <summary>
    /// Materials.xaml 的交互逻辑
    /// </summary>
    public partial class Materials : Window
    {
        //public MaterialsViewModel _materialsViewModel;
        public Materials(Document document)
        {
            InitializeComponent();
            //_materialsViewModel = new MaterialsViewModel(document);
            //this.DataContext = _materialsViewModel;
            // 绑定到 Revit 的主窗体
            System.Windows.Interop.WindowInteropHelper mainUI = new System.Windows.Interop.WindowInteropHelper(this);
            mainUI.Owner = System.Diagnostics.Process.GetCurrentProcess().MainWindowHandle;

            //消息通知
            Messenger.Default.Register<bool>(this,Tokens.CloseWindow, CloseWindow);
            Messenger.Default.Register<NotificationMessageAction<MaterialDTO>>(this, Tokens.ShowMaterialDialog, ShowMaterialDialog);
            this.Unloaded += Materials_Unloaded;
        }

        //带消息回调的消息中心的使用方式
        private void ShowMaterialDialog(NotificationMessageAction<MaterialDTO> message)
        {
            MaterialDialog dialog = new MaterialDialog();
            MaterialDialogViewModel dialogViewModel = (MaterialDialogViewModel)message.Target;
            dialog.DataContext = dialogViewModel;
            dialogViewModel.Initial(message.Sender);
            // 将编辑材质窗口展示出来；创建材质窗口关闭的时候result返回true
            if (dialog.ShowDialog().Value)
            {
                //子窗口submit提交后，把创建或者修改的Material发送给父窗口VM,消息的回调函数更新数据
                message.Execute(dialogViewModel.Material);
            }
        }

        //简单的不带消息回调的消息中心的使用方式
        //private void ShowMaterialDialog(MaterialDTO material)
        //{
        //    MaterialDialog dialog = new MaterialDialog();
        //    dialog.DataContext = new MaterialDialogViewModel(material);
        //    dialog.ShowDialog();
        //}

        private void Materials_Unloaded(object sender, RoutedEventArgs e)
        {
            //不加任何泛型的话，直接可以注销这个view，那么在这个View上边注册的消息就会全部取消注册
            Messenger.Default.Unregister(this);
            //在ViewModel中注册的消息通知，也可以在其对应的View的DataContext中取消消息的注册
            Messenger.Default.Unregister(this.DataContext);
        }

        private void CloseWindow(bool result)
        {
            this.DialogResult = result;
            this.Close();
        }
    }
}
