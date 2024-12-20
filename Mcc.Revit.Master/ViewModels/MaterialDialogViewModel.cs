using Autodesk.Revit.DB;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Mcc.Revit.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Mcc.Revit.Toolkit.Extension;
using Mcc.Revit.Master.Contacts;
using GalaSoft.MvvmLight.Messaging;
using System.Runtime.Remoting.Messaging;

namespace Mcc.Revit.Master.ViewModels
{
    public class MaterialDialogViewModel:ViewModelBase
    {

        private NotificationMessageAction<MaterialDTO> _message;
        public MaterialDTO Material { get; set; }
        public MaterialDialogViewModel(NotificationMessageAction<MaterialDTO> message)
        {
            if (message.Sender is MaterialDTO material) {
                Material = material;
                Name = material.Name;
                Color = material.Color;
                AppearanceColor = material.AppearanceColor;
            }
            _message=message;
        }

        private string name;

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                RaisePropertyChanged();
            }
        }

        private Color color;

        public Color Color
        {
            get { return color; }
            set
            {
                color = value;
                RaisePropertyChanged();
            }
        }

        private Color appearanceColor;

        public Color AppearanceColor
        {
            get { return appearanceColor; }
            set
            {
                appearanceColor = value;
                RaisePropertyChanged();
            }
        }

        


        public RelayCommand SetColorCommand {
            get => new RelayCommand(SetColor);
        }

        private void SetColor()
        {
            ColorDialog dialog = new ColorDialog();
            if (dialog.ShowDialog() == DialogResult.OK) {
                Color = dialog.Color.ConvertToRevitColor();
            }  
        }

        public RelayCommand SubmitCommand {
            get => new RelayCommand(Submit);
        }

        private void Submit()
        {
            if (_message.Notification == "Create") { 
                Document document = _message.Target as Document;
                document.NewTransaction("创建材质", () =>
                {
                    ElementId id = Autodesk.Revit.DB.Material.Create(document, Name);
                    Material = new MaterialDTO(document.GetElement(id) as Material);
                });
            }

            Material.Name = Name;
            Material.Color = Color;
            Material.AppearanceColor = AppearanceColor;

            _message.Execute(Material);
            MessengerInstance.Send(true, Tokens.CloseDialogWindow);
        }
    }
}
