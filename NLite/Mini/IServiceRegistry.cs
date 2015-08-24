using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;
using NLite.Reflection;
using System.IO;
using NLite.Mini.Fluent;
using NLite.Internal;

namespace NLite
{
    /// <summary>
    /// ����ע���ӿ�
    /// </summary>
    public interface IServiceRegistry
    {
        /// <summary>
        /// ͨ�����Ԫ����ע�������������
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        IServiceRegistry Register(IComponentInfo info);

        /// <summary>
        /// �����ʵ�������������������ͨ�������Զ�ע�����
        /// </summary>
        /// <param name="componentInstance"></param>
        /// <returns></returns>
        IServiceRegistry Compose(object componentInstance);

        /// <summary>
        /// ע��ʵ��
        /// </summary>
        /// <param name="id">ʵ��Id</param>
        /// <param name="instance">ʵ��</param>
        IServiceRegistry RegisterInstance(string id, object instance);

        /// <summary>
        /// ע��ʵ��
        /// </summary>
        /// <param name="id">ʵ��Id</param>
        /// <param name="contract">����</param>
        /// <param name="instance">ʵ��</param>
        IServiceRegistry RegisterInstance(string id, Type contract, object instance);

        /// <summary>
        /// �Ƿ�ע����ʵ�ָ�����Լ�ӿڵ����
        /// </summary>
        /// <param name="contract"></param>
        /// <returns></returns>
        bool HasRegister(Type contract);

        /// <summary>
        /// �Ƿ�ע���˺������Id�����
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool HasRegister(string id);

        /// <summary>
        /// ͨ�����Idע�����
        /// </summary>
        /// <param name="id">���Id</param>
        void UnRegister(string id);

        /// <summary>
        ///ͨ����Լ����ע����Ӧ�����
        /// </summary>
        void UnRegister(Type contract);

        /// <summary>
        /// �õ�����������
        /// </summary>
        IComponentListenerManager ListenerManager { get; }
    }

    /// <summary>
    /// ����ע�������
    /// </summary>
    public static class ServiceRegistry
    {
        const string key = "NLite_ServiceLocator";
        static IServiceRegistry _current;
       
        /// <summary>
        /// ���ص�ǰ��ķ���ע���
        /// </summary>
        public static IServiceRegistry Current
        {
            get
            {
                var current = NLiteEnvironment.IsWeb ? NLiteEnvironment.Application[key] as IServiceRegistry : _current;
                return current;
            }
            set
            {
                  _current = value;
                if (NLiteEnvironment.IsWeb)
                    NLiteEnvironment.Application[key] = value;
            }
        }

        /// <summary>
        /// �Ƿ�ע����ʵ�ָ�����Լ�ӿڵ����
        /// </summary>
        /// <typeparam name="TContract"></typeparam>
        /// <returns></returns>
        public static bool HasRegister<TContract>()
        {
            return Current.HasRegister(typeof(TContract));
        }

        /// <summary>
        /// �Ƿ�ע����ʵ�ָ�����Լ�ӿڵ����
        /// </summary>
        /// <param name="service"></param>
        /// <returns></returns>
        public static bool HasRegister(Type service)
        {
            Guard.NotNull(service, "service");
            return Current.HasRegister(service);
        }

        /// <summary>
        /// �Ƿ�ע���˺������Id�����
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool HasRegister(string id)
        {
            Guard.NotNullOrEmpty(id, "id");

            return Current.HasRegister(id);
        }

        /// <summary>
        /// ͨ���������ע�����
        /// </summary>
        /// <typeparam name="TComponent"></typeparam>
        /// <returns></returns>
        public static IServiceRegistry Register<TComponent>()
        {
            return Current.Register<TComponent>();
        }

        /// <summary>
        /// ͨ��������ͺͺ����Id��ע�����
        /// </summary>
        /// <typeparam name="TComponent"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public static IServiceRegistry Register<TComponent>(string id)
        {
            Guard.NotNullOrEmpty(id, "id");
            return Current.Register<TComponent>(id);
        }

        /// <summary>
        /// ͨ������Api�ӿ���ע�����
        /// </summary>
        /// <param name="handler"></param>
        /// <returns></returns>
        public static IServiceRegistry Register(Action<IComponentExpression> handler)
        {
            Guard.NotNull(handler, "handler");
            return Current.Register(handler);
        }

        ///// <summary>
        ///// ͨ������Api�ӿ���ע�����
        ///// </summary>
        ///// <param name="handlers"></param>
        ///// <returns></returns>
        //public static IServiceRegistry Register(params Action<IComponentExpression>[] handlers)
        //{
        //    return Current.Register(handlers);
        //}

        /// <summary>
        /// ע��ָ����������Assembly�µ����б��Ϊ"ComponentAttribue"�����
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IServiceRegistry RegisteryFromAssemblyOf<T>()
        {
            return Current.RegisterFromAssemblyOf<T>();
        }

        /// <summary>
        /// �����ʵ�������������������ͨ�������Զ�ע�����
        /// </summary>
        /// <param name="component"></param>
        /// <returns></returns>
        public static IServiceRegistry Compose(object component)
        {
            Guard.NotNull(component, "component");
            return Current.Compose(component);
        }
    }
}
