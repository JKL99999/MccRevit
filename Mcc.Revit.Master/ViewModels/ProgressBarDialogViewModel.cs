using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mcc.Revit.Master.ViewModels
{
    public class ProgressBarDialogViewModel:ViewModelBase
    {
        public ProgressBarDialogViewModel()
        {

            MessengerInstance.Register<int>(this, Contacts.Tokens.ProgressBarMaximum, (max) =>{
                Maximum=max;
            });

            MessengerInstance.Register<string>(this, Contacts.Tokens.ProgressBarTitle, (title) =>
            {
                //事务里面创建UI无法使用多线程
                //释放资源，让UI线程在当前代码上下文中切换去处理其它Windows消息，避免界面的假死
                //DoEvents使得单线程实现类似多线程的效果，带来好的用户体验，但是会成倍降低运行效率
                System.Windows.Forms.Application.DoEvents();
                Value++;
                Title = $"{Value}/{Maximum}-{title}";
            });
                
        }



        private int _maximum;

        public int Maximum
        {
            get { return _maximum; }
            set { _maximum = value;
                RaisePropertyChanged();
            }
        }

        private int _value;

        public int Value
        {
            get { return _value; }
            set { _value = value; 
                RaisePropertyChanged(); }
        }

        private string _title;

        public string Title
        {
            get { return _title; }
            set { _title = value; 
                RaisePropertyChanged(); }
        }



    }
}
