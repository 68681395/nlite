using System;
using System.Diagnostics;

namespace NLite
{
    /// <summary>
    /// Enum ��ǩ��
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    [DebuggerDisplay("Name={Name},Value={Value},Description={Description}")]
    public class EnumAttribute : Attribute
    {
        
        /// <summary>
        /// �õ�������ö����ȱʡ����
        /// </summary>
        public string DefaultDescription { get; set; }
        /// <summary>
        /// �õ�������ö�������
        /// </summary>
        public int Order { get; set; }
        /// <summary>
        /// �õ���������ԴKey
        /// </summary>
        public string ResourceKey { get; set; }

        /// <summary>
        /// �õ�ö��������
        /// </summary>
        public string Name { get; internal set; }

        /// <summary>
        /// �õ�ö�����ֵ
        /// </summary>
        public Enum OriginalValue { get; internal set; }

        /// <summary>
        /// �õ�ö���������ֵ
        /// </summary>
        public int Value { get; internal set; }
        /// <summary>
        /// �õ�ö��������
        /// </summary>
        public string Description
        {
            get
            {
                if (!string.IsNullOrEmpty(ResourceKey))
                    return ResourceKey.StringResource();
                if (!string.IsNullOrEmpty(DefaultDescription))
                    return DefaultDescription;
                return Name;
            }
        }
    }
}
