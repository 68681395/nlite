using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;

using NLite.Internal;
using NLite.Messaging;
using NLite.Messaging.Internal;
using NLite.Reflection;

namespace NLite
{
    namespace Messaging
    {
        /// <summary>
        /// 
        /// </summary>
         #if !SILVERLIGHT
    [Serializable]
    #endif
        public class SimpleBus : IMessageBus
        {
            private readonly IMessageRepository Repository;

            /// <summary>
            /// 
            /// </summary>
            public SimpleBus()
            {
                this.Repository = new MessageRepository();
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="req"></param>
            /// <returns></returns>
            public IMessageResponse Publish(IMessageRequest req)
            {
                if (req != null)
                {
                    Type type = req.Data != null ? req.Data.GetType() : null;
                    return Repository[req.Topic, type].Publish(req);
                }

                return MessageResult.Empty;
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="hks"></param>
            public void RegisterListner(params IMessageListener[] hks)
            {
                Repository.ListnerManager.Register(hks);
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="hks"></param>
            public void UnRegisterListner(params IMessageListener[] hks)
            {
                Repository.ListnerManager.UnRegister(hks);
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="topic">��Ϣ����</param>
            /// <param name="messageType"></param>
            /// <param name="handler">������</param>
            public void Unsubscribe(string topic, Type messageType, IObserverHandler<IMessage> handler)
            {
                Repository[topic, messageType].Subscriber.Unsubscribe(handler);
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="topic">��Ϣ����</param>
            /// <param name="messageType"></param>
            /// <param name="mode"></param>
            /// <param name="handler">������</param>
            /// <returns></returns>
            public IDisposable Subscribe(ISubscribeInfo info)
            {
                return Repository[info.Topic, info.MessageType].Subscriber.Subscribe(info.Mode, info.Handler);
            }


            /// <summary>
            /// 
            /// </summary>
            /// <param name="topic">��Ϣ����</param>
            /// <param name="binderType">����</param>
            public void Remove(string topic, Type type)
            {
                Repository.Remove(topic, type);
            }

            /// <summary>
            /// 
            /// </summary>
            public void Dispose()
            {
                if (Repository != null)
                    Repository.Dispose();
            }


            /// <summary>
            /// �Ƴ�����������Ϣ
            /// </summary>
            public void RemoveAll()
            {
                if (Repository != null)
                    Repository.Clear();

            }
        }
    }

    
}
