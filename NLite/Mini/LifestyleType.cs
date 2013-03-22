
namespace NLite
{
    /// <summary>
    /// ���󹤳�����
    /// </summary>
    public class ActivatorType
    {
        /// <summary>
        /// ȱʡ��������
        /// </summary>
        public const string Default = "Default";
        /// <summary>
        /// ʵ����������
        /// </summary>
        public const string Instance = "Instance";
        /// <summary>
        /// �����Ĺ�������
        /// </summary>
        public const string Factory = "Factory";
        /// <summary>
        /// ����������
        /// </summary>
        public const string Proxy = "Proxy";
    }

    /// <summary>
    /// ���������������
    /// </summary>
    public class  LifestyleType
    {
        /// <summary>
        /// �õ������������ȷʵ������������
        /// </summary>
        public static LifestyleFlags Default {get;set;}

        static LifestyleType()
        {
            Default = LifestyleFlags.Singleton;
        }

        internal static LifestyleFlags GetGenericLifestyle(LifestyleFlags lifestyle)
        {
            if (lifestyle == LifestyleFlags.Singleton)
                return LifestyleFlags.GenericSingleton;
            if (lifestyle == LifestyleFlags.Transient)
                return LifestyleFlags.GenericTransient;
            if (lifestyle == LifestyleFlags.Thread)
                return LifestyleFlags.GenericThread;
            return lifestyle;
        }

        internal static LifestyleFlags GetLifestyle(LifestyleFlags lifestyle)
        {
            if (lifestyle == LifestyleFlags.GenericSingleton)
                return LifestyleFlags.Singleton;
            if (lifestyle == LifestyleFlags.GenericTransient)
                return LifestyleFlags.Transient;
            if (lifestyle == LifestyleFlags.GenericThread)
                return LifestyleFlags.Thread;
            return lifestyle;
        }
    }

    /// <summary>
    /// ���������������ö��
    /// </summary>
    public enum LifestyleFlags
    {
        /// <summary>
        /// ����
        /// </summary>
        Singleton,
        /// <summary>
        /// �߳��ڵ���
        /// </summary>
        Thread,
        /// <summary>
        /// ��ʱ
        /// </summary>
        Transient,

        /// <summary>
        /// ���͵���
        /// </summary>
        GenericSingleton,
        /// <summary>
        /// �����߳��ڵ���
        /// </summary>
        GenericThread,
        /// <summary>
        /// ������ʱ
        /// </summary>
        GenericTransient,
    }
}
