using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
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
    public class DockablePaneCommand : CommandBase
    {
        public override Window CreateMainWindow()
        {
            return null;
        }

        public override Result Execute(ref string message, ElementSet elements)
        {
            var id = DockablePanes.DockablePaneProvider.Id;
            if (!DockablePane.PaneExists(id))
            {
                return Result.Cancelled;
            }
            DockablePane pane = DataContext.GetUIApplication().GetDockablePane(id);
            if (pane.IsShown())
            {
                pane.Hide();
            }
            else
            {
                pane.Show();
            }

            return Result.Succeeded;
        }
    }
}
