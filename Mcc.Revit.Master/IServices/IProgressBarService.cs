using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mcc.Revit.Master.IServices
{
    public interface IProgressBarService
    {
        void Start(int maximum);
        void Stop();
    }
}
