using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace NLite.Globalization
{
    /// <summary>
    /// ��Դע���ӿ�
    /// </summary>
    public interface IResourceRegistry : ILanguageChangedListner
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="items"></param>
        /// <param name="asm"></param>
        void Register(IEnumerable<IResourceItem> items, Assembly asm);

        /// <summary>
        /// ����ResourceItem ������ע���ļ���Դ���߳�����Դ
        /// </summary>
        /// <param name="item"></param>
        /// <param name="asm"></param>
        void Register(IResourceItem item, Assembly asm);

        /// <summary>
        /// ע��Assembly��Դ
        /// </summary>
        /// <param name="baseResourceName"></param>
        /// <param name="assembly"></param>
        void Register(string baseResourceName, Assembly assembly);
        /// <summary>
        /// ע���ļ���Դ
        /// </summary>
        /// <param name="fileName"></param>
        void Register(string fileName);
        /// <summary>
        /// ע������Դ
        /// </summary>
        /// <param name="stream"></param>
        void Register(Stream stream);
    }
}