using GalaSoft.MvvmLight.Ioc;
using Mcc.Revit.Toolkit.Mvvm;
using Mcc.Revit.Toolkit.Mvvm.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mcc.Revit.Master
{
    public class App : ApplicationBase
    {
        //程序的主入口文件
        //程序按照ApplicationBase里面定义的方法顺序执行的
        //最后在Onstat()方法内部执行RegisterTypes(SimpleIoc.Default)
        public override void RegisterTypes(SimpleIoc containter)
        {
            //注册程序级UI
            containter.Register<IApplicationUI, AppUI>();
            //注册程序级事件
            containter.Register<IEventManager, AppEvent>();
        }
    }
}
