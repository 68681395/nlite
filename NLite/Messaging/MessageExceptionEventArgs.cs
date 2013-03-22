using System;

namespace NLite.Messaging
{
    /// <summary>
    /// ��Ϣ�쳣�¼�����
    /// </summary>
    #if !SILVERLIGHT
    [Serializable]
    #endif
    public class MessageExceptionEventArgs : MessageEventArgs
    {
       /// <summary>
       /// 
       /// </summary>
       /// <param name="ctx"></param>
       /// <param name="ex"></param>
        public MessageExceptionEventArgs(IMessageContext ctx, Exception ex)
            : base(ctx)
        {
            Error = ex;
        }

        /// <summary>
        /// 
        /// </summary>
        public Exception Error { get; internal set; }

        /// <summary>
        /// 
        /// </summary>
        public bool Canceled { get; set; }
    }
}
