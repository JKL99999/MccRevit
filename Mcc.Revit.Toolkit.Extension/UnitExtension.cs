using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



/**
 * 使用条件编译，来解决多版本框架的无感切换
 * 使用if endif来包裹不同版本RevitAPI的使用方式切换
 * 这样使用方在使用这个代码的时候就会无感切换
 * 这里要使用纯大写的方式，或者右键在属性>生成中配置别名
 * 支持或与非&&、||、!。
 */
namespace Mcc.Revit.Toolkit.Extension
{
    public static class UnitExtension
    {
        public static double ConvertToFeet(this double value)
        {
#if !RVT_22_DEBUG
            return UnitUtils.Convert(value, DisplayUnitType.DUT_MILLIMETERS, DisplayUnitType.DUT_DECIMAL_FEET);
#endif

#if RVT_22_DEBUG
            return UnitUtils.Convert(value, UnitTypeId.Millimeters, UnitTypeId.Feet);
#endif
        }
    }
}
