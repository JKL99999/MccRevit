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
    /// MaterialDialog.xaml 的交互逻辑
    /// </summary>
    public partial class MaterialDialog : Window
    {
        public MaterialDialog()
        {
            InitializeComponent();
            Messenger.Default.Register<bool>(this, Contacts.Tokens.CloseDialogWindow, CloseWindow);

            this.Unloaded += MaterialDialog_Unloaded;
        }

        private void MaterialDialog_Unloaded(object sender, RoutedEventArgs e)
        {
            Messenger.Default.Unregister(this); // 卸载所有消息
        }

        private void CloseWindow(bool obj)
        {
            this.DialogResult = obj;
            this.Close();
        }
    }
}
