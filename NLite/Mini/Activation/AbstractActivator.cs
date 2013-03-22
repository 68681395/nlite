using System;
using System.Linq;
using NLite.Mini.Context;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using System.Text;
using NLite.Internal;
using NLite.Mini.Activation.Internal;
using NLite.Threading;

namespace NLite.Mini.Activation
{
    /// <summary>
    /// �����������
    /// </summary>
    public abstract class AbstractActivator : BooleanDisposable, IActivator
    {
        /// <summary>
        /// �������
        /// </summary>
        /// <param name="context">����������</param>
        /// <returns>���������������</returns>
        public virtual object Create(IComponentContext context)
        {
            Guard.NotNull(context, "context");
            //1. ��¼��������������Ķ���ͼ
            Tracker.Start(context);
          
            object instance = null;

            //2. �õ��������������
            var componentListner = context.Kernel.ListenerManager as IComponentListener;

            //3. ���������ǰ���м���
            if (componentListner != null)
                componentListner.OnPreCreation(context);

            //4. ����� Create instance
            instance = InternalCreate(context);
            context.Instance = instance;
            if (componentListner != null)
            {
                //5. �������������м����������������ע������ŵ㣩
                componentListner.OnPostCreation(context);

                //6. �����������������ʼ�����м���
                componentListner.OnInitialization(context);
                //7. �������ʼ������м���
                componentListner.OnPostInitialization(context);
            }

            context.Args = null;
            context.NamedArgs = null;

            Tracker.Stop();

            return instance;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        protected virtual object InternalCreate(IComponentContext context) { throw new NotImplementedException(); }
    }

    namespace Internal
    {

        class Tracker
        {
            //const string key = "ComponentActivator.Traker";
            //[ThreadStatic]
            //static StringBuilder o = new StringBuilder(500);

            //public static void Start(IComponentContext ctx)
            //{
            //    var trakeItem = ctx.Component.Implementation == null ? ctx.Component.Id : ctx.Component.Implementation.FullName;

            //    if (o == null)
            //        o = new StringBuilder(trakeItem);
            //    else
            //        o.Append("->").Append(trakeItem);
            //}

            //public static void Stop()
            //{
            //    o.Length = 0;
            //}
            //public static string CallStack
            //{
            //    get
            //    {
            //        return o.ToString();
            //    }
            //}

            [ThreadStatic]
            static TrackStack stack;

            public static void Start(IComponentContext ctx)
            {
                if (stack == null) stack = new TrackStack();
                stack.Push(new TrackItem(ctx));
            }

            public static void Stop()
            {
                stack.Pop();
            }
        }

        class TrackStack
        {
            private TrackItem _current;
            private TrackItem _root;

            internal TrackStack()
            {
            }

           
            public TrackItem Root { get { return _root; } }

         
            public TrackItem Current { get { return _current; } }

            public TrackItem Parent { get { return _current.parent; } }

            internal void Push(TrackItem frame)
            {
                if (_root == null)
                {
                    _root = _current = frame;
                }
                else
                {
                    if (_root.Contains(frame))
                        throw ExceptionManager.HandleAndWrapper<LoopDependencyException>(_root.ToStackString());
                    _current.Attach(frame);
                    _current = frame;
                }
            }

            internal TrackItem Pop()
            {
                _current = _current.Detach();
                if (_current == null) _root = null;
                return _current;
            }

            public void Clear()
            {
                _current = _root = null;
            }
        }

        class TrackItem
        {
            public readonly string Name;
            public readonly IComponentContext Context;
            readonly int HashCode;
            public TrackItem(IComponentContext ctx)
            {
                Name = ctx.Component.Implementation == null ? ctx.Component.Id : ctx.Component.Implementation.FullName;
                Context= ctx;
                HashCode = Name.GetHashCode();
            }
            private TrackItem next;
            internal TrackItem parent;

            internal void Attach(TrackItem next)
            {
                this.next = next;
                this.next.parent = this;
            }

            internal TrackItem Detach()
            {
                if (parent != null) parent.next = null;
                return parent;
            }

            public bool Equals(TrackItem obj)
            {
                if (ReferenceEquals(null, obj)) return false;
                if (ReferenceEquals(this, obj)) return true;
                return Equals(obj.Name, Name);
            }

            public override bool Equals(object obj)
            {
                if (ReferenceEquals(null, obj)) return false;
                if (ReferenceEquals(this, obj)) return true;
                if (obj.GetType() != typeof(TrackItem)) return false;
                return Equals((TrackItem)obj);
            }

            public override int GetHashCode()
            {
                unchecked
                {
                    return HashCode;
                }
            }

            public bool Contains(TrackItem frame)
            {
                if (Name == frame.Name)
                {
                    return true;
                }

                return next == null ? false : next.Contains(frame);
            }

            public override string ToString()
            {
                return Name;
            }

            public string ToStackString()
            {
                string message = "\n1. " + ToString();
                TrackItem tmpnext = next;

                int i = 2;
                while (tmpnext != null)
                {
                    message += string.Format("\n{0}. {1}",i++, next);
                    tmpnext = tmpnext.next;
                }

                return message;
            }
        }
    }
}
