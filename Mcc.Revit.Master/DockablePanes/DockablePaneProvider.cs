using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mcc.Revit.Master.DockablePanes
{
    public class DockablePaneProvider : IDockablePaneProvider
    {
        public static DockablePaneId Id = new DockablePaneId(new Guid("A7C0B2A9-D8F8-4A5F-B9F6-F9F9F9F9F9F9"));

        public void SetupDockablePane(DockablePaneProviderData data)
        {
            #region Default Mode 默认上下左右
            var dockablePaneState = new DockablePaneState();
            dockablePaneState.DockPosition = DockPosition.Right;
            #endregion


            #region Floating Mode 设置浮动的位置
            //var dockablePaneState = new DockablePaneState();
            //dockablePaneState.DockPosition = DockPosition.Floating;
            //dockablePaneState.SetFloatingRectangle(new Autodesk.Revit.DB.Rectangle(100,100, 500, 500));
            #endregion

            #region Tabbed Mode  合并到项目浏览器或者属性
            //var dockablePaneState = new DockablePaneState();
            //dockablePaneState.DockPosition = DockPosition.Tabbed;
            //dockablePaneState.TabBehind = Autodesk.Revit.UI.DockablePanes.BuiltInDockablePanes.ProjectBrowser;
            #endregion

            //需要创建一个停靠面板
            data.FrameworkElementCreator = new FrameworkElementCreator();
            //设置面板的初始化位置
            data.InitialState = dockablePaneState;
            //第一次加载插件的时候，是否展示
            data.VisibleByDefault = true;
        }
    }
}
