using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using GalaSoft.MvvmLight.Ioc;
using Mcc.Revit.Toolkit.Mvvm.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mcc.Revit.Toolkit.Mvvm
{
    public class DataContext : IDataContext
    {

        //和以前的写法不太一样
        //var uiDoc = commandData.Application.ActiveUIDocument;
        //var doc = uiDoc.Document;

        public SimpleIoc ContainerProvider => ExternalApplicationBase.ContainerProvider;
        public Document GetDocument()
        {
            //容器池是程序公用的。默认的池子名称是default。
            //从default容器里面获取注册的Document服务
            return ContainerProvider.GetInstance<Document>();
        }

        public UIApplication GetUIApplication()
        {
            return GetUIDocument().Application;
        }

        //UIDocument里面有许多我们需要的属性
        //ActiveView;ActiveGraphicalView;Application;Document;IsValidObject;Selection
        public UIDocument GetUIDocument()
        {
            return new UIDocument(GetDocument());
        }
    }
}
