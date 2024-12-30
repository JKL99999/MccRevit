using Autodesk.Revit.UI;
using CommonServiceLocator;
using GalaSoft.MvvmLight.Ioc;
using Mcc.Revit.Master.Cache;
using Mcc.Revit.Master.ExtEvents;
using Mcc.Revit.Master.IServices;
using Mcc.Revit.Master.Services;
using Mcc.Revit.Master.ViewModels;
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
        public static SimpleIoc CurrentContainer => (SimpleIoc)ServiceLocator.Current;

        public static App Current { get; private set; }

        //注册外部事件
        private ExternalEvent _createText;

        public override void RegisterTypes(SimpleIoc containter)
        {
            Current = this;

            //初始化外部事件的方法
            RegisterExternalEvents();

            //注册程序级UI
            containter.Register<IApplicationUI, AppUI>();

            //注册程序级事件
            containter.Register<IEventManager, AppEvent>();

            //注册VM解耦的服务Service
            containter.Register<IMaterialService, MaterialService>();
            containter.Register<IProgressBarService, ProgressBarService>();

            //注册命令级ViewModel。与View对应
            containter.Register<MaterialsViewModel>();
            containter.Register<ProgressBarDialogViewModel>();

            //注册命令级View。新增按钮也统一在这里注册。
            containter.Register<Views.Materials>();
            containter.Register<Views.ProgressBarDialog>();
        }

        private void RegisterExternalEvents()
        {
            //注册外部事件,把载入族的外部事件提前加载进来
            CreateText createText = new CreateText();
            _createText = ExternalEvent.Create(createText);
            SysCache.Instance.CreateText = _createText;
        }
    }
}
