using Autodesk.Revit.UI;
using CommonServiceLocator;
using GalaSoft.MvvmLight.Ioc;
using Mcc.Revit.Toolkit.Mvvm.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Mcc.Revit.Toolkit.Mvvm
{
    public abstract class ApplicationBase : IExternalApplication
    {
        //程序的主入口
        //有抽象属性或抽象方法的类一定是抽象类(abstract class)，抽象类中的属性或方法不一定都是抽象的
        //非抽象子类，必须实现基类中的抽象方法。抽象方法的修饰符必须是Public
        public abstract void RegisterTypes(SimpleIoc containter);


        public Result OnShutdown(UIControlledApplication application)
        {
            //插件程序结束，取消所有事件的注册
            var events = ServiceLocator.Current.GetInstance<IEventManager>();
            if (events != null)
            {
                events.Unsubscribe();
            }
            return Result.Succeeded;
        }

        public Result OnStartup(UIControlledApplication application)
        {
            //手动加载dll
            this.load();

            //开始初始化IOC容器，并根据接口或实例注入
            //容器写在Application这里，不可以使用AddinManager调试

            if (!ServiceLocator.IsLocationProviderSet)
            {
                ServiceLocator.SetLocatorProvider(()=>SimpleIoc.Default);
            }
            //IOC容器创建好后，就开始向Ioc容器中添加各种所需用的对象
            SimpleIoc.Default.Register<UIControlledApplication>(()=>application);
            SimpleIoc.Default.Register<IUIProvider,UIProvider>();
            SimpleIoc.Default.Register<IDataContext, DataContext>();

            //执行子类重载之后的方法，进行服务的注册
            RegisterTypes(SimpleIoc.Default);

            //订阅事件
            var events = ServiceLocator.Current.GetInstance<IEventManager>();
            if (events != null)
            {
                events.Subscribe();
            }

            //创建Ribbon UI
            //IApplicationUI是在项目中注册App.cs中创建的，在APPUI.cs实例化的
            var appUI = ServiceLocator.Current.GetInstance<IApplicationUI>();
            // 如果appUI是null返回cancelled。否则进行UI界面初始化
            return appUI == null ? Result.Cancelled : appUI.Initial();
        }


        private void load()
        {
            // Assembly.GetExecutingAssembly().Location 
            //var folder = System.IO.Path.GetDirectoryName(this.GetType().Assembly.Location);
            //var path = System.IO.Path.Combine(folder, @"GalaSoft.MvvmLight.dll");
            //Assembly.LoadFrom(path);
            //AssemblyResolve事件在.Net对程序集的解析失败时触发，返回一个Assembly对象即参数e。
            AppDomain.CurrentDomain.AssemblyResolve += (o, e) =>
            {
                Assembly result = null;
                var assemblyName = e.Name.Split(',')[0];
                if (assemblyName.EndsWith(".resorces", StringComparison.InvariantCultureIgnoreCase))
                {
                    return result;
                }
                DirectoryInfo info = new DirectoryInfo(this.GetType().Assembly.Location);
                string filePath = $@"{info.Parent.FullName}/{assemblyName}.dll";
                if (File.Exists(filePath))
                {
                    result = Assembly.LoadFrom(filePath);
                }
                return result;
            };
        }
    }
}
