using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mcc.Revit.Toolkit.Mvvm.Interfaces
{
    public interface IApplicationUI
    {

        //把UI和事件分开写
        //这里注册RibbonUI的一些内容
        //由于每一个插件的UI都不一样，所以只定义了接口，所以放到主项目实现
        Result Initial();
    }
}
