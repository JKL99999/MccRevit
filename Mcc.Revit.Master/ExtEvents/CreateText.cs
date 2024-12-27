using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Mcc.Revit.Master.Cache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mcc.Revit.Master.ExtEvents
{
    public class CreateText : IExternalEventHandler
    {
        public void Execute(UIApplication app)
        {
            UIDocument uIDocument = app.ActiveUIDocument;
            Document document = uIDocument.Document;

            string text=SysCache.Instance.Text;
            if (!string.IsNullOrEmpty(text))
            {
                using (Transaction tr = new Transaction(document))
                {
                    tr.Start("创建文字");
                    ElementId typeId = document.GetDefaultElementTypeId(ElementTypeGroup.TextNoteType);
                    TextNote note = TextNote.Create(document, uIDocument.ActiveGraphicalView.Id, XYZ.Zero, text, typeId);
                    tr.Commit();
                }
            }
        }

        public string GetName()
        {
            return "CreateText";
        }
    }
}
