using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Mcc.Revit.Master.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Mcc.Revit.Master.Commands
{
    [Transaction(TransactionMode.Manual)]
    public class MaterialsCommand : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {

            //MessageBox.Show("Hello,World!") ;
            UIDocument uIDocument = commandData.Application.ActiveUIDocument;
            Document document = uIDocument.Document;
            Materials materials = new Materials(document);

            TransactionStatus status;
            using(TransactionGroup group = new TransactionGroup(document,"资源管理"))
            {
                group.Start();
                if (materials.ShowDialog().Value)
                {
                    status = group.Assimilate();
                }
                else { 
                    status = group.RollBack();
                }
            }

            if (status == TransactionStatus.Committed)
            {
                return Result.Succeeded;
            }
            else {
                return Result.Cancelled;
            }
            
        }
    }
}
