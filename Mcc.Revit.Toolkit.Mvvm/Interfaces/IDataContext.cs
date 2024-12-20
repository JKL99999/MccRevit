using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mcc.Revit.Toolkit.Mvvm.Interfaces
{
    public interface IDataContext
    {
        //command级Application(按钮对应的命令)
        Document GetDocument();
        UIDocument GetUIDocument();
        UIApplication GetUIApplication();
    }
}
