using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mcc.Revit.Toolkit.Mvvm.Interfaces
{
    public interface IUIProvider
    {
        //程序级Application
        //public Result OnStartup(UIControlledApplication application){}
        //代表Revit用户界面，提供对Ul自定义方法和事件的访问。
        UIControlledApplication GetUIApplication();
        ControlledApplication GetApplication();
        //窗口句柄
        IntPtr GetWindowHandle();
        //Revit插件AddIn文件ID
        AddInId GetAddInId();
    }
}
