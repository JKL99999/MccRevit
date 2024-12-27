using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mcc.Revit.Master.Cache
{
    public class SysCache
    {
        private SysCache()
        {

        }
        private static SysCache _instance;

        public static SysCache Instance
        {
            get
            {
                if (ReferenceEquals(null, _instance))
                {

                    _instance = new SysCache();
                }
                return _instance;
            }
        }

        public ExternalEvent CreateText { get; set; }  //引用加载族的外部事件
        public string Text { get; set; } //当前族文件路径
    }
}
