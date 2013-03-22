using System;

namespace NLite.Messaging
{
    /// <summary>
    /// ��Ϣ�¼�����
    /// </summary>
    #if !SILVERLIGHT
    [Serializable]
    #endif
    public class MessageEventArgs
    {
        /// <summary>
        /// 
        /// </summary>
        public object Sender { get { return Context.Request.Sender; } }

        /// <summary>
        /// 
        /// </summary>
        public IMessageContext Context { get; internal set; }

      
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ctx"></param>
        public MessageEventArgs(IMessageContext ctx)
        {
            Context = ctx;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Context.Request.ToString();
        }
    }
}
