using System.Collections.Generic;

namespace Rtully.LinkedIn.Articles.ObjectComposer
{
    public class ComposerService : IObjectComposer
    {
        protected static Dictionary<string, object> mappers;

        public static IObjectComposer Composer
        {
            get; set;
        }

        /// <summary>
        /// static constructors in C# are specified to execute only when an instance 
        /// of the class is created or a static member is referenced, and to execute 
        /// only once per AppDomain
        /// </summary>
        static ComposerService()
        {
            Composer = new ComposerService();
            mappers = new Dictionary<string, object>();
        }

        private ComposerService()
        {           
        }

        public static void RegisterObjectMapper<S, T>(IObjectMapper<S, T> objectFactory)
        {
            string factoryKey = typeof(S).FullName + typeof(T).FullName;
            mappers[factoryKey] = objectFactory;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="S">Source type</typeparam>
        /// <typeparam name="T">Target type</typeparam>
        /// <returns></returns>
        private IObjectMapper<S, T> GetObjectMapper<S, T>()
            where S : new()
            where T : new()
        {
            string factoryKey = typeof(S).FullName + typeof(T).FullName;
            mappers.TryGetValue(factoryKey, out object mapperObj);
            IObjectMapper<S, T> mapper = mapperObj as IObjectMapper<S, T>;
            if (mapper == null)
            {
                return new SimpleObjectMapper<S, T>();
            }

            return mapper;
        }

        public T Compose<S, T>(S source, params object[] optionalParams)
            where S : new()
            where T : new()
        {
            return GetObjectMapper<S, T>().CreateAndCopy(source, optionalParams);
        }

        public T Compose<S, T>(S source, T target, params object[] optionalParams)
            where S : new()
            where T : new()
        {
            return GetObjectMapper<S, T>().Copy(source, target, optionalParams);
        }
    }
}
