using GalaSoft.MvvmLight.Ioc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Mcc.Revit.Toolkit.Mvvm.Extensions
{
    public static class SimpleIocExtension
    {

        public static TView Resolve<TView, TViewModel>(this SimpleIoc container, bool modeless = false)
           where TView : Window where TViewModel : class
        {
            // modeless为true时候，说明是非模态ShowDialog()线程阻塞，窗口不关闭的情况下，无法用鼠标进行下一步
            // 非模态时候返回单例，模态返回瞬时
            var view = modeless ? container.GetInstance<TView>() : container.GetInstanceWithoutCaching<TView>();
            //View层绑定对应的ViewModel
            view.DataContext = container.GetInstanceWithoutCaching<TViewModel>();
            return view;
        }
    }
}
