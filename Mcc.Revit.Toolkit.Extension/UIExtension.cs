using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows;
using Autodesk.Revit.UI;

namespace Mcc.Revit.Toolkit.Extension
{
    public static class UIExtension
    {
        // T就是与按钮关联的执行实际按钮命令的class。 
        // Action<T>是系统定义的一个无返回值的泛型委托
        // 静态类的静态扩展方法才能扩展，this用来修饰第一个参数，第一个参数的类型就是扩展的类型
        // 为RibbonPanel类型定义了一个扩展方法，返回值也是RibbonPanel。调用时就像实例上的方法一样：panel.CreateButton<T>()
        public static RibbonPanel CreateButton<T>(this RibbonPanel panel, Action<PushButtonData> action)
        {
            // 此处的T是点击按钮实际执行命令的类，实现了IExternalCommand接口
            Type type = typeof(T);
            string name = $"btn_{type.Name}";
            PushButtonData pushButtonData = new PushButtonData(name, name, type.Assembly.Location, type.FullName);
            // 从线程池中调用委托执行；Invoke是同步的。当然直接调用委托也行 action()
            action.Invoke(pushButtonData);
            // button添加到面板上
            panel.AddItem(pushButtonData);
            return panel;
        }
        // revit的命令图标需要的是BitmapSource,我们的resource里面是Bitmap，所以需要转换一下
        // 这里同样是使用了扩展方法
        public static BitmapSource ConvertToBitmapSource(this System.Drawing.Bitmap bitmap)
        {
            return System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
                bitmap.GetHbitmap(),
                IntPtr.Zero,
                Int32Rect.Empty,
                BitmapSizeOptions.FromEmptyOptions());
        }
    }
}
