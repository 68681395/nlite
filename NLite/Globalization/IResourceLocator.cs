
namespace NLite.Globalization
{
    /// <summary>
    /// ��Դ��λ���ӿ�
    /// </summary>
    /// <typeparam name="TResource">��Դ����</typeparam>
    public interface IResourceLocator<TResource>
    {
        /// <summary>
        /// ͨ����Դ���Ƶõ�ָ������Դ����
        /// </summary>
        /// <param name="name">��Դ����</param>
        /// <returns></returns>
        TResource Get(string name);
    }
}
