
namespace NLite
{
    /// <summary>
    /// ��������������ӿ�
    /// </summary>
    public interface IComponentListenerManager : IListenerManager<IComponentListener>
    {
        /// <summary>
        /// ��ʼ���������������
        /// </summary>
        /// <param name="registry"></param>
        void Init(IKernel kernel);
        /// <summary>
        /// �Ƿ����ü�����
        /// </summary>
        bool Enabled { get; set; }
    }
}
