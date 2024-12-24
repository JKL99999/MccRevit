using Autodesk.Revit.DB;
using Mcc.Revit.Entity;
using Mcc.Revit.Master.IServices;
using Mcc.Revit.Toolkit.Extension;
using Mcc.Revit.Toolkit.Mvvm.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Mcc.Revit.Master.Services
{
    public class MaterialService : IMaterialService
    {
        private readonly IDataContext _dataContext;
        public MaterialService(IDataContext dataContext)
        {
            _dataContext = dataContext;
        }

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
            _dataContext.GetDocument().NewTransaction("删除多个材质",
                () => {
                    foreach (MaterialDTO element in elements) {
                        _dataContext.GetDocument().Delete(element.Material.Id);
                        _dataContext.GetDocument().Regenerate();
                    } 
                }
                );
        }

        public IEnumerable<MaterialDTO> GetElements(Func<MaterialDTO, bool> predicate = null)
        {
            FilteredElementCollector elements = new FilteredElementCollector(_dataContext.GetDocument())
                .OfClass(typeof(Material));
            IEnumerable<MaterialDTO> materialDTOs = elements.ToList()
                .ConvertAll(x => new MaterialDTO(x as Material));
            if (predicate != null)
            {
                materialDTOs = materialDTOs.Where(x => predicate(x));
            }
            return materialDTOs;
        }
    }
}
