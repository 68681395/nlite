using System;

namespace NLite
{
    /// <summary>
    /// Runtime Type Handle ��չ��
    /// </summary>
    public static class RuntimeTypeHandleExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="handle"></param>
        /// <returns></returns>
        public static Type Type(this RuntimeTypeHandle handle)
        {
            return System.Type.GetTypeFromHandle(handle);
        }
    }
}
