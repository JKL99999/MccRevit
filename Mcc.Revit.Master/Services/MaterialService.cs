using Mcc.Revit.Entity;
using Mcc.Revit.Master.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mcc.Revit.Master.Services
{
    public class MaterialService : IMaterialService
    {
        public MaterialDTO CreateElement(string name)
        {
            throw new NotImplementedException();
        }

        public void DeleteElement(MaterialDTO element)
        {
            throw new NotImplementedException();
        }

        public void DeleteElements(IEnumerable<MaterialDTO> elements)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<MaterialDTO> GetElements(Func<MaterialDTO, bool> predicate = null)
        {
            throw new NotImplementedException();
        }
    }
}
