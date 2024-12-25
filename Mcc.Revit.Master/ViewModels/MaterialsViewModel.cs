using Autodesk.Revit.DB;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Mcc.Revit.Entity;
using Mcc.Revit.Master.Contacts;
using Mcc.Revit.Master.IServices;
using Mcc.Revit.Master.Views;
using Mcc.Revit.Toolkit.Extension;
using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Media.Media3D;
using Material = Autodesk.Revit.DB.Material;

namespace Mcc.Revit.Master.ViewModels
{
    public class MaterialsViewModel:ViewModelBase
    {
        private readonly IMaterialService _service;
        public MaterialsViewModel(IMaterialService service)
        {
            this._service = service;
            QueryElements();

        }
        //搜索框里面的text
        private string _keyword;
        public string Keyword
        {
            get { return _keyword; }
            //当前端View层修改了keyword后，会自动执行set
            //通过RaiseCanExecuteChanged告诉后端执行_queryElementsCommand实例
            set { _keyword = value;}
        }

        private ObservableCollection<MaterialDTO> materials;
        public ObservableCollection<MaterialDTO> Materials { 
            get { return materials; }
            set { materials = value;
                RaisePropertyChanged();
            }
        }


        //编辑材质
        public RelayCommand<MaterialDTO> EditMaterialCommand {
            get => new RelayCommand<MaterialDTO>(EditMaterial);
        }

        private void EditMaterial(MaterialDTO material)
        {
            //MessengerInstance.Send(material, Tokens.ShowMaterialDialog);
            MessengerInstance.Send(new NotificationMessageAction<MaterialDTO>(material, 
                new MaterialDialogViewModel(this._service), 
                "Edit", (e) => { }), 
                Tokens.ShowMaterialDialog);
        }

        //创建材质

        public RelayCommand CreateElementCommand {
            get => new RelayCommand(CreateElement);
        }

        private void CreateElement()
        {
            MessengerInstance.Send(new NotificationMessageAction<MaterialDTO>(null, new MaterialDialogViewModel(this._service),"Create", (e) => { 
                if (e != null)
                {
                    Materials.Insert(0, e);
                }
            }), Tokens.ShowMaterialDialog);
        }

        //提交材质事务，才会最终保存材质
        public RelayCommand SubmitCommand
        {
            get => new RelayCommand(Submit);
        }

        private void Submit()
        {
            //Messenger.Default.Send(true, Tokens.MaterialsDialog);
            MessengerInstance.Send(true, Tokens.CloseWindow);
        }

        public RelayCommand QueryElementsCommand {
            get => new RelayCommand(QueryElements);
        }

        private void QueryElements()
        {
            Materials = new ObservableCollection<MaterialDTO>(_service.GetElements(e => string.IsNullOrEmpty(Keyword) || e.Name.Contains(Keyword)));
        }

        //private RelayCommand<IList> _deleteElementCommand;

        public RelayCommand<IList> DeleteElementCommand {
            get => new RelayCommand<IList>(DeleteRevitElements);
        }

        private void DeleteRevitElements(IList item)
        {
            _service.DeleteElements(item.Cast<MaterialDTO>());
            QueryElements();
        }
    }
}
