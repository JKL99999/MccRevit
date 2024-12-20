using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mcc.Revit.Toolkit.Extension
{
    public static class ColorExtension
    {
        public static Autodesk.Revit.DB.Color ConvertToRevitColor(this System.Drawing.Color color)
        {
            //将Brush类型的Color转换为Revit中的color
            return new Autodesk.Revit.DB.Color(color.R, color.G, color.B);
        }

    }
}
