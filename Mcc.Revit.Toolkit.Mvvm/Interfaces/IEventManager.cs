using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mcc.Revit.Toolkit.Mvvm.Interfaces
{
    public interface IEventManager
    {
        //把UI和事件分开写
        //这里注册一些事件
        ////由于每一个插件的事件都不一样，所以只定义了接口，所以放到主项目实现
        void Subscribe();
        void Unsubscribe();
    }
}
