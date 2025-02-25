using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using GalaSoft.MvvmLight.Ioc;
using Mcc.Revit.Master.ViewModels;
using Mcc.Revit.Master.Views;
using Mcc.Revit.Toolkit.Extension;
using Mcc.Revit.Toolkit.Mvvm;
using Mcc.Revit.Toolkit.Mvvm.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Mcc.Revit.Master.Commands
{
    //事务特性(必须添加)；日志；生成模式
    [Transaction(TransactionMode.Manual)]
    public class MaterialCommand : ExternalCommandBase
    {
        public override Window CreateMainWindow()
        {
            return ContainerProvider.Resolve<Materials, MaterialsViewModel>();
        }

        public override Result Execute(ref string message, ElementSet elements)
        {
            TransactionStatus status = DataContext.GetDocument().NewTransactionGroup(() => MainWindow.ShowDialog().Value, "资源管理");
            return status == TransactionStatus.Committed ? Result.Succeeded : Result.Failed;
        }
    }
}
