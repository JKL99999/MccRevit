using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Mcc.Revit.Toolkit.Mvvm.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mcc.Revit.Toolkit.Mvvm
{
    //这里定义的是程序级的Application
    public class UIProvider : IUIProvider
    {
        private UIControlledApplication _application;
        public UIProvider(UIControlledApplication application)
        {
            this._application = application;
        }

        public ControlledApplication GetApplication()
        {
            return GetUIApplication().ControlledApplication;
        }

        public UIControlledApplication GetUIApplication()
        {
            return _application;
        }

        public IntPtr GetWindowHandle()
        {
            return GetUIApplication().MainWindowHandle;
        }

        public AddInId GetAddInId()
        {
            //返回当前程序的AddInId
            return GetUIApplication().ActiveAddInId;
        }
    }
}
