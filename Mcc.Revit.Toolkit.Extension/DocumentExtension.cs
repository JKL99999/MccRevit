using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mcc.Revit.Toolkit.Extension
{
    public static class DocumentExtension
    {
        public static void NewTransaction(this Document document,string name,Action action) { 
        
            using(Transaction ts =  new Transaction(document,name))
            {
                ts.Start();
                action?.Invoke();
                ts.Commit();
            }
        }
    }
}
