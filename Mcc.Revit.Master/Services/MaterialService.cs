﻿using Autodesk.Revit.DB;
using GalaSoft.MvvmLight.Messaging;
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
            Element element = null;
            _dataContext.GetDocument().NewTransaction("创建材质", () => {
                ElementId id = Material.Create(_dataContext.GetDocument(), name);
                element = _dataContext.GetDocument().GetElement(id);
            });
            return new MaterialDTO(element as Material);
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
                        Messenger.Default.Send<string>(element.Name, Contacts.Tokens.ProgressBarTitle);
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
