using System;
using System.Linq;
using NLite.Reflection;
namespace NLite
{
    /// <summary>
    /// ��ʼ���ӿ�
    /// </summary>
    public interface IInitializable
    {
        /// <summary>
        /// ��ʼ��
        /// </summary>
        void Init();
    }

    /// <summary>
    /// ��ʼ����
    /// </summary>
    //[Contract]
    public abstract class Initializer : IInitializable
    {
        /// <summary>
        /// ������ʶһ��������ִ�е�˳��
        /// </summary>
        [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
        protected class OrderAttribute : Attribute
        {
            /// <summary>
            /// ���
            /// </summary>
            public readonly uint Order;
            /// <summary>
            /// 
            /// </summary>
            /// <param name="order">����ִ�е�˳��</param>
            public OrderAttribute(uint order)
            {
                Order = order;
            }
        }

        static Func[] Actions;

        /// <summary>
        /// �����ʼ����
        /// </summary>
        protected Initializer()
        {
            if (Actions == null)
                Actions = (from m in GetType().GetMethods(MemberFlags.InstanceFlags)
                           let attr = m.GetAttribute<OrderAttribute>(true)
                           let length = m.GetParameters().Length
                           where attr != null && length == 0
                           orderby attr.Order
                           select m.GetFunc()).ToArray();
        }

        /// <summary>
        /// �ڳ�ʼ��ǰ����
        /// </summary>
        protected virtual void OnInitializing() { }
        /// <summary>
        /// �ڳ�ʼ���󴥷�
        /// </summary>
        protected virtual void OnInitialized() { }
        /// <summary>
        /// �쳣����ʱ����
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnExceptionFired(Exception e) { throw e.Handle(); }

        void IInitializable.Init()
        {
            try
            {
                OnInitializing();
                foreach (var action in Actions)
                    action(this, null);
                OnInitialized();
            }
            catch (Exception e)
            {
                OnExceptionFired(e);
            }
        }
        
    }

    /// <summary>
    /// �����ʼ����
    /// </summary>
    [Component]
    public abstract class AbstractInitializer : Initializer { }
}
