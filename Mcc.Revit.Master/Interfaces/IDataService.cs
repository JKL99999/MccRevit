using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mcc.Revit.Master.Interfaces
{
    public interface IDataService<TElement>
    {
        //类名，方法名，接口名，属性名首字母大写，参数名小写
        //方法的参数是个返回值为True的委托，默认为null
        IEnumerable<TElement> GetElements(Func<TElement, bool> predicate = null);
        void DeleteElement(TElement element);
        //传入值当然是一个可迭代的泛型列表
        void DeleteElements(IEnumerable<TElement> elements);
        TElement CreateElement(string name);
    }
}
