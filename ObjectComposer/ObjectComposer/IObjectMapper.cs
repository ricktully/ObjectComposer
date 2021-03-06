namespace Rtully.LinkedIn.Articles.ObjectComposer
{
    public interface IObjectMapper<S, T>
    {
        T CreateAndCopy(S source, params object[] optionalParams);

        T Copy(S source, T target, params object[] optionalParams);
    }
}
