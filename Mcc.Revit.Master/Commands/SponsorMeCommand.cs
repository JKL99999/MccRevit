using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Mcc.Revit.Master.Views;
using Mcc.Revit.Toolkit.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Mcc.Revit.Master.Commands
{
    [Transaction(TransactionMode.Manual)]
    public class SponsorMeCommand : ExternalCommandBase
    {
        public override Window CreateMainWindow()
        {
            return new SponsorMe(); // 直接创建 SponsorMe 窗口
        }

        public override Result Execute(ref string message, ElementSet elements)
        {
            MainWindow.ShowDialog(); // 直接展示窗口，不使用事务
            return Result.Succeeded; // 成功返回
        }
    }
}
