using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using GalaSoft.MvvmLight.Ioc;
using Mcc.Revit.Toolkit.Extension;
using Mcc.Revit.Toolkit.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using UIFramework;

namespace Mcc.Revit.Master.Commands.Test
{
    [Transaction(TransactionMode.Manual)]
    public class DocumentCommand : ExternalCommandBase
    {

        public override void RegisterTypes(SimpleIoc simpleIoc)
        {
            //MessageBox.Show(simpleIoc.GetHashCode().ToString());
            //MessageBox.Show(simpleIoc.IsRegistered<UIProvider>().ToString()); 
            //MessageBox.Show(simpleIoc.IsRegistered<DataContext>().ToString());
            //MessageBox.Show(simpleIoc.IsRegistered<Document>().ToString());
        }

        public override Window CreateMainWindow()
        {
            //// 创建你的主窗体
            //var mainWindow = new MainWindow();
            //// 为主窗体绑定 ViewModel
            //mainWindow.DataContext = ContainerProvider.GetInstance<MyViewModel>();
            //return mainWindow;
            return null;
        }

        public override Result Execute(ref string message, ElementSet elements)
        {

            var doc =DataContext.GetDocument();
            var uiDoc = DataContext.GetUIDocument();
            var application =  DataContext.GetUIApplication();
            var activeView = uiDoc.ActiveView;
            TaskDialog.Show("Active View", $"当前视图名：{activeView.Id}");
            Selection selection = uiDoc.Selection;
            var selectedIds = selection.GetElementIds();

            doc.NewTransaction("test", () => {
                foreach (ElementId id in selectedIds)
                {
                    Element element = doc.GetElement(id);
                    doc.Delete(id);
                }
            });
            
            return Result.Succeeded;
        }
    }
}
