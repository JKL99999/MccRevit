using Autodesk.Revit.UI;
using Mcc.Revit.Toolkit.Mvvm.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Mcc.Revit.Toolkit.Mvvm
{
    public class MouseHookProvider
    {

        //判断是否已经注册的标识
        public static bool isHookSet = false;
        //定义了鼠标低级钩子的类型编号，14
        private const int WH_MOUSE_LL = 14;
        //定义了鼠标中键按下的消息编号，0x0207
        private const int WM_MBUTTONDOWN = 0x0207;
        // 一个LowLevelMouseProc代理，指向HookCallback回调函数(监听到事件后的处理函数)
        private static LowLevelMouseProc _proc = HookCallback;
        //存储钩子的句柄，在设置钩子时获得
        private static IntPtr _hookID = IntPtr.Zero;
        //以下属性是用来判断双击的
        private static Stopwatch stopwatch = new Stopwatch(); // 用于计时点击间隔
        private const int DoubleClickTime = 300; // 双击的最大间隔时间（毫秒）
        private static int clickCount = 0; // 点击次数
                                           //【1】调用SetWindowsHookEx方法安装钩子
        public static void SetHook()
        {
            // WH_MOUSE_LL表示安装一个鼠标低级钩子;_proc是回调函数;
            // IntPtr.Zero表示钩子过程与所有线程关联; 0表示钩子过程与所有GUI线程关联
            _hookID = SetWindowsHookEx(WH_MOUSE_LL, _proc, IntPtr.Zero, 0);
            MouseHookProvider.isHookSet = true;
        }
        //【2】调用UnhookWindowsHookEx取消钩子的注册
        public static void ReleaseHook()
        {

            UnhookWindowsHookEx(_hookID);
            MouseHookProvider.isHookSet = false;

        }
        // 【3】定义事件，方便在外部定义触发键盘监控后执行事件
        public static event EventHandler MiddleMouseDoubleClicked;
        private delegate IntPtr LowLevelMouseProc(int nCode, IntPtr wParam, IntPtr lParam);
        private static IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0 && wParam == (IntPtr)WM_MBUTTONDOWN)
            {
                // 检测到鼠标中键点击
                clickCount++;
                if (clickCount == 1)
                {
                    // 如果是第一次点击，启动计时器
                    stopwatch.Restart();
                }
                else if (clickCount == 2)
                {
                    // 如果是第二次点击，检查时间间隔
                    if (stopwatch.ElapsedMilliseconds <= DoubleClickTime)
                    {
                        // 【4】在此处触发您的处理逻辑,
                        // 只有在Revit激活后的窗口中触发双击才会执行
                        IDataContext dataContext = ExternalApplicationBase.ContainerProvider.GetInstance<IDataContext>();
                        UIApplication uiApp = dataContext.GetUIApplication();
                        //先判断当前窗口是否是ReVIT激活的窗口,通过UIApplication对象获取主窗口
                        IntPtr mainRevitWindow = uiApp.MainWindowHandle;
                        IntPtr activeWindow = GetForegroundWindow();
                        if (activeWindow == mainRevitWindow)
                        {
                            //RevitCommandId cmdId = RevitCommandId.LookupPostableCommandId(PostableCommand.ModelLine);
                            //uiApp.PostCommand(cmdId);
                            MiddleMouseDoubleClicked?.Invoke(null, EventArgs.Empty);
                        }
                        // 重置计数器和计时器
                        clickCount = 0;
                        stopwatch.Reset();
                    }
                    else
                    {
                        // 如果第二次点击超出了时间限制，重置计数器，重新开始计时
                        clickCount = 1;
                        stopwatch.Restart();
                    }
                }
            }
            return CallNextHookEx(_hookID, nCode, wParam, lParam);
        }
        //使用DllImport属性声明的外部函数
        //使用此功能，安装了一个钩子
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelMouseProc lpfn, IntPtr hMod, uint dwThreadId);
        //调用此函数卸载钩子
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);
        //使用此功能，通过信息钩子继续下一个钩子
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);
        // 取得当前线程编号（线程钩子需要用到）
        [DllImport("kernel32.dll")]
        static extern int GetCurrentThreadId();
        //使用WINDOWS API函数代替获取当前实例的函数,防止钩子失效
        [DllImport("kernel32.dll")]
        public static extern IntPtr GetModuleHandle(string name);
        //获取当前激活的窗口句柄
        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();
    }
}
