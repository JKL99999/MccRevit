using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Mcc.Revit.Master.DockablePanes
{
    public class FrameworkElementCreator : IFrameworkElementCreator
    {
        public FrameworkElement CreateFrameworkElement()
        {
           var dockablePane = new Views.DockablePaneView();
           return dockablePane;
        }
    }
}
