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
using Mcc.Revit.Master.IServices;

namespace Mcc.Revit.Master.ViewModels
{
    public class MaterialDialogViewModel:ViewModelBase
    {

        //private NotificationMessageAction<MaterialDTO> _message;
        public MaterialDTO Material { get; set; }

        private readonly IMaterialService _service;
        
        public MaterialDialogViewModel(IMaterialService service)
        {
            this._service = service;
        }

        public void Initial(object Sender)
        {
            //sender为null的时候是创建材质，自然不需要初始化
            if (Sender != null)
            {
                // 将从父窗口传递进来的material赋值给缓冲数据,进而弹出子窗口将缓冲数据展示出来
                Material = (MaterialDTO)Sender;
                Name = Material.Name;
                Color = Material.Color;
                AppearanceColor = Material.AppearanceColor;
            }
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
            // 只有创建材质的时候这里才会执行
            if (Material == null)
            {
                Material = this._service.CreateElement(Name);
            }
            // 只有在提交的时候才会修改真正的材质的颜色
            // 避免了直接双向绑定真实数据的时候，还没提交，UI层已经刷新
            if (Material.Name != Name)
            {
                //这里修改的就是父窗口传递过来的选择的材质
                //里面已经执行了事务
                Material.Name = Name;
            }
            if (Material.Color != Color)
            {
                Material.Color = Color;
            }
            if (Material.AppearanceColor != AppearanceColor)
            {
                Material.AppearanceColor = AppearanceColor;
            }
            //另外一种创建材质的方法是在子VM创建完材质后直接发送給父VM
            //MessengerInstance.Send(Material,"InsertMaterial");
            // 将新建的Material再发送给消息的发出方，发出方接收到值后会立即执行回调函数，使得创建的材质显示出来
            //_message.Execute(Material);
            //VM层发送消息到View层。message:bool:true
            //这里只是正常的关闭窗口，父窗口的后台监测到子窗口关闭后且返回值为true后，执行Execute，将Material发送给回调函数
            MessengerInstance.Send(true, Tokens.CloseDialogWindow);
        }
    }
}
