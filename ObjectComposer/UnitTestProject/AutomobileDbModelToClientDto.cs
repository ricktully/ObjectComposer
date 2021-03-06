using Rtully.LinkedIn.Articles.ObjectComposer;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTestProject
{
    public class AutomobileDbModelToClientDto<S, T> : IObjectMapper<S, T>
    where S:AutomobileDto,new()
    where T:AutomobileDbModel,new()
    {
        private SimpleObjectMapper<S, T> simpleMapper;

        public AutomobileDbModelToClientDto()
        {
            simpleMapper = new SimpleObjectMapper<S, T>();
        }

        public T Copy(S source, T target, params object[] optionalParams)
        {
            throw new NotImplementedException();
        }

        public T CreateAndCopy(S source, params object[] optionalParams)
        {
            throw new NotImplementedException();
        }
    }
}
