
namespace NLite.Globalization
{
    /// <summary>
    /// ��Դָ���ӿ�
    /// </summary>
    public interface IResourceRepository:ILanguageChangedListner
    {
        /// <summary>
        /// ע����Դע���
        /// </summary>
        /// <param name="key">ע�������</param>
        /// <param name="resourceMgr">ע���</param>
        void Register(string key, IResourceRegistry resourceMgr);
        /// <summary>
        /// �õ���Դע���
        /// </summary>
        /// <param name="key">ע�������</param>
        /// <returns></returns>
        IResourceRegistry Get(string key);
        /// <summary>
        /// �õ���Դ��λ��
        /// </summary>
        /// <typeparam name="TResource">��Դ��λ������</typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        IResourceLocator<TResource> Get<TResource>(string key);
    }
}
