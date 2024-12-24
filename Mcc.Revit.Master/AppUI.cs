using Autodesk.Revit.UI;
using Mcc.Revit.Master.Commands;
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
            RibbonPanel toWebPanel = _uiProvider.GetUIApplication().CreateRibbonPanel(_tab, "联系我");
            panel.CreateButton<MaterialCommand>(
               (b) => {
                   b.Text = "材质管理";
                   b.ToolTip = "材质CRUD";
                   b.LargeImage = Properties.Resources.StairColumn_32.ConvertToBitmapSource();
               }
            );

            toWebPanel.CreateButton<Commands.HelpCommand>(
              (b) => {
                  b.Text = "技术支持";
                  b.ToolTip = "是初一不是十五";
                  b.LargeImage = Properties.Resources.callmeB.ConvertToBitmapSource();
              }
           );
            return Result.Succeeded;
        }
    }
}
