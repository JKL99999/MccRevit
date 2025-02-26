using Autodesk.Revit.UI;
using Mcc.Revit.Master.Commands;
using Mcc.Revit.Toolkit.Extension;
using Mcc.Revit.Toolkit.Mvvm.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mcc.Revit.Master
{
    public class AppUIManager : IApplicationUI
    {
        //将UIProvider存储，提供给后面使用
        private readonly IUIProvider _uiProvider;
        public AppUIManager(IUIProvider uIProvider)
        {
            this._uiProvider = uIProvider;
        }

        public Result Initial()
        {
            const string _tab = "宝冶大师";
            //Tab -> Panel ->Button
            UIControlledApplication application = _uiProvider.GetUIApplication();

            application.CreateRibbonTab(_tab);
            RibbonPanel panel = application.CreateRibbonPanel(_tab, "资源");
            RibbonPanel testPanel = application.CreateRibbonPanel(_tab, "模块测试");
            RibbonPanel dockablePanel = application.CreateRibbonPanel(_tab, "可停靠窗口");
            RibbonPanel xProPanel = application.CreateRibbonPanel(_tab, "XPro");
            RibbonPanel toWebPanel = application.CreateRibbonPanel(_tab, "联系我");

            panel.CreateButton<MaterialCommand>(
               (b) => {
                   b.Text = "材质管理";
                   b.ToolTip = "材质CRUD";
                   b.LargeImage = Properties.Resources.materialBig.ConvertToBitmapSource();
                   b.Image = Properties.Resources.material.ConvertToBitmapSource();
               }
            );

          

            testPanel.CreateButton<Commands.Test.DocumentCommand>(
            (b) => {
                b.Text = "功能测试";
                b.ToolTip = "DocumentAndIOC";
                b.LargeImage = Properties.Resources.testBig.ConvertToBitmapSource();
                b.Image = Properties.Resources.test.ConvertToBitmapSource();
            }
            );

            dockablePanel.CreateButton<Commands.DockablePaneCommand>((b) => {
                b.Text = "生成文字";
                b.ToolTip = "输入文字，生成文字";
                b.LargeImage = Properties.Resources.dragonBig.ConvertToBitmapSource();
                b.Image = Properties.Resources.dragon.ConvertToBitmapSource();
            });
            //UIControlledApplication注册可停靠窗口
            application.RegisterDockablePane(DockablePanes.DockablePaneProvider.Id, "DockablePane", new DockablePanes.DockablePaneProvider());

            xProPanel.CreateButton<Commands.SurfingCommand>((b) =>
            {
                b.Text = "XPro";
                b.ToolTip = "做一些有趣的功能";
                b.LargeImage = Properties.Resources.bilibili_32.ConvertToBitmapSource();
                b.Image = Properties.Resources.bilibili_16.ConvertToBitmapSource();
            });

            toWebPanel.CreateButton<Commands.HelpCommand>(
            (b) => {
                b.Text = "技术支持";
                b.ToolTip = "是初一不是十五";
                b.LargeImage = Properties.Resources.toWebBig.ConvertToBitmapSource();
                b.Image = Properties.Resources.toWeb.ConvertToBitmapSource();
            }
            );
            toWebPanel.CreateButton<Commands.SponsorMeCommand>((b) =>
            {
             
                b.Text = "赞助";
                b.LargeImage = Properties.Resources.sponsor_32.ConvertToBitmapSource();
                b.Image = Properties.Resources.sponsor_16.ConvertToBitmapSource();
                b.ToolTip = "感谢您的赞助";
            });
            return Result.Succeeded;
        }
    }
}
