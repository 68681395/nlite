using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace NLite.Collections
{
    /// <summary>
    /// ���Լ��ӿ�
    /// </summary>
    public interface IPropertySet : IEnumerable<KeyValuePair<string, object>>, IEditableObject, IChangeTracking
    {
        /// <summary>
        /// ͨ���������õ�����������ֵ
        /// </summary>
        /// <param name="propertyName">������</param>
        /// <returns></returns>
        object this[string propertyName] { get; set; }

        /// <summary>
        /// �������е�����Key
        /// </summary>
        string[] Keys { get; }

        /// <summary>
        /// ͨ���������õ�����ֵ
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="property"></param>
        /// <returns></returns>
        T Get<T>(string property);

        /// <summary>
        /// ͨ���������õ�����ֵ
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="property"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        T Get<T>(string property, T defaultValue);

        ///// <summary>
        ///// ��������ֵ
        ///// </summary>
        ///// <param name="propertyName"></param>
        ///// <param name="value"></param>
        //void Set(string propertyName, object value);

        /// <summary>
        /// �ж����Լ����Ƿ�����ض���������
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        bool Contains(string propertyName);

        /// <summary>
        /// �������Ե�����
        /// </summary>
        int Count { get; }

        /// <summary>
        /// ͨ���������Ƴ�����
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        bool Remove(string propertyName);

        /// <summary>
        /// 
        /// </summary>
        void Clear();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="items"></param>
        void AddRange(IEnumerable<KeyValuePair<string, object>> items);

        /// <summary>
        /// ���Ըı��¼�
        /// </summary>
        event EventHandler<PropertyChangedEventArgs> PropertyChanged;


    }

   
}
