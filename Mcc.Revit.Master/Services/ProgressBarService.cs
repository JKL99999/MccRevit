using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Messaging;
using Mcc.Revit.Master.IServices;
using Mcc.Revit.Master.ViewModels;
using Mcc.Revit.Master.Views;
using Mcc.Revit.Toolkit.Mvvm.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace Mcc.Revit.Master.Services
{
    public class ProgressBarService : IProgressBarService
    {
        private ProgressBarDialog _dialog;
        public void Start(int maximum)
        {
            _dialog = App.ContainerProvider.Resolve<ProgressBarDialog, ProgressBarDialogViewModel>();
            _dialog.Show();
            Messenger.Default.Send<int>(maximum, Contacts.Tokens.ProgressBarMaximum);
        }

        public void Stop()
        {
            Messenger.Default.Unregister(_dialog.DataContext);
            //关闭窗口
            _dialog.Close();
        }
    }
}
