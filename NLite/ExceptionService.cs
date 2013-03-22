﻿using System;
using System.Linq;
using System.Configuration;
using System.Collections.Generic;
using NLite.Collections;
using NLite.Log;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using NLite.Internal;
using System.Diagnostics;
using NLite.Threading;

namespace NLite
{
    /// <summary>
    /// Represents errors that occur in the framework.
    /// </summary>
     #if !SILVERLIGHT
    [Serializable]
    #endif
    public class NLiteException:Exception 
    {
        /// <summary>
        /// 
        /// </summary>
        public ErrorState ErrorState { get; private set; }

         #region Ctor
        /// <summary>
        /// Initializes a new instance of the <c>NLiteException</c> class.
        /// </summary>
        public NLiteException() : base() { ErrorState = new ErrorState(); }
        /// <summary>
        /// Initializes a new instance of the <c>NLiteException</c> class with the specified
        /// error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public NLiteException(string message) : base(message) { ErrorState = new ErrorState(); }
        
        /// <summary>
        /// Initializes a new instance of the <c>NLiteException</c> class with the specified
        /// error message and the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="innerException">The inner exception that is the cause of this exception.</param>
        public NLiteException(string message, Exception innerException)
            : base(message, innerException)
        {
            var nliteException = innerException as NLiteException;
            ErrorState = nliteException == null ? new ErrorState() : nliteException.ErrorState;
        }
        #if !SILVERLIGHT
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bindingInfo"></param>
        /// <param name="context"></param>
        protected NLiteException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
        #endif
        #endregion


        ///// <summary>
        ///// Gets or sets exception handled
        ///// </summary>
        //public bool ExceptionHandled
        //{
        //    get;
        //    set;
        //}


    }
    /// <summary>
    /// 异常的扩展类
    /// </summary>
    public static class ExceptionService
    {
        /// <summary>
        /// 处理异常
        /// </summary>
        /// <param name="ex"></param>
        public static Exception Handle(this Exception ex)
        {
            return ExceptionManager.HandleException(ex);
        }

        /// <summary>
        /// 处理异常
        /// </summary>
        /// <typeparam name="TNewExcption"></typeparam>
        /// <param name="ex"></param>
        /// <param name="message"></param>
        public static TNewExcption Handle<TNewExcption>(this Exception ex, string message) where TNewExcption : Exception, new()
        {
            return ExceptionManager.HandleAndWrapper<TNewExcption>(ex, message);
        }

        /// <summary>
        /// 处理异常
        /// </summary>
        /// <typeparam name="TNewExcption"></typeparam>
        /// <param name="ex"></param>
        public static TNewExcption Handle<TNewExcption>(this Exception ex) where TNewExcption : Exception, new()
        {
            return ExceptionManager.HandleAndWrapper<TNewExcption>(ex, ex.Message);
        }
    }

    /// <summary>
    /// Represents that the implemented classes are exception handlers.
    /// </summary>
    //[Contract]
    public interface IExceptionHandler
    {
        /// <summary>
        /// 处理异常
        /// </summary>
        /// <param name="ex"></param>
        void HandleException(Exception ex);
    }

    /// <summary>
    /// 异常解析器接口
    /// </summary>
    //[Contract]
    public interface IExceptionResolver
    {
        /// <summary>
        /// 解析器的顺序
        /// </summary>
        int Order { get; set; }
        /// <summary>
        /// 异常代码字典
        /// </summary>
        IExceptionCode ExceptionCode { get; }
        /// <summary>
        /// 异常呈现器集合
        /// </summary>
        IExceptionRender[] ExceptionRenders { get; }
        /// <summary>
        /// 是否支持特定类型异常的解析
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        bool HasSupport(Exception ex);
        /// <summary>
        /// 处理异常
        /// </summary>
        /// <param name="ex"></param>
        void HandleException(Exception ex);
    }

    /// <summary>
    /// 异常呈现器
    /// </summary>
    //[Contract]
    public interface IExceptionRender
    {
        /// <summary>
        /// 呈现异常
        /// </summary>
        /// <param name="code">异常代码</param>
        /// <param name="ex">异常</param>
        void RenderException(int code, Exception ex);
    }

    /// <summary>
    /// 异常代码字典接口
    /// </summary>
    //[Contract]
    public interface IExceptionCode
    {
        /// <summary>
        /// 成功的代码编号
        /// </summary>
        int Success { get; }
        /// <summary>
        /// 未知异常的代码编号
        /// </summary>
        int UnknowExceptionCode { get; }

        /// <summary>
        /// Db异常范围-开始代码编号
        /// </summary>
        int DbExceptionStart { get; }
        /// <summary>
        /// Db异常范围-结束代码编号
        /// </summary>
        int DbExceptionEnd { get; }
        /// <summary>
        /// 查询异常的代号
        /// </summary>
        int QueryException { get; }
        /// <summary>
        /// 持久化（增删改）的异常代码编号
        /// </summary>
        int PersistenceException { get; }
        /// <summary>
        /// 添加的异常代码编号
        /// </summary>
        int InsertException { get; }
        /// <summary>
        /// 删除的异常代码编号
        /// </summary>
        int DeleteException { get; }
        /// <summary>
        /// 更新的异常代码编号
        /// </summary>
        int UpdateException { get; }

        /// <summary>
        /// 领域服务的异常范围-开始代码编号
        /// </summary>
        int DomainExceptionStart { get; }
        /// <summary>
        /// 领域服务的异常范围-结束代码编号
        /// </summary>
        int DomainExceptionEnd { get; }

        /// <summary>
        /// 服务分发器异常范围：从10000开始
        /// </summary>
        int ServiceDispatcherExceptionStart { get; }

        /// <summary>
        /// 服务分发器异常范围：以19999结束
        /// </summary>
        int ServiceDispatcherExceptionEnd { get; }

        /// <summary>
        /// 自定义的异常范围-开始代码编号
        /// </summary>
        int CustomExceptionStart { get; }
        /// <summary>
        /// 自定义的异常范围-结束代码编号
        /// </summary>
        int CustomExceptionEnd { get; }
    }

    /// <summary>
    ///  异常代码字典
    /// </summary>
    public class ExceptionCode : IExceptionCode
    {
        /// <summary>
        /// 成功的代码编号，缺省是1
        /// </summary>
        public virtual int Success
        {
            get { return 1; }
        }

        /// <summary>
        /// 未知异常的代码编号，缺省是-1
        /// </summary>
        public virtual int UnknowExceptionCode
        {
            get { return -1; }
        }

      
        /// <summary>
        /// Db异常范围-20000开始
        /// </summary>
        public virtual int DbExceptionStart
        {
            get { return 20000; }
        }

        /// <summary>
        /// Db异常范围-29999结束
        /// </summary>
        public virtual int DbExceptionEnd
        {
            get { return 29999; }
        }

        /// <summary>
        /// 查询异常代码：20001
        /// </summary>
        public virtual int QueryException
        {
            get { return 20001; }
        }

        /// <summary>
        /// 持久化异常代码：20002
        /// </summary>
        public virtual int PersistenceException
        {
            get { return 20002; }
        }

        /// <summary>
        /// 添加异常代码：20003
        /// </summary>
        public virtual int InsertException
        {
            get { return 20003; }
        }

        /// <summary>
        /// 删除异常代码：20004
        /// </summary>
        public virtual int DeleteException
        {
            get { return 20004; }
        }

        /// <summary>
        /// 更新异常代码：20005
        /// </summary>
        public virtual int UpdateException
        {
            get { return 20005; }
        }

        /// <summary>
        /// 领域异常范围-30000开始
        /// </summary>
        public virtual int DomainExceptionStart
        {
            get { return 30000; }
        }

        /// <summary>
        /// 领域异常范围-59999结束
        /// </summary>
        public virtual int DomainExceptionEnd
        {
            get { return 59999; }
        }

        /// <summary>
        /// 服务分发器异常范围：从10000开始
        /// </summary>
        public virtual int ServiceDispatcherExceptionStart
        {
            get { return 10000; }
        }

        /// <summary>
        /// 服务分发器异常范围：以19999结束
        /// </summary>
        public virtual int ServiceDispatcherExceptionEnd
        {
            get { return 19999; }
        }

        /// <summary>
        /// 自定义异常范围-60000开始
        /// </summary>
        public virtual int CustomExceptionStart
        {
            get { return 60000; }
        }

        /// <summary>
        /// 自定义异常范围-99999结束
        /// </summary>
        public virtual int CustomExceptionEnd
        {
            get { return 99999; }
        }
    }

    /// <summary>
    /// 异常处理器
    /// </summary>
    public class ExceptionHandler : IExceptionHandler
    {
        /// <summary>
        /// 得到异常解析器集合
        /// </summary>
        [InjectMany]
        public IExceptionResolver[] Resolvers { get; protected set; }

        /// <summary>
        /// 处理异常，按照解析器的Order从小到大进行排序然后依次处理
        /// </summary>
        /// <param name="ex"></param>
        public virtual void HandleException(Exception ex)
        {
            if (Resolvers == null || Resolvers.Length == 0)
                throw new Exception("No any exceptionResolver");

            var resolver = Resolvers.OrderBy(p => p.Order).FirstOrDefault(p => p.HasSupport(ex));
            if (resolver != null)
                resolver.HandleException(ex);
        }
    }

    /// <summary>
    /// 异常解析器基类
    /// </summary>
    [Component]
    public abstract class ExceptionResolver : IExceptionResolver
    {
        /// <summary>
        /// 异常代码字典
        /// </summary>
        [Inject]
        public IExceptionCode ExceptionCode { get; protected set; }

        /// <summary>
        /// 异常呈现器集合
        /// </summary>
        [InjectMany]
        public IExceptionRender[] ExceptionRenders { get; protected set; }
        /// <summary>
        /// 异常解析器Order
        /// </summary>
        public int Order { get; set; }

        /// <summary>
        /// 是否支持特定类型异常的解析
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        public abstract bool HasSupport(Exception ex);
        /// <summary>
        /// 解析异常
        /// </summary>
        /// <param name="ex"></param>
        protected abstract void OnResolve(Exception ex);

        /// <summary>
        /// 处理异常
        /// </summary>
        /// <param name="ex"></param>
        public void HandleException(Exception ex)
        {
            if (ExceptionRenders == null || ExceptionRenders.Length == 0)
                throw new ArgumentNullException("ExceptionRender");
            if (ExceptionCode == null)
                throw new ArgumentNullException("ResponseCode");
            OnResolve(ex);
        }

        /// <summary>
        /// 呈现异常
        /// </summary>
        /// <param name="code"></param>
        /// <param name="ex"></param>
        protected void RenderException(int code, Exception ex)
        {
            ExceptionRenders.ForEach(p=>p.RenderException(code, ex));
        }
    }

    /// <summary>
    /// 未知异常解析器
    /// </summary>
    public class UnknowExceptionResolver : ExceptionResolver
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public UnknowExceptionResolver()
        {
            Order = int.MaxValue;
        }

        /// <summary>
        /// 支持所有异常
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        public override bool HasSupport(Exception ex)
        {
            return true;
        }

        /// <summary>
        /// 解析异常
        /// </summary>
        /// <param name="ex"></param>
        protected override void OnResolve(Exception ex)
        {
            RenderException(ExceptionCode.UnknowExceptionCode, ex);
        }
    }

    /// <summary>
    /// Debug异常呈现器
    /// </summary>
    public class DebugExceptionRender : IExceptionRender
    {
        /// <summary>
        /// 呈现异常
        /// </summary>
        /// <param name="code"></param>
        /// <param name="ex"></param>
        public void RenderException(int code, Exception ex)
        {
            Debug.WriteLine("ErrorCode:" + code + "\t Error Message:" + ex.Message +"\n\t"+ex.StackTrace);
        }
    }

    /// <summary>
    /// 日志异常呈现器
    /// </summary>
    public class LogExceptionReander : IExceptionRender
    {
        private static ILog log = LogManager.GetLogger(typeof(LogExceptionReander));

        /// <summary>
        /// 呈现异常
        /// </summary>
        /// <param name="code"></param>
        /// <param name="ex"></param>
        public void RenderException(int code, Exception ex)
        {
            log.Error("ErrorCode:" + code + "\t Error Message:" + ex.Message ,ex);
        }
    }

    /// <summary>
    /// 异常管理器
    /// </summary>
    public static class ExceptionManager
    {
        private static IExceptionHandler ExceptionHandler;
        private static readonly object Mutext = new object();

        private static void HandleExceptionInternal(Exception ex)
        {
            if (ExceptionHandler == null)
                lock (Mutext)
                {
                    if(ExceptionHandler == null)
                        ExceptionHandler = ServiceLocator.Get<IExceptionHandler>();
                }

            if (ExceptionHandler != null)
            {
                var tmpEx = Local.Get("Exception") as Exception;
                if (tmpEx != ex)
                {
                    ExceptionHandler.HandleException(ex);
                    Local.Set("Exception", ex);
                }
            }
        }

      
        /// <summary>
        /// 处理异常
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        public static Exception HandleException(Exception ex)
        {
            ExceptionManager.HandleExceptionInternal(ex);
            return ex;
        }

        /// <summary>
        /// 处理异常
        /// </summary>
        /// <typeparam name="TException"></typeparam>
        /// <param name="ex"></param>
        /// <returns></returns>
        public static TException HandleException<TException>(TException ex)
            where TException : Exception
        {
            ExceptionManager.HandleExceptionInternal((Exception)ex);
            return ex;
        }

        /// <summary>
        /// 处理并包装异常
        /// </summary>
        /// <typeparam name="TException"></typeparam>
        /// <param name="message"></param>
        /// <returns></returns>
        public static TException HandleAndWrapper<TException>(string message) where TException : Exception, new()
        {
            return HandleAndWrapper<TException>(null, message);
        }

        /// <summary>
        /// 抛出新异常
        /// </summary>
        /// <typeparam name="TException"></typeparam>
        /// <param name="ex"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static TException ThrowNew<TException>(this Exception ex, string message)
            where TException : Exception, new()
        {
            var targetException = default(TException);

            if (ex != null)
            {
                var ctor = typeof(TException).GetConstructor(new Type[] { typeof(string), typeof(Exception) });
                if (ctor != null)
                    targetException = ctor.Invoke(new object[] { message, ex }) as TException;
            }
            else if (!string.IsNullOrEmpty(message))
            {
                var ctor = typeof(TException).GetConstructor(new Type[] { typeof(string) });
                if (ctor != null)
                    targetException = ctor.Invoke(new object[] { message }) as TException;
            }
            else
                targetException = new TException();

            return targetException;
        }

        /// <summary>
        /// 处理并包装异常
        /// </summary>
        /// <typeparam name="TException"></typeparam>
        /// <param name="ex"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static TException HandleAndWrapper<TException>(Exception ex, string message)
            where TException : Exception, new()
        {
            var targetException = ThrowNew<TException>(ex, message);
            targetException.Handle();
            return targetException;
        }
    }
}