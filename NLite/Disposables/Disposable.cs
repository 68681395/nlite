using System;
using NLite.Internal;

namespace NLite
{
    /// <summary>
    /// Disposable����
    /// </summary>
    [Serializable]
    public struct Disposable
    {
        /// <summary> 
        /// �õ���Disposable����
        /// </summary>
        public static readonly IDisposable Empty = new EmptyDisposeAction();

        /// <summary>
        /// ����Disposable����
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public static IDisposable Create(Action action)
        {
            Guard.NotNull(action, "action");
            return new ActionDisposable(action);
        }

        /// <summary>
        /// ����Disposable����
        /// </summary>
        /// <param name="target"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static IDisposable Create(object target, Action action)
        {
            Guard.NotNull(target, "target");
            Guard.NotNull(action, "action");
            return new ActionDisposable(target,action);
        }
    }
}
