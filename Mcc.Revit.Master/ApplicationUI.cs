using Autodesk.Revit.UI;
using Mcc.Revit.Toolkit.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mcc.Revit.Master
{
    public class ApplicationUI : IExternalApplication
    {
        private const string _tab = "宝冶大师";

        public Result OnShutdown(UIControlledApplication application)
        {
            return Result.Succeeded;
        }

        public Result OnStartup(UIControlledApplication application)
        {
            application.CreateRibbonTab(_tab);
            RibbonPanel panel = application.CreateRibbonPanel(_tab,"资源");
            panel.CreateButton<Commands.MaterialsCommand>(
                (b) => {
                    b.Text = "材质管理";
                    b.ToolTip = "材质CRUD";
                    b.LargeImage = Properties.Resources.StairColumn_32.ConvertToBitmapSource();
                }
            );

            return Result.Succeeded;
        }
    }
}
