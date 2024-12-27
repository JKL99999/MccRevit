using Autodesk.Revit.DB;
using Mcc.Revit.Toolkit.Mvvm.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mcc.Revit.Master
{
    public class AppEvent : IEventManager
    {

        //虽然我们定义的时候是接口，但是创建的时候用的是子类
        private readonly IUIProvider _uIProvider;
        public AppEvent(IUIProvider uIProvider)
        {
            _uIProvider = uIProvider;
        }
        //程序级(Application)事件的注册与取消的实现
        //调用的地方在我们的基类ApplicationBase里面
        //onStartUp执行Subscribe，onShutDown执行Unsubscribe
        public void Subscribe()
        {
            _uIProvider.GetApplication().DocumentCreated += AppEvent_DocumentCreated;
            _uIProvider.GetUIApplication().ViewActivated += AppEvent_ViewActivated;
        }


        private void AppEvent_DocumentCreated(object sender, Autodesk.Revit.DB.Events.DocumentCreatedEventArgs e)
        {
            //System.Windows.MessageBox.Show("新项目创建成功!");
        }

        private void AppEvent_ViewActivated(object sender, Autodesk.Revit.UI.Events.ViewActivatedEventArgs e)
        {
            Document doc = e.Document as Document;
            string viewName = doc.ActiveView.Name;
            string docPath = doc.PathName;
            //System.Windows.MessageBox.Show($"文档路径:-{docPath}\n激活视图:-{viewName}");
        }

        public void Unsubscribe()
        {
            _uIProvider.GetApplication().DocumentCreated -= AppEvent_DocumentCreated;
            _uIProvider.GetUIApplication().ViewActivated -= AppEvent_ViewActivated;
        }
    }
}
