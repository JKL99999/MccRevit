using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mcc.Revit.Toolkit.Extension
{
    public static class TransactionExtension
    {
        //有些情况下我们需要执行事务创建一些元素获取参数再取消，因此添加了一个rollback参数
        public static void NewTransaction(this Document document, Action action, string name = "Default", bool rollback = false)
        {
            if (document == null) throw new ArgumentNullException(nameof(document));
            using (Transaction ts = new Transaction(document, name))
            {
                if (ts.Start() == TransactionStatus.Started)
                {
                    action.Invoke();
                    if (!rollback)
                    {
                        if (ts.Commit() != TransactionStatus.Committed)
                        {
                            //实际要写在日志中
                            TaskDialog.Show("Message", "Transaction Failure");
                        }
                        //不往下执行了
                        return;
                    }
                }
                ts.RollBack();
            }
        }
        //子事务
        public static void NewSubTransaction(this Document document, Action predicate, bool rollback = false)
        {
            if (document == null) throw new ArgumentNullException(nameof(document));
            using (SubTransaction sts = new SubTransaction(document))
            {
                if (sts.Start() == TransactionStatus.Started)
                {
                    predicate.Invoke();
                    if (!rollback)
                    {
                        if (sts.Commit() != TransactionStatus.Committed)
                        {
                            //实际要写在日志中
                            TaskDialog.Show("Message", "Transaction Failure");
                        }
                        //不往下执行了
                        return;
                    }
                }
                sts.RollBack();
            }
        }
        //执行Assimilate后会将事务进行打组处理
        public static void NewTransactionGroup(this Document document, Action predicate, string name = "DefaultGroup",
            bool assimilate = true, bool rollback = false)
        {
            if (document == null) throw new ArgumentNullException(nameof(document));
            using (TransactionGroup tsg = new TransactionGroup(document, name))
            {
                if (tsg.Start() == TransactionStatus.Started)
                {
                    if (!rollback)
                    {
                        predicate.Invoke();
                        TransactionStatus status = assimilate ? tsg.Assimilate() : tsg.Commit();
                        if (status != TransactionStatus.Committed)
                        {
                            TaskDialog.Show("Message", "Transaction Failure");
                        }
                        return;
                    }
                }
                tsg.RollBack();
            }
        }

        //事务组处理；带返回参数TransactionStatus 的
        //可选参数必须在所有必选参数之后
        public static TransactionStatus NewTransactionGroup(this Document document, Func<bool> func, string name = "DefaultGroup")
        {
            TransactionStatus status = TransactionStatus.Uninitialized;
            using (TransactionGroup ts = new TransactionGroup(document, name))
            {
                ts.Start();
                bool result = func.Invoke();
                //窗口正常打开，返回true，合并事务，否则回滚。
                status = result ? ts.Assimilate() : ts.RollBack();
            }
            return status;
        }

    }
}
