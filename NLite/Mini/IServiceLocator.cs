using System;
using System.Collections.Generic;
using NLite.Internal;

namespace NLite
{
    /// <summary>
    /// ����λ���ӿ�
    /// </summary>
    public interface IServiceLocator : IServiceProvider,IDisposable
    {
        
        /// <summary>
        /// �õ�����
        /// </summary>
        /// <param name="id">����Id</param>
        /// <returns>���ط���ʵ��</returns>
        object Get(string id);

        /// <summary>
        /// �õ�����
        /// </summary>
        /// <param name="serviceType">��������</param>
        /// <returns>���ط���ʵ��</returns>
        object Get(Type serviceType);

        /// <summary>
        /// �õ�����
        /// </summary>
        /// <param name="id">����Id</param>
        /// <returns>���ط���ʵ��</returns>
        object Get(string id, IDictionary<string, object> args);

        /// <summary>
        /// �õ�����
        /// </summary>
        /// <param name="service">��������</param>
        /// <returns>���ط���ʵ��</returns>
        object Get(Type service, IDictionary<string, object> args);

        /// <summary>
        /// �õ�����
        /// </summary>
        /// <param name="id"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        object Get(string id, params object[] args);


        /// <summary>
        /// �õ�����
        /// </summary>
        /// <param name="serviceType"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        object Get(Type serviceType, params object[] args);

        /// <summary>
        /// �õ����з���
        /// </summary>
        /// <param name="service"></param>
        /// <returns></returns>
        IEnumerable<object> GetAll(Type service);

        /// <summary>
        /// �õ����з���
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IEnumerable<T> GetAll<T>();
    }


    /// <summary>
    /// ��������֪ͨ�ӿ�
    /// </summary>
    public interface IServiceReinjectedNotification
    {
        /// <summary>
        /// 
        /// </summary>
        void OnReinjected();
    }
 
    /// <summary>
    /// ����λ����չ��
    /// </summary>
    public static class ServiceLocatorExtensions
    {

        /// <summary>
        /// ͨ�����Id�õ�ָ�������
        /// </summary>
        /// <typeparam name="TComponent">�������</typeparam>
        /// <param name="id">���Id</param>
        /// <returns>�������ʵ��</returns>
        public static TComponent Get<TComponent>(this IServiceLocator locator, string id)
        {
            if (locator == null)
                throw new ArgumentNullException("locator");
            Guard.NotNullOrEmpty(id, "id");
            return (TComponent)locator.Get(id);
        }
       
    }
      
}

namespace NLite
{
    /// <summary>
    /// �����ṩ����չ��
    /// </summary>
    public static class ServiceProviderExtensions
    {
        /// <summary>
        /// ͨ����Լ���͵õ����ʵ��
        /// </summary>
        /// <typeparam name="TContract">��Լ����</typeparam>
        /// <returns>�������ʵ��</returns>
        public static TContract Get<TContract>(this IServiceLocator locator)
        {
            if (locator == null)
                throw new ArgumentNullException("locator");
            return (TContract)locator.Get(typeof(TContract));
        }
        
        /// <summary>
        /// ͨ����Լ���͵õ�ָ�������ʵ��
        /// </summary>
        /// <typeparam name="TContract"></typeparam>
        /// <typeparam name="TComponent"></typeparam>
        /// <param name="locator"></param>
        /// <returns></returns>
        public static TComponent Get<TContract, TComponent>(this IServiceLocator locator) where TComponent : TContract
        {
            if (locator == null)
                throw new ArgumentNullException("locator");
            return (TComponent)locator.GetService(typeof(TContract));
        }

    }

}
