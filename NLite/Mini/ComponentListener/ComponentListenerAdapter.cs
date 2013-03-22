using System;
using NLite.Mini.Context;
using System.Collections.Generic;
using System.Reflection;
using NLite.Reflection.Internal;

namespace NLite.Mini.Listener
{
  
    
   
    /// <summary>
    /// �������������
    /// </summary>
    public class ComponentListenerAdapter :  IComponentListener
    {
        /// <summary>
        /// 
        /// </summary>
        public IKernel Kernel { get; private set; }

        void IComponentListener.Init(IKernel kernel)
        {
            Kernel = kernel;
            if (kernel != null)
                Init();
        }

        /// <summary>
        /// 
        /// </summary>
        protected virtual void Init() { }
        /// <summary>
        /// �����Ԫ����ע��ǰ���м���������Aop������
        /// </summary>
        /// <param name="bindingInfo"></param>
        public virtual bool OnMetadataRegistering(IComponentInfo info)
        {
            return true;
        }
        /// <summary>
        /// �����Ԫ����ע�����м���������Aop������
        /// </summary>
        /// <param name="bindingInfo"></param>
        public virtual void OnMetadataRegistered(IComponentInfo info)
        {
        }

        /// <summary>
        /// �����Ԫ����ע������м���������Aop������
        /// </summary>
        /// <param name="bindingInfo"></param>
        public virtual void OnMetadataUnregistered(IComponentInfo info)
        {
        }

        /// <summary>
        /// ���������ǰ���м���
        /// </summary>
        /// <param name="ctx"></param>
        public virtual void OnPreCreation(IComponentContext ctx)
        {
        }

        /// <summary>
        /// �������������м���
        /// </summary>
        /// <param name="ctx"></param>
        public virtual void OnPostCreation(IComponentContext ctx)
        {
        }

        /// <summary>
        /// �����������������ʼ�����м���
        /// </summary>
        /// <param name="ctx"></param>
        public virtual void OnInitialization(IComponentContext ctx)
        {
        }

        /// <summary>
        /// �������ʼ������м���
        /// </summary>
        /// <param name="ctx"></param>
        public virtual void OnPostInitialization(IComponentContext ctx)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ctx"></param>
        public virtual void OnFetch(IComponentContext ctx)
        {
        }
        /// <summary>
        /// ������ͷ�ǰ���м���
        /// </summary>
        /// <param name="bindingInfo"></param>
        /// <param name="instance"></param>
        public virtual void OnPreDestroy(IComponentInfo info, object instance)
        {
        }

        /// <summary>
        /// ������ͷź���м���
        /// </summary>
        /// <param name="bindingInfo"></param>
        public virtual void OnPostDestroy(IComponentInfo info)
        {
        }
    }
}
