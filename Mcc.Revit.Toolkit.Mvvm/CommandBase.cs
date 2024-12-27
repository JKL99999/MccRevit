using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using CommonServiceLocator;
using GalaSoft.MvvmLight.Ioc;
using Mcc.Revit.Toolkit.Mvvm.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Mcc.Revit.Toolkit.Mvvm
{
    public abstract class CommandBase : IExternalCommand
    {
        //abstract抽象方法一定要重载;virtual虚方法是预留的接口，需要用到的时候再重载
        public virtual void RegisterTypes(SimpleIoc simpleIoc) { }

        public abstract Window CreateMainWindow();
        //创建按钮程序的主窗体,View，并绑定对应的ViewModel
        public Window MainWindow { get; set; }
        public abstract Result Execute(ref string message, ElementSet elements);

        //DataContext是在OnstartUp方法中注册到容器的，包含了document,UIAPPLication,UIDocument，在不同地方注册到容器
        protected IDataContext DataContext { get => ServiceLocator.Current.GetInstance<IDataContext>(); }

        public SimpleIoc ContainerProvider => ApplicationBase.ContainerProvider;

        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {


            try
            {
                // 注册 Document 到 IOC 容器
                ContainerProvider.Register<Document>(() => commandData.Application.ActiveUIDocument.Document);
                //MessageBox.Show(ContainerProvider.GetHashCode().ToString());
                // 注册其他类型
                RegisterTypes(ContainerProvider);

                // 创建主窗口
                var window = CreateMainWindow();

                if (window != null)
                {
                    MainWindow = window;
                }

                // 执行具体的命令逻辑
                Execute(ref message, elements);
            }
            catch (Exception ex)
            {
                // 处理异常
                message = ex.Message;
                return Result.Failed;  // 返回 Result.Failed
            }
            finally
            {
                // 取消注册 Document
                ContainerProvider.Unregister<Document>();
            }

            // 这一行不会执行
            return Result.Succeeded;
        }
    }
}
