using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;


namespace Mcc.Revit.Toolkit.UI
{
    //1.自定义Button的样式
    //2.通过依赖属性来添加

    public class IconButton:Button
    {
        static IconButton()
        {   //DefaultStyleKey是用于查找控件样式的键，没有这句代码控件就找不到默认样式
            DefaultStyleKeyProperty.OverrideMetadata(typeof(IconButton), new FrameworkPropertyMetadata(typeof(IconButton)));
        }
        //给自定义的控件添加自定义的属性（依赖属性）propdb+Tab Tab
        //依赖属性标识符的名称必须为“属性名+Property"
        //参数说明：1.被包装的CLR属性名2.CLR属性的类型3.将属性应用到哪种类型4.设置默认值/设置回调函数

        public static readonly DependencyProperty IntervalProperty = DependencyProperty.Register("Interval", typeof(Thickness), typeof(IconButton), new PropertyMetadata(new Thickness(0.0, 0.0, 0.0, 0.0)));
        public static readonly DependencyProperty IconFontFamilyProperty = DependencyProperty.Register("IconFontFamily", typeof(FontFamily), typeof(IconButton), new PropertyMetadata(Fonts.SystemFontFamilies.FirstOrDefault<FontFamily>()));
        public static readonly DependencyProperty IconProperty = DependencyProperty.Register("Icon", typeof(string), typeof(IconButton), new PropertyMetadata(string.Empty));
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(IconButton), new PropertyMetadata(string.Empty));
        //实现属性包装器。在Getter和Setter中分别调用GetValue和SetValue，除此之外Getter和Setter中不应该有其它任何自定义代码。
        public Thickness Interval
        {
            get
            {
                //没有override，加不加base都一样，父子类实例指向的是一个
                return (Thickness)base.GetValue(IconButton.IntervalProperty);
            }
            set
            {
                base.SetValue(IconButton.IntervalProperty, value);
            }
        }
        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }
        //Icon是阿里图标库的图标的编码
        public string Icon
        {
            get => (string)GetValue(IconProperty);
            set => SetValue(IconProperty, value);
        }
        public FontFamily IconFontFamily
        {
            get => (FontFamily)GetValue(IconFontFamilyProperty);
            set => SetValue(IconFontFamilyProperty, value);
        }
        //重载button控件的OnContentChanged方法
        protected override void OnContentChanged(object oldContent, object newContent)
        {
            if (newContent != null)
            {
                this.Interval = new Thickness(5.0, 0.0, 0.0, 0.0);
            }
            base.OnContentChanged(oldContent, newContent);
        }



    }
}
