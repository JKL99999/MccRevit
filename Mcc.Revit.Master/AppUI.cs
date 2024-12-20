using Autodesk.Revit.UI;
using Mcc.Revit.Toolkit.Extension;
using Mcc.Revit.Toolkit.Mvvm.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mcc.Revit.Master
{
    public class AppUI : IApplicationUI
    {
        //将UIProvider存储，提供给后面使用
        private readonly IUIProvider _uiProvider;
        public AppUI(IUIProvider uIProvider)
        {
            this._uiProvider = uIProvider;
        }

        public Result Initial()
        {
            const string _tab = "宝冶大师";
            //Tab -> Panel ->Button
            _uiProvider.GetUIApplication().CreateRibbonTab(_tab);
            RibbonPanel panel = _uiProvider.GetUIApplication().CreateRibbonPanel(_tab, "资源");
            return Result.Succeeded;
        }
    }
}
