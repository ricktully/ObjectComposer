using System;
using System.Collections.Generic;
using System.Linq;

namespace Rtully.LinkedIn.Articles.ObjectComposer
{
    public class SimpleObjectMapper<S, T> : IObjectMapper<S, T>
        where S : new()
        where T : new()
    {
        public T CreateAndCopy(S source, object[] ignoreProps = null)
        {
            T dest = (T)Activator.CreateInstance(typeof(T));

            return Copy(source, dest, ignoreProps);
        }

        public T Copy(S source, T target, object[] ignorePropList = null)
        {
            List<string> ignoreProps = null;
            if (ignorePropList != null && ignorePropList.Count() > 0)
            {
                ignoreProps = ignorePropList[0] as List<string>;
            }

            // need to check for all nullable properties (long?, float?, etc. not primitive)
            var sourceProps = typeof(S).GetProperties().Where(
                x => x.CanRead
                &&
                (
                    x.PropertyType.IsPrimitive
                    || x.PropertyType.IsAssignableFrom(typeof(string))
                    || x.PropertyType.IsAssignableFrom(typeof(DateTime))
                    || x.PropertyType.IsAssignableFrom(typeof(long))
                    || x.PropertyType.IsAssignableFrom(typeof(decimal))
                    || x.PropertyType.IsAssignableFrom(typeof(bool))
                    || x.PropertyType.IsAssignableFrom(typeof(double))
                    || x.PropertyType.IsAssignableFrom(typeof(float))
                    || x.PropertyType.IsAssignableFrom(typeof(int))
                    || x.PropertyType.IsAssignableFrom(typeof(char))
                    || x.PropertyType.IsAssignableFrom(typeof(short))
                    || x.PropertyType.IsAssignableFrom(typeof(byte))
                )
                &&
                !x.PropertyType.IsInterface
                &&
                !(ignoreProps == null ? false : ignoreProps.Contains(x.Name, StringComparer.CurrentCultureIgnoreCase))
            );

            var destProps = typeof(T).GetProperties().Where(x => x.CanWrite);

            foreach (var sourceProp in sourceProps)
            {
                if (destProps.Any(x => x.Name == sourceProp.Name))
                {
                    var p = destProps.First(x => x.Name == sourceProp.Name);
                    if (p.CanWrite)
                    { // check if the property can be set or no.

                        // check for DateTime and make utc
                        if (sourceProp.PropertyType.IsAssignableFrom(typeof(DateTime)))
                        {
                            var sourceVal = sourceProp.GetValue(source, null);
                            if (sourceVal != null)
                            {
                                p.SetValue(target, TimeUtils.ConvertToUTC((DateTime?)sourceVal), null);
                            }
                        }
                        else
                        {
                            p.SetValue(target, sourceProp.GetValue(source, null), null);
                        }
                    }
                }
            }
            return target;
        }
    }
}
