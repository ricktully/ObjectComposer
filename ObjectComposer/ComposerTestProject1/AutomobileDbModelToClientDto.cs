using Rtully.LinkedIn.Articles.ObjectComposer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ComposerTestProject
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
            //do shallow copy first
            T carModel = simpleMapper.CreateAndCopy(source);

            //db schema needs a create date
            carModel.CreateDate = DateTime.UtcNow;

            string chars="ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            carModel.VIN = new string(Enumerable.Repeat(chars, 12)
                                                    .Select(s => s[random.Next(s.Length)]).ToArray());

            return carModel;
        }
    }
}
