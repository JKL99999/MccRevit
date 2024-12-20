using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Visual;
using GalaSoft.MvvmLight;
using Mcc.Revit.Toolkit.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mcc.Revit.Entity
{
    public class MaterialDTO:ObservableObject
    {
        public Material Material { get; private set; }
        public Document Document { get => Material.Document; }
        public ElementId Id { get => Material.Id; }

        private string name;

        public string Name
        {
            get { return name; }
            set { name = value;
                Document.NewTransaction("修改名称", () => Material.Name = value);
                RaisePropertyChanged();
            }
        }

        private Color color;

        public Color Color
        {
            get { return color; }
            set { color = value;
                Document.NewTransaction("修改颜色", () => Material.Color = value);
                RaisePropertyChanged();
            }
        }

        private Color appearanceColor;

        public Color AppearanceColor
        {
            get { return appearanceColor; }
            set { appearanceColor = value;
                RaisePropertyChanged();
            }
        }




        public MaterialDTO(Material material)
        {
            Material = material;
            name = material.Name;
            color = material.Color;
            AppearanceColor = GetAppearanceColor();
        }

        private Color GetAppearanceColor()
        {
            ElementId id = Material.AppearanceAssetId;
            // 判断是否是有效材质ID
            if (id != null && id.IntegerValue != -1)
            {
                //拿到外观元素
                AppearanceAssetElement appearanceAssetElement = Document.GetElement(id) as AppearanceAssetElement;
                // 拿到渲染资产
                Asset asset = appearanceAssetElement.GetRenderingAsset();
                return GetColorProperty(asset)?.GetValueAsColor();
            }
            return null;
        }

        private void SetAppearanceColor(Color color)
        {
            // 修改材质的真实模式颜色
            // 代码暂时是有问题的，修改不起作用，后面再改吧
            ElementId id = Material.AppearanceAssetId;
            if (id != null && id.IntegerValue != -1)
            {
                using (AppearanceAssetEditScope scope = new AppearanceAssetEditScope(Document))
                {
                    Asset asset = scope.Start(id);
                    GetColorProperty(asset)?.SetValueAsColor(color);
                    scope.Commit(true);
                }
            }
        }

        private AssetPropertyDoubleArray4d GetColorProperty(Asset asset)
        {
            //转换属性,这里只解析了常规类别的外观；还有其他类别的，自己做功能的时候要注意完善
            return (AssetPropertyDoubleArray4d)asset?.FindByName("generic_diffuse");
        }

        //第三种开启事务的方式，在实体类中合并多个单个的处理
        public void Save()
        {
            Document.NewTransaction("修改材质", () =>
            {
                Material.Name = Name;
                Material.Color = Color;
                
            });
        }
    }
}
