using System;
using System.Collections.Generic;
using NLite.Mini.Lifestyle;
namespace NLite.Mini.Context
{
    /// <summary>
    /// Mini ���������Ľӿ�
    /// </summary>
    public interface IComponentContext
    {
        /// <summary>
        /// �õ�Mini����
        /// </summary>
        IKernel Kernel { get; }
        /// <summary>
        /// �õ����Ԫ����
        /// </summary>
        IComponentInfo Component { get; }

        ILifestyleManager LifestyleManager { get;}

        /// <summary>
        /// 
        /// </summary>
        Type[] GenericParameters { get; }

        /// <summary>
        /// 
        /// </summary>
        IDictionary<string, object> NamedArgs { get; set; }

        /// <summary>
        /// 
        /// </summary>
        object[] Args { get; set; }

        /// <summary>
        /// 
        /// </summary>
        object Instance { get; set; }
    }
}
