using System.Collections.Generic;

namespace Rtully.LinkedIn.Articles.ObjectComposer
{
    public class ComposerService : IObjectComposer
    {
        private static IObjectComposer _Clone = null;
        protected static Dictionary<string, object> factories;

        public static IObjectComposer ComposerFactory
        {
            get
            {
                if (_Clone == null)
                {
                    _Clone = new ComposerService();
                }
                return _Clone;
            }
            set { _Clone = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public ComposerService()
        {
            factories = new Dictionary<string, object>();
        }

        public void RegisterObjectMapper<S, T>(IObjectMapper<S, T> objectFactory)
        {
            string factoryKey = typeof(S).FullName + typeof(T).FullName;
            factories[factoryKey] = objectFactory;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="S">Source type</typeparam>
        /// <typeparam name="T">Target type</typeparam>
        /// <returns></returns>
        public IObjectMapper<S, T> GetObjectMapper<S, T>()
            where S : new()
            where T : new()
        {
            string factoryKey = typeof(S).FullName + typeof(T).FullName;
            factories.TryGetValue(factoryKey, out object mapperObj);
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
