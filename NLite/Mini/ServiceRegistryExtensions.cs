using System;
using System.Reflection;
using NLite.Reflection;
using System.Collections.Generic;
using System.Linq;
using NLite.Globalization;

namespace NLite
{
    /// <summary>
    /// ����ע�����չ��
    /// </summary>
    public static partial class ServiceRegistryExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <param name="registry"></param>
        /// <returns></returns>
        public static bool HasRegister<TService>(this IServiceRegistry registry)
        {
            if (registry == null)
                throw new ArgumentNullException("registry");
            return registry.HasRegister(typeof(TService));
        }


        #region ע��ʵ��
        /// <summary>
        /// ͨ����Լ���ͣ����Id�Լ����ʵ����ע�����
        /// </summary>
        /// <typeparam name="TContract"></typeparam>
        /// <typeparam name="TComponent"></typeparam>
        /// <param name="registry"></param>
        /// <param name="id"></param>
        /// <param name="instance"></param>
        /// <returns></returns>
        public static IServiceRegistry RegisterInstance<TContract, TComponent>(this IServiceRegistry registry, string id, TComponent instance) where TComponent : TContract
        {
            if (registry == null)
                throw new ArgumentNullException("registry");
            return registry.RegisterInstance(id, typeof(TContract), instance);
        }

        /// <summary>
        /// ͨ����Լ���ͣ����Id�Լ����ʵ����ע�����
        /// </summary>
        /// <typeparam name="TContract"></typeparam>
        /// <typeparam name="TComponent"></typeparam>
        /// <param name="registry"></param>
        /// <param name="id"></param>
        /// <param name="instance"></param>
        /// <returns></returns>
        public static IServiceRegistry RegisterInstance<TContract, TComponent>(this IServiceRegistry registry, TComponent instance) where TComponent : TContract
        {
            if (registry == null)
                throw new ArgumentNullException("registry");
            if (instance == null)
                return registry;
            var id = instance.GetType().FullName��+ instance.GetHashCode().ToString();
            return registry.RegisterInstance(id, typeof(TContract), instance);
        }

        /// <summary>
        /// ע�����ʵ��
        /// </summary>
        /// <param name="registry"></param>
        /// <param name="instance"></param>
        /// <returns></returns>
        public static IServiceRegistry RegisterInstance(this IServiceRegistry registry, object instance)
        {
            if (registry == null)
                throw new ArgumentNullException("registry");
            if (instance == null)
                return registry;
            var id = instance.GetType().FullName + instance.GetHashCode().ToString();

            registry.RegisterInstance(id, instance);
            return registry;
        }
        #endregion


        #region ͨ���������ע�����
        /// <summary>
        /// ͨ���������ע�����
        /// </summary>
        /// <typeparam name="TComponent"></typeparam>
        /// <param name="registry"></param>
        /// <returns></returns>
        public static IServiceRegistry Register<TComponent>(this IServiceRegistry registry)
        {
            if (registry == null)
                throw new ArgumentNullException("registry");
            registry.Register(new ComponentInfo(typeof(TComponent)));
            return registry;
        }

        /// <summary>
        /// ͨ��������ͺͺ����Id��ע�����
        /// </summary>
        /// <typeparam name="TComponent"></typeparam>
        /// <param name="registry"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static IServiceRegistry Register<TComponent>(this IServiceRegistry registry, string id)
        {
            if (registry == null)
                throw new ArgumentNullException("registry");
            registry.Register(new ComponentInfo(id, null, typeof(TComponent)));
            return registry;
        }

        /// <summary>
        /// ͨ����Լ���ͺ����������ע�����
        /// </summary>
        /// <param name="registry"></param>
        /// <param name="contract"></param>
        /// <param name="component"></param>
        /// <returns></returns>
        public static IServiceRegistry Register(this IServiceRegistry registry, Type contract, Type component)
        {
            if (registry == null)
                throw new ArgumentNullException("registry");
            if (component != null)
                return registry.Register(new ComponentInfo(contract, component));
            return registry;
        }

        /// <summary>
        /// ͨ����Լ���ͣ�������ͣ����Id�Լ���������������ע�����
        /// </summary>
        /// <typeparam name="TContract"></typeparam>
        /// <typeparam name="TComponent"></typeparam>
        /// <param name="registry"></param>
        /// <param name="id"></param>
        /// <param name="lifestyle"></param>
        /// <returns></returns>
        public static IServiceRegistry Register<TContract, TComponent>(this IServiceRegistry registry, string id, LifestyleFlags lifestyle) where TComponent : TContract
        {
            if (registry == null)
                throw new ArgumentNullException("registry");
            return registry.Register(new ComponentInfo(id, typeof(TContract), typeof(TComponent), ActivatorType.Default, lifestyle));
        }

        /// <summary>
        /// ͨ����Լ���ͺ����������ע�����
        /// </summary>
        /// <typeparam name="TContract"></typeparam>
        /// <typeparam name="TComponent"></typeparam>
        /// <param name="registry"></param>
        /// <returns></returns>
        public static IServiceRegistry Register<TContract, TComponent>(this IServiceRegistry registry) where TComponent : TContract
        {
            if (registry == null)
                throw new ArgumentNullException("registry");
            var type = typeof(TComponent);
            return registry.Register(new ComponentInfo(type.FullName, typeof(TContract), type));
        }

        /// <summary>
        ///  ͨ����Լ���ͣ�������ͣ����Id�Լ���������������ע�����
        /// </summary>
        /// <typeparam name="TContract"></typeparam>
        /// <typeparam name="TComponent"></typeparam>
        /// <param name="registry"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static IServiceRegistry Register<TContract, TComponent>(this IServiceRegistry registry, string id) where TComponent : TContract
        {
            if (registry == null)
                throw new ArgumentNullException("registry");
            return registry.Register(new ComponentInfo(id, typeof(TContract), typeof(TComponent)));
        }

        /// <summary>
        /// ͨ���������ע�����
        /// </summary>
        /// <param name="registry"></param>
        /// <param name="component"></param>
        /// <returns></returns>
        public static IServiceRegistry Register(this IServiceRegistry registry, Type component)
        {
            if (registry == null)
                throw new ArgumentNullException("registry");
            if (component != null)
                return registry.Register(new ComponentInfo(component.FullName, component, component));
            return registry;
        }

        /// <summary>
        /// ͨ��������ͺͺ����Id��ע�����
        /// </summary>
        /// <param name="registry"></param>
        /// <param name="id"></param>
        /// <param name="component"></param>
        /// <returns></returns>
        public static IServiceRegistry Register(this IServiceRegistry registry, string id, Type component)
        {
            if (registry == null)
                throw new ArgumentNullException("registry");
            if (component != null)
                return registry.Register(new ComponentInfo(id, component, component));
            return registry;
        }

        /// <summary>
        /// ͨ����Լ���ͣ�������ͣ����Id�Լ���������������ע�����
        /// </summary>
        /// <param name="registry"></param>
        /// <param name="id"></param>
        /// <param name="contract"></param>
        /// <param name="component"></param>
        /// <returns></returns>
        public static IServiceRegistry Register(this IServiceRegistry registry, string id, Type contract, Type component)
        {
            if (registry == null)
                throw new ArgumentNullException("registry");
            if (component != null)
                return registry.Register(new ComponentInfo(id, contract, component));
            return registry;
        }
        #endregion


        #region ͨ����Լ�����Լ�����Ĺ���������ע�����

        /// <summary>
        /// ͨ����Լ�����Լ�����Ĺ���������ע�����
        /// </summary>
        /// <typeparam name="TContract"></typeparam>
        /// <param name="registry"></param>
        /// <param name="creator"></param>
        /// <returns></returns>
        public static IServiceRegistry Register<TContract>(this IServiceRegistry registry, Func<object> creator)
        {
            if (registry == null)
                throw new ArgumentNullException("registry");
            return registry.Register(new ComponentInfo(null, typeof(TContract), creator));
        }

        /// <summary>
        /// ͨ����Լ���ͣ����Id�Լ�����Ĺ���������ע�����
        /// </summary>
        /// <typeparam name="TContract"></typeparam>
        /// <param name="registry"></param>
        /// <param name="id"></param>
        /// <param name="creator"></param>
        /// <returns></returns>
        public static IServiceRegistry Register<TContract>(this IServiceRegistry registry, string id, Func<object> creator)
        {
            if (registry == null)
                throw new ArgumentNullException("registry");
            return registry.Register(new ComponentInfo(id, typeof(TContract), creator));
        }
        #endregion


        /// <summary>
        /// ע��ָ����������Assembly�µ����б��Ϊ"ComponentAttribue"�����
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="registry"></param>
        /// <returns></returns>
        public static IServiceRegistry RegisterFromAssemblyOf<T>(this IServiceRegistry registry)
        {
            return RegisterFromAssembly(registry, typeof(T).Assembly,FilteType);
        }

        /// <summary>
        /// ע��ָ����������Assembly�·��Ϲ�������������
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="registry"></param>
        /// <param name="typeFilter"></param>
        /// <returns></returns>
        public static IServiceRegistry RegisterFromAssemblyOf<T>(this IServiceRegistry registry, Func<Type,bool> typeFilter)
        {
            return RegisterFromAssembly(registry, typeof(T).Assembly, typeFilter);
        }

        /// <summary>
        /// ע����Assembly�µ����б��Ϊ"ComponentAttribue"�����
        /// </summary>
        /// <param name="registry"></param>
        /// <param name="assembly"></param>
        /// <returns></returns>
        public static IServiceRegistry RegisterFromAssembly(this IServiceRegistry registry, Assembly assembly)
        {
            return RegisterFromAssembly(registry, assembly, FilteType);
        }
        /// <summary>
        /// ע����ָ����assembly������Ϲ�������������
        /// </summary>
        /// <param name="registry"></param>
        /// <param name="assembly"></param>
        /// <returns></returns>
        public static IServiceRegistry RegisterFromAssembly(this IServiceRegistry registry, Assembly assembly, Func<Type,bool> typeFilter)
        {
            if (registry == null)
                throw new ArgumentNullException("registry");
            if (assembly == null)
                throw new ArgumentNullException("assembly");
            if (assembly.IsSystemAssembly())
                return registry;

            Func<Type, bool> filter = typeFilter ?? FilteType;

            var items = (from t in assembly.GetTypes().Where(t=>!t.IsInterface)
                         from item in GetTypes(t, typeFilter)
                         select new { Implemention = item.Key, Option = item.Value }).Distinct().ToArray();

            if (items != null && items.Length > 0)
            {
                string id = null;
                foreach (var item in items)
                {
                    id = string.IsNullOrEmpty(item.Option.Id) ? item.Implemention.FullName : item.Option.Id;
                    registry.Register(new ComponentInfo(id, item.Option.Contract, item.Implemention, item.Option.Lifestyle));
                }
            }

            //var resources = assembly
            //    .GetAttributes<StringResourceAttribute>(false)
            //    .Select(i => new ResourceItem
            //    {
            //        BaseResourceName = i.BaseResourceName
            //        ,
            //        ResourceFile = i.ResourceFile
            //    }).ToArray();

            //if (resources.Length > 0)
            //    NLite.Globalization.ResourceRepository.StringRegistry.Register(resources, assembly);

            //resources = assembly
            //    .GetAttributes<ImageResourceAttribute>(false)
            //    .Select(i => new ResourceItem
            //    {
            //        BaseResourceName = i.BaseResourceName
            //        ,
            //        ResourceFile = i.ResourceFile
            //    }).ToArray();
            //if (resources.Length > 0)
            //    ResourceRepository.ImageRegistry.Register(resources, assembly);


            //resources = assembly
            //  .GetAttributes<IconResourceAttribute>(false)
            //  .Select(i => new ResourceItem
            //  {
            //      BaseResourceName = i.BaseResourceName
            //      ,
            //      ResourceFile = i.ResourceFile
            //  }).ToArray();
            //if (resources.Length > 0)
            //    ResourceRepository.IconRegistry.Register(resources, assembly);

            return registry;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="registry"></param>
        /// <param name="typeFilter"></param>
        /// <returns></returns>
        public static IServiceRegistry RegisterFromAppDomain(this IServiceRegistry registry, Func<Type, bool> typeFilter)
        {
            if (registry == null)
                throw new ArgumentNullException("registry");
            foreach (var asm in AppDomain.CurrentDomain.GetAssemblies())
                RegisterFromAssembly(registry, asm, typeFilter);
            return registry;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="registry"></param>
        /// <returns></returns>
        public static IServiceRegistry RegisterFromAppDomain(this IServiceRegistry registry)
        {
            return RegisterFromAppDomain(registry, FilteType);
        }

        private static bool FilteType(Type type)
        {
            return type.HasAttribute<ComponentAttribute>(true);
        }

        private static IEnumerable<KeyValuePair<Type, ComponentAttribute>> GetTypes(Type type, Func<Type, bool> typeFilter)
        {
            List<KeyValuePair<Type, ComponentAttribute>> results = new List<KeyValuePair<Type, ComponentAttribute>>();
            const BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic;

            if (typeFilter(type))
            {
                var attr = type.GetAttribute<ComponentAttribute>(true);
                if (!type.IsAbstract && attr != null)
                    results.Add(new KeyValuePair<Type, ComponentAttribute>(type, attr));
                else
                    results.Add(new KeyValuePair<Type, ComponentAttribute>(type, ComponentAttribute.Default));
            }

            var types = type.GetNestedTypes(bindingFlags);
            if (types.Length > 0)
                foreach (var item in types)
                    results.AddRange(GetTypes(item, typeFilter));

            return results;
        }
    }
}
